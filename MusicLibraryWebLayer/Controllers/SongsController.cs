using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DataLayer;
using DataLayer.DAL;
using DomainClasses;
using DomainClasses.DAL;

namespace MusicLibraryWebLayer.Controllers
{
    public class SongsController : Controller
    {
        // GET: SongRepo
        private ISongRepository songRepository;

        public SongsController()
        {
            this.songRepository = new SongRepository(new LibraryContext());
        }

        public SongsController(ISongRepository repo)
        {
            this.songRepository = repo;
        }
        public ActionResult Index()
        {
            var songs = songRepository.Get().ToList();
            return View(songs);
        }

        // GET: SongRepo/Details/5
        public ActionResult Details(int id)
        {
            Song song = songRepository.GetById(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: SongRepo/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(songRepository.GetArtists(), "Id", "Name");
            return View();
        }

        // POST: SongRepo/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Title,Year,Duration,Likes,IsHit,ArtistId")] Song song)
        {
            string time = song.Duration.ToString();
            string[] values = time.Split(':');
            TimeSpan dur = new TimeSpan(00, Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
            song.Duration = dur;
            if (ModelState.IsValid)
            {
                songRepository.InsertObject(song);
                songRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(songRepository.GetArtists(), "Id", "Name", song.ArtistId);
            return View(song);
        }

        [HttpPost]
        public ActionResult BulkInsert()
        {
            using (var reader = new StreamReader(@"C:\Users\Stefan\Downloads\songs.csv"))
            {
                reader.ReadLine();
                List<Song> songs = new List<Song>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var song = new Song();
                    song.Title = values[0];
                    song.Year = int.Parse(values[1]);

                    song.Duration = TimeSpan.Parse(values[2]);
                    string time = song.Duration.ToString();
                    string[] val = time.Split(':');
                    TimeSpan dur = new TimeSpan(00, Convert.ToInt32(val[0]), Convert.ToInt32(val[1]));

                    song.Likes = Convert.ToInt32(values[3]);

                    bool ishit;
                    song.IsHit = Boolean.TryParse(values[4], out ishit);
                    Artist artist = new Artist();
                    artist.Name = values[5];
                    song.Artist = artist;

                    songs.Add(song);
                }
                foreach (var s in songs)
                {
                    songRepository.InsertObject(s);
                    songRepository.Save();
                }


                return View("BulkInsert");

            }
        }

        // GET: SongRepo/Edit/5
        public ActionResult Edit(int id)
        {
            Song song = songRepository.GetById(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(songRepository.GetArtists(), "Id", "Name", song.ArtistId);
            return View(song);

        }

        // POST: SongRepo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Year,Duration,Likes,IsHit,ArtistId")] Song song)
        {
            if (ModelState.IsValid)
            {
                songRepository.UpdateObject(song);
                songRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(songRepository.GetArtists(), "Id", "Name", song.ArtistId);
            return View(song);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int id)
        {
            Song song = songRepository.GetById(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = songRepository.GetById(id);
            songRepository.DeleteObject(id);
            songRepository.Save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult IncrementLikes(int id)
        {
            Song song = songRepository.GetById(id) ?? throw new ArgumentNullException("No Id found");
            song.Likes += 1;

            if (ModelState.IsValid)
            {

                songRepository.UpdateObject(song);
                songRepository.Save();
                return RedirectToAction("Index");
            }
            //Index();
            return View("Index");

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                songRepository.Dispose(disposing);
            }
            base.Dispose(disposing);
        }


    }
}

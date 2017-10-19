using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.DAL;
using DomainClasses;
using MusicLibraryWebLayer.ViewModels;

namespace MusicLibraryWebLayer.Controllers
{
    public class PlaylistsController : Controller
    {
        private PlaylistRepository playlistRepository;

        public PlaylistsController()
        {
            this.playlistRepository = new PlaylistRepository(new LibraryContext());
        }

        public PlaylistsController(PlaylistRepository repo)
        {
            this.playlistRepository = repo;
        }
        // GET: Playlists
        public ActionResult Index()
        {
            
            return View(playlistRepository.Get());
        }

        // GET: Playlists/Details/5
        public ActionResult Details(int id)
        {
            
            Playlist playlist = playlistRepository.GetById(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(playlist);
        }

        // GET: Playlists/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(playlistRepository.GetGenres(), "Id", "Name");
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Likes,GenreId")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
               playlistRepository.InsertObject(playlist);
                playlistRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(playlistRepository.GetGenres(), "Id", "Name", playlist.GenreId);
            return View(playlist);
        }

        // GET: Playlists/Edit/5
        public ActionResult Edit(int id)
        {

            Playlist playlist = playlistRepository.GetById(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(playlistRepository.GetGenres(), "Id", "Name", playlist.GenreId);
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Likes,GenreId")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                playlistRepository.UpdateObject(playlist);
                playlistRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(playlistRepository.GetGenres(), "Id", "Name", playlist.GenreId);
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        public ActionResult Delete(int id)
        {
           
            Playlist playlist = playlistRepository.GetById(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            playlistRepository.DeleteObject(id);
            playlistRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult AddSongs(int id)
        {
            var songs = playlistRepository.GetSongs();
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            Playlist playlist = playlistRepository.GetById(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }

            var viewModel = new AddSongsViewModel
            {
                Songs = songs.ToList(),
                PlaylistId = id
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AddSongToPlaylist(int songId, int playListId)
        {

            var pid = playListId;
            var songs = playlistRepository.GetSongs();

            Playlist list = playlistRepository.GetById(pid);
            var song = playlistRepository.GetSongById(songId);
            
            if (list != null)
            {
                list.Songs.Add(song);

                var viewModel = new AddSongsViewModel
                {
                    Songs = songs.ToList(),
                    PlaylistId = playListId
                };
                if (ModelState.IsValid)
                {
                    //db.Playlists.Add(list);
                    try
                    {
                       playlistRepository.UpdateObject(list);
                        playlistRepository.Save();

                        return RedirectToAction("Index");
                    }
                    catch (Exception x)
                    {
                        viewModel.ErrorMessage = "Song already exists in current playlist";
                        return View("AddSongs",viewModel);
                    }
                }
            }

            return View("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                playlistRepository.Dispose(disposing);
            }
            base.Dispose(disposing);
        }



    }
}

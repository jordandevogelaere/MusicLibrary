using System;
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
using DomainClasses.DAL;

namespace MusicLibraryWebLayer.Controllers
{
    public class ArtistsController : Controller
    {
        private IArtistRepository artistRepository;

        public ArtistsController()
        {
            this.artistRepository = new ArtistRepository(new LibraryContext());
        }

        public ArtistsController(IArtistRepository repo)
        {
            this.artistRepository = repo;
        }

        // GET: Artists
        public ActionResult Index()
        {
            
            return View(artistRepository.Get());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = artistRepository.GetById(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            var s = artistRepository.GetSongs().Where(a => a.ArtistId == id);
            foreach (var song in s)
                artist.Songs.Add(song);
            return View(artist);
        }

        // GET: Artists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AboutInfo,Origin,BirthDate")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                artistRepository.InsertObject(artist);
                artistRepository.Save();
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        // GET: Artists/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = artistRepository.GetById(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,AboutInfo,Origin,BirthDate")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                artistRepository.UpdateObject(artist);
                artistRepository.Save();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = artistRepository.GetById(id);
            if (artist == null)
            {
                return HttpNotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = artistRepository.GetById(id);
            artistRepository.DeleteObject(id);
            artistRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                artistRepository.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}

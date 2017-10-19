using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.DAL;
using DomainClasses;

namespace MusicLibraryWebLayer.Controllers
{
    public class GenresController : Controller
    {
        private GenreRepository genreRepository;

        public GenresController()
        {
            this.genreRepository = new GenreRepository(new LibraryContext());
        }

        public GenresController(GenreRepository repo)
        {
            this.genreRepository = repo;
        }
        // GET: Genres
        public ActionResult Index()
        {
            return View(genreRepository.Get().ToList());
        }

        // GET: Genres/Details/5
        public ActionResult Details(int id)
        {

            Genre genre = genreRepository.GetById(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                genreRepository.InsertObject(genre);
                genreRepository.Save();
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(int id)
        {

            Genre genre = genreRepository.GetById(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                genreRepository.UpdateObject(genre);
                genreRepository.Save();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(int id)
        {

            Genre genre = genreRepository.GetById(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            genreRepository.DeleteObject(id);
           genreRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                genreRepository.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}

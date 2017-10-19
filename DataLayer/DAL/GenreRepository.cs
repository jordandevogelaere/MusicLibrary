using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses;

namespace DataLayer.DAL
{
    public class GenreRepository:IRepository<Genre>
    {
        private LibraryContext Context;
        public GenreRepository(LibraryContext context)
        {
            this.Context = context;
        }
        public IEnumerable<Genre> Get()
        {
            return Context.Genres.ToList();
        }

        public Genre GetById(int id)
        {
            return Context.Genres.Find(id);
        }

        public void InsertObject(Genre obj)
        {
            Context.Genres.Add(obj);
        }

        public void DeleteObject(int id)
        {
            Genre genre = Context.Genres.Find(id);
            Context.Genres.Remove(genre);
        }

        public void UpdateObject(Genre obj)
        {
            Context.Entry(obj).State = EntityState.Modified;
        }

        public void Dispose(bool disposing)
        {
            Context?.Dispose();
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}

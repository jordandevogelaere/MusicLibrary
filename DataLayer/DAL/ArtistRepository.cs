using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses;

namespace DataLayer.DAL
{
    public class ArtistRepository:IArtistRepository
    {
        private LibraryContext Context;

        public ArtistRepository(LibraryContext context)
        {
            this.Context = context;
        }
        public IEnumerable<Artist> Get()
        {
            
            return Context.Artists.ToList();
        }

        public Artist GetById(int id)
        {
            return Context.Artists.Find(id);
        }

        public void InsertObject(Artist obj)
        {
            Context.Artists.Add(obj);
        }

        public void DeleteObject(int id)
        {
            Artist artist = Context.Artists.Find(id);
            Context.Artists.Remove(artist);
        }

        public void UpdateObject(Artist obj)
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

        public IEnumerable<Song> GetSongs()
        {
            return Context.Songs.ToList();
        }
    }
}

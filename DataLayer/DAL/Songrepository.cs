using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.DAL;

namespace DomainClasses.DAL
{
    public class SongRepository : ISongRepository
    {
        private LibraryContext Context;

        public SongRepository(LibraryContext context)
        {
            this.Context = context;
        }
        

        public IEnumerable<Song> Get()
        {
            var songs = Context.Songs.Include(a => a.Artist);
            return songs.ToList();
        }

        public IEnumerable<Artist> GetArtists()
        {
            return Context.Artists.ToList();
        }

        public Song GetById(int id)
        {
            return Context.Songs.Find(id);
        }

        public void InsertObject(Song obj)
        {
            Context.Songs.Add(obj);
        }

        public void DeleteObject(int id)
        {
            Song song = Context.Songs.Find(id);
            Context.Songs.Remove(song);
        }

        public void UpdateObject(Song obj)
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

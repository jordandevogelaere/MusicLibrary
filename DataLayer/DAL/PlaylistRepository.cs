using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses;

namespace DataLayer.DAL
{
    public class PlaylistRepository:IPlaylistRepository
    {
        private LibraryContext Context;

        public PlaylistRepository(LibraryContext context)
        {
            this.Context = context;
        }
        public IEnumerable<Playlist> Get()
        {
            var playlists = Context.Playlists.Include(g => g.Genre);
            return  playlists.ToList();
        }

        public Playlist GetById(int id)
        {
            return Context.Playlists.Find(id);
        }

        public void InsertObject(Playlist obj)
        {
            Context.Playlists.Add(obj);
        }

        public void DeleteObject(int id)
        {
            Playlist playlist = Context.Playlists.Find(id);
            Context.Playlists.Remove(playlist); ;
        }

        public void UpdateObject(Playlist obj)
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

        public IEnumerable<Genre> GetGenres()
        {
            return Context.Genres.ToList();
        }

        public IEnumerable<Song> GetSongs()
        {
            return Context.Songs.ToList();
        }

        public Song GetSongById(int id)
        {
            var song = Context.Songs.Find(id);
            return song;
        }
    }
}

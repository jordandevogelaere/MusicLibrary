using System.Data.Entity;
using DomainClasses;

namespace DataLayer
{
    public class LibraryContext:DbContext
    {
        public LibraryContext() : base("name = MusicLibraryConnection")
        { }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Playlist> Playlists { get; set; }


    }
}
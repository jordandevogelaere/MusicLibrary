using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses;


namespace DataLayer
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("name = MusicLibraryConnection")
        { }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        

    }
}

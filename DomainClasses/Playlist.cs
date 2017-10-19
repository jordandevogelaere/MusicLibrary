using System.Collections.Generic;

namespace DomainClasses
{
    public class Playlist
    {
        public Playlist()
        {
            this.Songs=new HashSet<Song>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Likes { get; set; }
        public Genre Genre { get; set; }
        public int? GenreId { get; set; }
    
        public ICollection<Song> Songs { get; set; }
    }
}
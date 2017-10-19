using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
   public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public TimeSpan Duration { get; set; }
        public int Likes { get; set; }
        public bool IsHit { get; set; }
        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}

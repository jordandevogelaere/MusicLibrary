using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Likes { get; set; }
        public Genre Genre { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}

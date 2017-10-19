using System;
using System.Collections.Generic;

namespace DomainClasses
{
    public class Artist
    {
        public Artist()
        {
            Songs=new HashSet<Song>();
            Genres=new HashSet<Genre>();
        }
        public int Id { get; set; }

        public string Name { get; set; }
        public string AboutInfo { get; set; }
        public string Origin { get; set; }
        public DateTime? BirthDate { get; set; }

        public  ICollection<Song> Songs { get; set; }
        public  ICollection<Genre> Genres { get; set; }
    }
}
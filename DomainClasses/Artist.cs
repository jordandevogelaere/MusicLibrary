using System;
using System.Collections;
using System.Collections.Generic;

namespace DomainClasses
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AboutInfo { get; set; }
        public string Origin { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
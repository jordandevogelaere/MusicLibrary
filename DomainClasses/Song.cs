using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DomainClasses
{
    public class Song 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        
        public TimeSpan Duration { get; set; }
        [DefaultValue(0)]
        public int Likes { get; set; }
        [DefaultValue(false)]
        public bool IsHit { get; set; }
        public Artist Artist { get; set; }

        public int  ArtistId { get; set; }
        public ICollection<Playlist> Playlists { get; set; }

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainClasses;

namespace MusicLibraryWebLayer.ViewModels
{
    public class AddSongsByCsvViewModel 
    {
        public List<Song> ExistingSongsInDb { get; set; }
        public List<Song> NewSongsInDb { get; set; }

    }
}
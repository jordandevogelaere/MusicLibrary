using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainClasses;

namespace MusicLibraryWebLayer.ViewModels
{
    public class AddSongsViewModel
    {
        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; }
        public List<Song> Songs { get; set; }
        public List<Song> SongsNotInPlaylist { get; set; }
        public string ErrorMessage { get; set; }

       
    }
}
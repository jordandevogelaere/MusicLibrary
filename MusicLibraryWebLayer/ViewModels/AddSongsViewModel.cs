using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainClasses;

namespace MusicLibraryWebLayer.ViewModels
{
    public class AddSongsViewModel
    {
        public int PlaylistId { get; set; }
        public List<Song> Songs { get; set; }
        public string ErrorMessage { get; set; }
    }
}
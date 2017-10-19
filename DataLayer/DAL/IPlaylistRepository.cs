using System.Collections.Generic;
using DomainClasses;

namespace DataLayer.DAL
{
    public interface IPlaylistRepository:IRepository<Playlist>
    {
        IEnumerable<Genre> GetGenres();
        IEnumerable<Song> GetSongs();
        Song GetSongById(int id);
    }
}
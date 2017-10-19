using System.Collections.Generic;
using DomainClasses;

namespace DataLayer.DAL
{
    public interface IArtistRepository:IRepository<Artist>
    {
        IEnumerable<Song> GetSongs();

    }
}
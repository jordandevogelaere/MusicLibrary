using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DAL;

namespace DomainClasses.DAL
{
    public interface ISongRepository : IRepository<Song>
    {
        IEnumerable<Artist> GetArtists();

    }


}

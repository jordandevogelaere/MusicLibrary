using DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new LibraryContext())
            {
                Console.WriteLine(ctx);
                Artist artist = new Artist {Name = "Adele", BirthDate = new DateTime(1988, 5, 5), Origin = "Tottenham"};
                Artist artist2= new Artist { Name = "Kempi & The Blockparty", Origin = "Nederland", BirthDate = new DateTime(1988, 5, 5) };
                

                Song song = new Song {Title = "Someone Like You",Year = new DateTime(2011,1,1).Year,Duration = new TimeSpan(0,3,30),IsHit = true};
                Song song2 = new Song { Title = "Cocaina", Year = new DateTime(2017,10,2).Year, Duration = new TimeSpan(0, 4, 30), IsHit = true };
                Song song3 = new Song { Title = "Hello", Year = new DateTime(2011,1,1).Year, Duration = new TimeSpan(0, 3, 15), IsHit = true };
                Song song4 = new Song { Title = "TestAdele", Year = new DateTime(2011, 1, 1).Year, Duration = new TimeSpan(0, 3, 15), IsHit = true };
                artist2.Songs=new List<Song>();
                artist2.Songs.Add((song2));

                song4.Artists=new List<Artist>();
                song4.Artists.Add(artist);

                artist.Songs=new List<Song>();
                artist.Songs.Add(song);
                artist.Songs.Add(song3);
                //artist.Songs.Add(song4);

                Playlist playlist1 = new Playlist {Name = "Summer"};
                playlist1.Songs=new List<Song>();
                playlist1.Songs.Add(song);
                playlist1.Songs.Add((song2));

                Playlist playlist2 = new Playlist {Name = "AdeleSongs"};
                playlist2.Songs=new List<Song>();
                playlist2.Songs.Add(song);
                playlist2.Songs.Add(song3);

                //Console.WriteLine(song.ToString());
                //ctx.Artists.Add((artist));
                //ctx.Artists.Add(artist2);
                //ctx.Songs.Add(song2);
                //ctx.Songs.Add((song));
                //ctx.Playlists.Add(playlist2);
                //ctx.Playlists.Add(playlist1);
                ctx.Songs.Add(song4);
                Console.WriteLine(song4);
                ctx.SaveChanges();
            }
        }
    }
}

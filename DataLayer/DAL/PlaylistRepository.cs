using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses;

namespace DataLayer.DAL
{
    public class PlaylistRepository:IPlaylistRepository
    {
        private LibraryContext Context;

        public PlaylistRepository(LibraryContext context)
        {
            this.Context = context;
        }
        public IEnumerable<Playlist> Get()
        {
            var playlists = Context.Playlists.Include(g => g.Genre);
            return  playlists.ToList();
        }

        public Playlist GetById(int id)
        {
            return Context.Playlists.Find(id);
        }

        public void InsertObject(Playlist obj)
        {
            Context.Playlists.Add(obj);
        }

        public void DeleteObject(int id)
        {
            Playlist playlist = Context.Playlists.Find(id);
            Context.Playlists.Remove(playlist); ;
        }

        public void UpdateObject(Playlist obj)
        {
            Context.Entry(obj).State = EntityState.Modified;
        }

        public void Dispose(bool disposing)
        {
            Context?.Dispose();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public IEnumerable<Genre> GetGenres()
        {
            return Context.Genres.ToList();
        }

        public IEnumerable<Song> GetSongs()
        {
            return Context.Songs.ToList();
        }

        public Song GetSongById(int id)
        {
            var song = Context.Songs.Find(id);
            return song;
        }

        public List<Song> GetSongsOfPlaylist(int playlistId)
        {
            List<Song> songs=new List<Song>();
            using (SqlConnection conn =
                new SqlConnection(
                    "Data Source=XPS13-JORDAN\\SQLEXPRESS;Initial Catalog=MusicLibraryTest;Integrated Security=True"))
            {
                
                SqlCommand cmd=new SqlCommand();
                cmd.CommandText =
                    "SELECT Artists.Name,Songs.Title,Songs.Duration,Songs.Year,Songs.Likes\r\nFROM Songs\r\nINNER JOIN PlaylistSongs on Songs.Id=PlaylistSongs.Song_Id\r\nInner join Artists on Songs.ArtistId=Artists.Id\r\nWHERE Playlist_Id=@pid";
                cmd.Parameters.Add("@pid", SqlDbType.Int);
                cmd.Parameters[0].Value = playlistId;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader=cmd.ExecuteReader(); 
                while (reader.Read())
                {
                    var song=new Song();
                    song.Artist=new Artist();
                    song.Artist.Name = reader[0].ToString();
                    song.Title = reader[1].ToString();
                    song.Duration = (TimeSpan) reader[2];
                    song.Year = (int) reader[3];
                    song.Likes = (int) reader[4];
                    songs.Add(song);
                }
                conn.Close();
                return songs;
            }
        }
    }
}

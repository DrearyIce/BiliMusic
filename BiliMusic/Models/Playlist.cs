using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliMusic.Models
{
    public class Playlist
    {
        public List<Song> songs = new List<Song>();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
    }

    public static class PlayLists
    {
        public static List<Playlist> Playlists = new List<Playlist>();
        
        public static void preLoad()
        {
            Playlists = new List<Playlist>();
            DirectoryInfo info = new DirectoryInfo(EnvironmentVariables.PlaylistPath);
            foreach (FileInfo file in info.GetFiles())
            {
                Playlist? pl = JsonConvert.DeserializeObject<Playlist>(File.ReadAllText(file.FullName));
                if (pl != null)
                    Playlists.Add(pl);
                else
                    Playlists.Add(new Playlist());
            }
        }

        public static void SavePlaylists()
        {
            foreach (var item in Playlists)
            {
                util_File.DeleteFile(item.Path);
                util_File.CreateWriteFile(JsonConvert.SerializeObject(item), item.Path);
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliMusic.Models
{
    public static class Favorite
    {
        public static List<Song> FavoriteSongs = new List<Song>();

        public static void LoadFav()
        {
            FavoriteSongs.Clear();
            FavoriteSongs = JsonConvert.DeserializeObject<List<Song>>(File.ReadAllText(EnvironmentVariables.FavoritePath));
            try
            {
                if (FavoriteSongs.Count == 0)
                    FavoriteSongs = new List<Song>();
            }
            catch
            {
                FavoriteSongs = new List<Song>();
            }
        }

        public static void SaveFav()
        {
            util_File.DeleteFile(EnvironmentVariables.FavoritePath);
            util_File.CreateWriteFile(JsonConvert.SerializeObject(FavoriteSongs), EnvironmentVariables.FavoritePath);
        }

        public static bool TryFindinFav(Song song)
        {
            foreach (var item in FavoriteSongs)
                if (item.AId == song.AId)
                    return true;
            return false;
        }

        public static void AddtoFav(Song song)
        {
            if(!FavoriteSongs.Exists(s => s.AId == song.AId))
                FavoriteSongs.Add(song);
        }

        public static void RemovefromFav(Song song)
        {
            FavoriteSongs.Remove(song);
        }
    }
}

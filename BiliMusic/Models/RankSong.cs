using System.IO;

namespace BiliMusic.Models
{
    public class RankSong : Song
    {
        
    }

    public static class RankSongMethods
    {
        public static string GetToday()
        {
            FileInfo info = new FileInfo(EnvironmentVariables.RankSongPath + $"\\{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}.rsf");
            if (info.Exists)
                return info.FullName;
            else
                return util_File.SaveFile_FromUrl(EnvironmentVariables.url_RankSong, info.FullName).FullName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliMusic.Models
{
    public  class Recommend : Song
    {

    }

    public static class RecommendMethods
    {
        public static string GetSongs(Song song)
        {
            FileInfo info = new FileInfo(EnvironmentVariables.RecommendPath + $"\\{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}.his");
            if (info.Exists)
                return info.FullName;
            else
                return util_File.SaveFile_FromUrl(EnvironmentVariables.url_Recommend + song.AId, info.FullName).FullName;
        }
    }
}

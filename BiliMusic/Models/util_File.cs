using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace BiliMusic.Models
{
    public static class util_File
    {
        public static FileInfo SaveFile_FromUrl(string Url, string fullname)
        {
            var str = util_WebReq.Post(Url);
            CreateWriteFile(str, fullname);
            return new FileInfo(fullname);
        }

        public static FileInfo SaveFile_SongJson(Song song)
        {
            var str = JsonConvert.SerializeObject(song);
            CreateWriteFile(str, EnvironmentVariables.SavesPath + "\\" + song.AId + "\\Song.json");
            return new FileInfo(EnvironmentVariables.SavesPath + "\\" + song.AId + "\\Song.json");
        }

        public static void CreateWriteFile(string content, string filepath)
        {
            using (FileStream fs = File.Create(filepath))
            {
                byte[] buffer = new UTF8Encoding(true).GetBytes(content);
                fs.Write(buffer);
                fs.Close();
                fs.Dispose();
            }
        }

        public static void DeleteFile(string path)
        {
            FileInfo file = new FileInfo(path);
            file.Delete();
        }

        public static async Task<string> GetSongPath(Song song)
        {
            FileInfo fi = new FileInfo($"{EnvironmentVariables.SavesPath}\\{song.AId}\\Song.m4a");
            if (fi.Exists)
                return fi.FullName;

            DirectoryInfo directory = new DirectoryInfo(EnvironmentVariables.SavesPath + "\\" + song.AId);
            if (directory.Exists)
                foreach (DirectoryInfo info in directory.GetDirectories())
                    foreach (FileInfo file in info.GetFiles())
                    {
                        if (file.Extension == ".m4a")
                        {
                            //File.Move(file.FullName, $"{file.DirectoryName}\\Song{SongIndex}.m4a");
                            util_File.SaveFile_SongJson(song);
                            //SongIndex++;
                        }
                        return null;
                    }
            try
            {
                FileInfo file = await util_Console.DownloadSong(song);
                return file.FullName;
            }
            catch { return null; }
        }

        public static async Task<List<string>> GetCollectionSongs(Song song)
        {
            List<string> Collection = new List<string>();
            DirectoryInfo directory = new DirectoryInfo(EnvironmentVariables.SavesPath + "\\" + song.AId);
            foreach (DirectoryInfo info in directory.GetDirectories())
                foreach (FileInfo file in info.GetFiles())
                    if (file.Extension == ".m4a")
                        Collection.Add(file.FullName);
            return Collection;
        }
    }
}

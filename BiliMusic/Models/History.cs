using Microsoft.Windows.Themes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xaml;

namespace BiliMusic.Models
{
    public static class History
    {
        public static List<Song> TodayHistory = new List<Song>();
        public static FileInfo file { get; set; }

        public static void LoadHistory()
        {
            TodayHistory.Clear();
            DateTime dN = DateTime.Now;
            file = new FileInfo($"{EnvironmentVariables.HistoryPath}\\{dN.Year}{dN.Month}{dN.Day}.history");
            if (file.Exists)
            {
                try
                {
                    TodayHistory = JsonConvert.DeserializeObject<List<Song>>(File.ReadAllText(file.FullName));
                    if (TodayHistory == null)
                        TodayHistory = new List<Song>();
                }
                catch
                {
                    TodayHistory = new List<Song>();
                }
            }
            else
                util_File.CreateWriteFile("", file.FullName);
        }

        public static void SaveHistory()
        {
            util_File.DeleteFile(file.FullName);
            util_File.CreateWriteFile(JsonConvert.SerializeObject(TodayHistory), file.FullName);
        }

        public static void AddtoHistory(Song song)
        {
            if (!TodayHistory.Exists(s => s.AId == song.AId))
                TodayHistory.Add(song);
        }

        public static List<Song> GetWeeklyHistory()
        {
            List<Song> list = new List<Song>();
            for (int i = 1; i < 7; i++)
            {
                DateTime w = DateTime.Now - TimeSpan.FromDays(i);
                FileInfo info = new FileInfo($"{EnvironmentVariables.HistoryPath}\\{w.Year}{w.Month}{w.Day}.history");
                if (info.Exists)
                {
                    try
                    {
                        List<Song>? history = JsonConvert.DeserializeObject<List<Song>>(File.ReadAllText(info.FullName));
                        if (history != null)
                            foreach (var item in history)
                                list.Add(item);
                    }
                    catch { }
                }
            }
            return list;
        }

        public static List<Song> GetAllHistory()
        {
            List<Song> list = new List<Song>();
            DirectoryInfo directory = new DirectoryInfo(EnvironmentVariables.HistoryPath);
            foreach (FileInfo info in directory.GetFiles())
            {
                try
                {
                    List<Song>? history = JsonConvert.DeserializeObject<List<Song>>(File.ReadAllText(info.FullName));
                    if (history != null)
                        foreach (var item in history)
                            list.Add(item);
                }
                catch { }
            }
            return list;
        }
    }
}

using BiliMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BiliMusic
{
    public static class PlayingList
    {
        public static MainWindow mainWindow { get; set; }
        public static List<Song> PlayList = new List<Song>();
        public static long PlayingIndex = -1;
        
        public static void pre_Main(MainWindow mw)
        {
            if (mw != null)
                mainWindow = mw;
        }

        public static void ClearList()
        {
            PlayList = new List<Song>();
            PlayingIndex = -1;
            mainWindow.Playing.Media.Stop();
            mainWindow.RefreshPlayingMusic();
        }

        public static void Remove(Song song)
        {
            int index = PlayList.IndexOf(song);
            if (-1 < index && index < PlayingIndex)
                PlayingIndex--;
            
            PlayList.Remove(song);
            mainWindow.RefreshPlayingMusic();
        }

        public static void FindandPlay(Song song)
        {
            PlayingIndex = PlayList.IndexOf(song);
            mainWindow.Playing.Play(song);
        }

        public static void AddMusicToList_andPlay(Song song)
        {
            if (PlayList.Count > 0)
                PlayList.Insert((int)PlayingIndex, song);
            else
                PlayList.Add(song);
            PlayingIndex++;
            mainWindow.Playing.Play(song);
            mainWindow.RefreshPlayingMusic();
        }

        public static void AddMusicToListNext(Song song)
        {
            if (PlayList.Count > 0)
                PlayList.Insert((int)PlayingIndex, song);
            else
                PlayList.Add(song);
            mainWindow.RefreshPlayingMusic();
        }

        public static void AddMusicToListEnd(Song song)
        {
            PlayList.Add(song);
            mainWindow.RefreshPlayingMusic();
        }

        public static void ReplaceList(List<Song> list)
        {
            if (list.Count > 0)
            {
                PlayList = list;
                PlayingIndex = -1;
                PlayNextSong();
            }
            mainWindow.RefreshPlayingMusic();
        }

        public static void ReplaceList(Models.Playlist list)
        {
            if (list.songs.Count > 0)
            {
                PlayList = list.songs;
                PlayingIndex = -1;
                PlayNextSong();
            }
            mainWindow.RefreshPlayingMusic();
        }

        public static void ReplaySong()
        {
            mainWindow.Playing.Play(PlayList[(int)PlayingIndex]);
        }

        public static void PlayRandomSong()
        {
            Random rand = new Random();
            PlayingIndex = rand.Next(PlayList.Count);
            mainWindow.Playing.Play(PlayList[(int)PlayingIndex]);
        }

        public static void PlayPerviousSong()
        {
            if (PlayList.Count > 0)
            {
                try
                {
                    if (PlayingIndex == 0)
                        PlayingIndex = PlayList.Count - 1;
                    else
                        PlayingIndex--;
                    mainWindow.Playing.Play(PlayList[(int)PlayingIndex]);
                }
                catch
                {
                    PlayingIndex = 0;
                }
            }
        }

        public static void PlayNextSong()
        {
            if (PlayList.Count > 0) 
            {
                try
                {
                    if (PlayingIndex == PlayList.Count - 1)
                        PlayingIndex = 0;
                    else
                        PlayingIndex++;
                    mainWindow.Playing.Play(PlayList[(int)PlayingIndex]);
                }
                catch
                {
                    PlayingIndex = 0;
                }
            }
        }
    }
}

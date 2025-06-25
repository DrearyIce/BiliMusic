using BiliMusic.Models;
using BiliMusic.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BiliMusic.Controls.Flyout
{
    /// <summary>
    /// AddMusic.xaml 的交互逻辑
    /// </summary>
    public partial class AddMusic : UserControl
    {
        public AddMusic()
        {
            InitializeComponent();
        }

        List<Song> songs = new List<Song>();
        Playlist _playlist;
        public AddMusic(Playlist playlist)
        {
            InitializeComponent();

            _playlist = playlist;
            Add.Click += Add_Click;
        }

        public AddMusic(bool isPlayingList)
        {
            InitializeComponent();
            Add.Click += Add_Click1;
        }

        private void Add_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] strs = Regex.Split(bvs.Text, ";", RegexOptions.IgnoreCase);
                foreach (string str in strs)
                    songs.Add(util_Regex.GetVideoInformation(util_BvConverter.Decode(str.Substring(3)).ToString()));
                foreach (var item in songs)
                    PlayingList.AddMusicToListEnd(item);
            }
            catch { }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] strs = Regex.Split(bvs.Text, ";", RegexOptions.IgnoreCase);
                foreach (string str in strs)
                    songs.Add(util_Regex.GetVideoInformation(util_BvConverter.Decode(str.Substring(3)).ToString()));
                int index = PlayLists.Playlists.IndexOf(_playlist);
                if (index != -1)
                    foreach (var item in songs)
                        PlayLists.Playlists[index].songs.Add(item);
                ActionTip actionTip = new ActionTip() { Text = "Addition successful, click Refresh to refresh page." };
                actionTip.Show();
            }
            catch { }
        }
    }
}

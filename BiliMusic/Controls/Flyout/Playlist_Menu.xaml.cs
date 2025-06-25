using BiliMusic.Models;
using BiliMusic.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    /// Playlist_Menu.xaml 的交互逻辑
    /// </summary>
    public partial class Playlist_Menu : UserControl
    {
        Playlist _playlist;
        public Playlist_Menu(Playlist playlist)
        {
            InitializeComponent();

            _playlist = playlist;
        }

        private void ply_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.ReplaceList(_playlist);
        }

        private void Addtolist_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in _playlist.songs)
                PlayingList.AddMusicToListNext(item);
        }

        private void addtoplayinglist_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in _playlist.songs)
                PlayingList.AddMusicToListEnd(item);
        }

        private void removefromlist_Click(object sender, RoutedEventArgs e)
        {
            File.Delete(_playlist.Path);
            PlayLists.preLoad();
        }

        private void shar_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase @base = new FlyoutBase(new ShareMusic(_playlist.songs), 120, 220);
            @base.Show();
        }
    }
}

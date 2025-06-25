using BiliMusic.Models;
using BiliMusic.Windows;
using System;
using System.Collections.Generic;
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
    /// Music_Menu.xaml 的交互逻辑
    /// </summary>
    public partial class Music_Menu : UserControl
    {
        Song _song = null;
        public Music_Menu(Song song)
        {
            InitializeComponent();

            _song = song;
            if (Favorite.TryFindinFav(song))
            {
                f_y.Visibility = Visibility.Visible;
                f_n.Visibility = Visibility.Collapsed;
            }
        }

        Playlist playlist = null;
        public Music_Menu(Song song, Playlist pl)
        {
            InitializeComponent();

            playlist = pl;
            _song = song;
            if (Favorite.TryFindinFav(song))
            {
                f_y.Visibility = Visibility.Visible;
                f_n.Visibility = Visibility.Collapsed;
                addtoplayinglist.Visibility = Visibility.Collapsed;
            }
            removefromlist.Visibility = Visibility.Visible;
        }

        public Music_Menu(Song song, bool isPlayingList)
        {
            InitializeComponent();

            _song = song;
            if (Favorite.TryFindinFav(song))
            {
                f_y.Visibility = Visibility.Visible;
                f_n.Visibility = Visibility.Collapsed;
                addtoplayinglist.Visibility = Visibility.Collapsed;
            }
            removefromlist.Visibility = Visibility.Visible;
            removefromlist.Click += Removefromlist_Click;
        }

        private void Removefromlist_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.Remove(_song);
        }

        private void ply_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.AddMusicToList_andPlay(_song);
        }

        private void f_n_Click(object sender, RoutedEventArgs e)
        {
            Favorite.AddtoFav(_song);
        }

        private void f_y_Click(object sender, RoutedEventArgs e)
        {
            Favorite.RemovefromFav(_song);
        }

        private void Addtolist_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.AddMusicToListNext(_song);
        }

        private void addtoplayinglist_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.AddMusicToListEnd(_song);
        }

        private void addtolist_Click_1(object sender, RoutedEventArgs e)
        {
            AddToPlaylist apl = new AddToPlaylist(_song);
            apl.Show();
        }

        private void shar_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase @base = new FlyoutBase(new ShareMusic(_song), 120, 220);
            @base.Show();
        }

        private void removefromlist_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in PlayLists.Playlists)
                if (item == playlist)
                {
                    item.songs.Remove(_song);
                    PlayingList.mainWindow.MFrame.Refresh();
                    PlayingList.mainWindow.MFrame.Navigate(new Pages.Playlist(item));
                }
        }
    }
}

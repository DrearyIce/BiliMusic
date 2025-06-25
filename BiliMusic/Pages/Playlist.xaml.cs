using BiliMusic.Controls;
using BiliMusic.Controls.Flyout;
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

namespace BiliMusic.Pages
{
    /// <summary>
    /// Playlist.xaml 的交互逻辑
    /// </summary>
    public partial class Playlist : Page
    {
        Models.Playlist _playlist { get; set; }
        public Playlist(Models.Playlist playlist)
        {
            InitializeComponent();

            _playlist = playlist;
            _PlayAll.Click += _PlayAll_Click;
            _ViewAll.Click += _ViewAll_Click;
            _Refresh.Click += _Refresh_Click;
            Loaded += Playlist_Loaded;
            _Title.Text = playlist.Title;
            _Desc.Text = playlist.Description;
            if (_Desc.Text == "")
                _Desc.Text = "No description.";
        }

        public Playlist()
        {
            InitializeComponent();
        }

        private void Playlist_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in _playlist.songs)
                songs.Children.Add(new MusicCard_Small(item, _playlist));
        }

        private void _Refresh_Click(object sender, RoutedEventArgs e)
        {
            songs.Children.Clear();
            foreach (var item in _playlist.songs)
                songs.Children.Add(new MusicCard_Small(item, _playlist));
        }

        private void _ViewAll_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.mainWindow.MFrame.Navigate(new Playlists());
        }

        private void _PlayAll_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.ReplaceList(_playlist);
        }

        private void _Add_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase @base = new FlyoutBase(new AddMusic(_playlist), 140, 300);
            @base.Show();
        }
    }
}

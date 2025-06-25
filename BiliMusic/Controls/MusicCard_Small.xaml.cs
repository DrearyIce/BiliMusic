using BiliMusic.Controls.Flyout;
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

namespace BiliMusic.Controls
{
    /// <summary>
    /// MusicCard_Small.xaml 的交互逻辑
    /// </summary>
    public partial class MusicCard_Small : UserControl
    {
        public Song _Song
        {
            get { return (Song)GetValue(_SongProperty); }
            set { SetValue(_SongProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _Song.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _SongProperty =
            DependencyProperty.Register("_Song", typeof(Song), typeof(MusicCard_Small), new PropertyMetadata(new Song()));

        public MusicCard_Small()
        {
            InitializeComponent();

            Cursor = Cursors.Hand;
        }

        public MusicCard_Small(Song song)
        {
            InitializeComponent();

            Cover.Source = BitmapFrame.Create(new Uri(song.Pic + "@64w_64h_1c"));
            Title.Text = song.Title;
            UPName.Text = song.UPName;

            Cursor = Cursors.Hand;
            MouseUp += MusicCard_Large_MouseUp;
            MouseDown += MusicCard_Large_MouseDown;
            _Song = song;
        }

        bool isDown = false;
        private void MusicCard_Large_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDown = true;
        }

        private void MusicCard_Large_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDown)
                PlayingList.AddMusicToList_andPlay(_Song);
            isDown = false;
        }

        public MusicCard_Small(Song song, bool isPlayingList)
        {
            InitializeComponent();

            isInlist = true;
            Cover.Source = BitmapFrame.Create(new Uri(song.Pic + "@200w_200h_1c"));
            Title.Text = song.Title;
            UPName.Text = song.UPName;
            Cursor = Cursors.Hand;
            MouseUp += MusicCard_Small_MouseUp; ;
            MouseDown += MusicCard_Large_MouseDown;
            More.Click += More_Click1;
            _Song = song;
        }

        private void MusicCard_Small_MouseUp(object sender, MouseButtonEventArgs e)
        {
            PlayingList.FindandPlay(_Song);
        }

        private void More_Click1(object sender, RoutedEventArgs e)
        {
            FlyoutBase @base = new FlyoutBase(new Music_Menu(_Song, true), 158, 130);
            @base.Show();
        }

        bool isInlist = false;
        Playlist _playlist = null;
        public MusicCard_Small(Song song, Playlist playlist)
        {
            InitializeComponent();

            isInlist = true;
            Cover.Source = BitmapFrame.Create(new Uri(song.Pic + "@200w_200h_1c"));
            Title.Text = song.Title;
            UPName.Text = song.UPName;
            _playlist = playlist;
            Cursor = Cursors.Hand;
            MouseUp += MusicCard_Large_MouseUp;
            MouseDown += MusicCard_Large_MouseDown;
            _Song = song;
        }

        private void ShowVideoList_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.mainWindow.MFrame.Navigate(new Pages.Videolist(new User() { mid = _Song.UPID, Name = _Song.UPName }));
        }

        private void More_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase fb;
            if (isInlist)
                fb = new FlyoutBase(new Music_Menu(_Song, _playlist), 158, 130);
            else
                fb = new FlyoutBase(new Music_Menu(_Song), 158, 130);
            fb.Show();
        }
    }
}

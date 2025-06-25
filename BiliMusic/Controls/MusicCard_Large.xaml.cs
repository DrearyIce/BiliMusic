using BiliMusic.Controls.Flyout;
using BiliMusic.Models;
using BiliMusic.Windows;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace BiliMusic.Controls
{
    /// <summary>
    /// MusicCard_Large.xaml 的交互逻辑
    /// </summary>
    public partial class MusicCard_Large : UserControl
    {

        public Song _Song
        {
            get { return (Song)GetValue(_SongProperty); }
            set { SetValue(_SongProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _Song.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _SongProperty =
            DependencyProperty.Register("_Song", typeof(Song), typeof(MusicCard_Large), new PropertyMetadata(new Song()));

        public MusicCard_Large()
        {
            InitializeComponent();

            Cursor = Cursors.Hand;
        }

        public MusicCard_Large(Song song)
        {
            InitializeComponent();

            Cover.Source = BitmapFrame.Create(new Uri(song.Pic + "@200w_200h_1c"));
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

        bool isInlist = false;
        Playlist _playlist = null;
        public MusicCard_Large(Song song, Playlist playlist)
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

using BiliMusic.Controls.Flyout;
using BiliMusic.Models;
using BiliMusic.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BiliMusic.Controls
{
    /// <summary>
    /// PlayingView.xaml 的交互逻辑
    /// </summary>
    public partial class PlayingView : UserControl
    {
        public PlayingView()
        {
            InitializeComponent();

            _Play.Click += _Play_Click;
            _Pause.Click += _Pause_Click;
            _Like.Click += _Like_Click;
            _PreviousSong.Click += _PreviousSong_Click;
            _NextSong.Click += _NextSong_Click;
            _PlayOptionSwitch.Click += _PlayOptionSwitch_Click;
            _Volume.Click += _Volume_Click;
            ViewList.Checked += ViewList_Checked;
            ViewList.Unchecked += ViewList_Unchecked;
            Media.MediaOpened += Media_MediaOpened;
            Media.MediaEnded += Media_MediaEnded;
            Progress.ValueChanged += Progress_ValueChanged;

            ProgressChecker.Interval = TimeSpan.FromSeconds(0.05);
            ProgressChecker.Tick += ProgressChecker_Tick;
        }

        private void ViewList_Unchecked(object sender, RoutedEventArgs e)
        {
            PlayingList.mainWindow._PlayingList.Visibility = Visibility.Collapsed;
            PlayingList.mainWindow.RightBar.Width = new GridLength(0);
        }

        private void ViewList_Checked(object sender, RoutedEventArgs e)
        {
            PlayingList.mainWindow._PlayingList.Visibility = Visibility.Visible;
            PlayingList.mainWindow.RightBar.Width = new GridLength(260);
            PlayingList.mainWindow.RefreshPlayingMusic();
        }

        private void _Volume_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase @base = new FlyoutBase(new ChangeVolume(), 80, 290);
            @base.Show();
        }

        private void Media_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (playOption == PlayOption.Repeat)
                PlayingList.PlayNextSong();
            else if (playOption == PlayOption.RepeatOne)
                PlayingList.ReplaySong();
            else if (playOption == PlayOption.Random)
                PlayingList.PlayRandomSong();
        }

        private void _PlayOptionSwitch_Click(object sender, RoutedEventArgs e)
        {
            switch (playOption)
            {
                case PlayOption.Repeat:
                    _PlayOptionSwitch.Content = "􀊟";
                    playOption = PlayOption.RepeatOne;
                    break;
                case PlayOption.RepeatOne:
                    _PlayOptionSwitch.Content = "􀊝";
                    playOption = PlayOption.Random;
                    break;
                case PlayOption.Random:
                    _PlayOptionSwitch.Content = "􀊞";
                    playOption = PlayOption.Repeat;
                    break;
                default:
                    break;
            }
        }

        private void _NextSong_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.PlayNextSong();
        }

        private void _PreviousSong_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.PlayPerviousSong();
        }

        Song playingS = new Song();
        bool isLike = false;
        private void _Like_Click(object sender, RoutedEventArgs e)
        {
            switch (isLike)
            {
                case true:
                    _Like.Content = "􀊴";
                    Favorite.RemovefromFav(playingS);
                    break;
                case false:
                    _Like.Content = "􀊵";
                    Favorite.AddtoFav(playingS);
                    break;
            }
            isLike = !isLike;
        }

        private void _Pause_Click(object sender, RoutedEventArgs e)
        {
            Media.Pause();
            ChangePlayButtonStyle(PlayState.Paused);
        }

        bool isProgChangeByCode = false;
        private void Progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!isProgChangeByCode)
                Media.Position = TimeSpan.FromSeconds((e.NewValue / 100) * MediaTotalSecond);
            isProgChangeByCode = false;
        }

        private void Media_MediaOpened(object sender, RoutedEventArgs e)
        {
            MediaTotalSecond = Media.NaturalDuration.TimeSpan.TotalSeconds; 
        }

        private async void _Play_Click(object sender, RoutedEventArgs e)
        {
            Media.Play();
            ChangePlayButtonStyle(PlayState.Playing);
        }

        #region ?
        public PlayOption playOption
        {
            get { return (PlayOption)GetValue(playOptionProperty); }
            set { SetValue(playOptionProperty, value); }
        }

        public static readonly DependencyProperty playOptionProperty =
            DependencyProperty.Register("playOption", typeof(PlayOption), typeof(PlayingView), new PropertyMetadata(PlayOption.Repeat));

        DispatcherTimer ProgressChecker = new DispatcherTimer();
        public bool isPlaying
        {
            get { return (bool)GetValue(isPlayingProperty); }
            set { SetValue(isPlayingProperty, value); 
                if (value)
                    ProgressChecker.Start();
                else
                    ProgressChecker.Stop();
            }
        }

        double MediaTotalSecond = 0;
        private void ProgressChecker_Tick(object? sender, EventArgs e)
        {
            isProgChangeByCode = true;
            if (MediaTotalSecond != 0)
                Progress.Value = Media.Position.TotalSeconds / Media.NaturalDuration.TimeSpan.TotalSeconds * 100;
        }

        public static readonly DependencyProperty isPlayingProperty =
            DependencyProperty.Register("isPlaying", typeof(bool), typeof(PlayingView), new PropertyMetadata(false));

        public double Volume
        {
            get { return (double)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); 
                Media.Volume = value;
                if (value == 0)
                    _Volume.Content = "􀊣";
                else if (value > 0 && value <= 0.3)
                    _Volume.Content = "􀊥";
                else if (value > 0.3 && value <= 0.6)
                    _Volume.Content = "􀊧";
                else
                    _Volume.Content = "􀊩";
            }
        }

        // Using a DependencyProperty as the backing store for Volume.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VolumeProperty =
            DependencyProperty.Register("Volume", typeof(double), typeof(PlayingView), new PropertyMetadata(double.Parse("1")));
        #endregion

        public async void Play(Song song)
        {
            SetLikeButtonContnet(false);
            MediaTotalSecond = 0;
            ChangePlayButtonStyle(PlayState.Loading);
            string FilePath = await util_File.GetSongPath(song);
            if (FilePath != null)
            {
                FileInfo fileInfo = new FileInfo(FilePath);
                Media.Source = null;
                if (fileInfo.Exists)
                {
                    Cover.Source = BitmapFrame.Create(new Uri(song.Pic + "@200w_200h_1c"));
                    Title.Text = song.Title;
                    UPName.Text = song.UPName;
                    Media.Source = new Uri(fileInfo.FullName);
                    Media.Play();
                    isPlaying = true;
                    ChangePlayButtonStyle(PlayState.Playing);
                }
                playingS = song;
            }
            else
            {
                List<string> CollectionSongs = await util_File.GetCollectionSongs(song);

                Song? _song = JsonConvert.DeserializeObject<Song>(File.ReadAllText(EnvironmentVariables.SavesPath + "\\" + song.AId + "\\Song.json"));
                if (_song != null)
                {
                    if (CollectionSongs[0] != null)
                    {
                        Cover.Source = BitmapFrame.Create(new Uri(_song.Pic + "@200w_200h_1c"));
                        Title.Text = song.Title;
                        UPName.Text = song.UPName;
                        Media.Source = new Uri(CollectionSongs[0]);
                        Media.Play();
                        isPlaying = true;
                        ChangePlayButtonStyle(PlayState.Playing);
                         playingS = _song;
                    }
                }
            }
            if (Favorite.TryFindinFav(song))
                SetLikeButtonContnet(true);

            History.AddtoHistory(song);
        }

        private void ShowVideoList_Click(object sender, RoutedEventArgs e)
        {
            if (playingS.UPID != null)
                PlayingList.mainWindow.MFrame.Navigate(new Pages.Videolist(new User() { mid = playingS.UPID, Name = playingS.UPName }));
        }

        public void SetLikeButtonContnet(bool _isLike)
        {
            if (_isLike)
            {
                isLike = true;
                _Like.Content = "􀊵";
            }
            else
            {
                isLike = false;
                _Like.Content = "􀊴";
            }
        }

        public void ChangePlayButtonStyle(PlayState ps)
        {
            //ps - play station 🤔
            _Play.Visibility = Visibility.Collapsed;
            _Pause.Visibility = Visibility.Collapsed;
            _Loading.Visibility = Visibility.Collapsed;
            switch (ps)
            {
                case PlayState.Playing:
                    _Pause.Visibility = Visibility.Visible;
                    break;
                case PlayState.Paused:
                    _Play.Visibility = Visibility.Visible;
                    break;
                case PlayState.Loading:
                    _Loading.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
    }

    public enum PlayState
    {
        Playing,
        Paused,
        Loading
    }

    public enum PlayOption
    {
        Repeat,
        RepeatOne,
        Random
    }
}

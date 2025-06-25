using BiliMusic.Controls;
using BiliMusic.Models;
using BiliMusic.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace BiliMusic.Pages
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();

            Loaded += Home_Loaded;
            re_ShowMore.Click += Re_ShowMore_Click;
        }

        private void Re_ShowMore_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.mainWindow.WindowState = WindowState.Minimized;
            RankSongWindow rankSong = new RankSongWindow();
            rankSong.Show();
        }

        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
            Thread getRank = new Thread(new ThreadStart(async () =>
            {
                string FileName = await util_Console.Regex(RankSongMethods.GetToday(), util_Console_Action.RankSong);
                List<RankSong>? rankSongs = JsonConvert.DeserializeObject<List<RankSong>>(File.ReadAllText(FileName));
                for (int i = 0; i < 30; i++)
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        RankSongs.Children.Add(new MusicCard_Large(rankSongs[i]));
                    }));
            }));
            getRank.Start();

            Thread getHistory = new Thread(new ThreadStart(() =>
            {
                List<Song> songs = Models.History.GetAllHistory();
                this.Dispatcher.Invoke(new Action(() =>
                {
                    if (songs.Count <= 30)
                        foreach (var song in songs)
                            _History.Children.Add(new MusicCard_Large(song));
                    else
                        for (int i = 0; i < 20; i++)
                            _History.Children.Add(new MusicCard_Large(songs[i]));
                }));
            }));
            getHistory.Start();

            int _historyC = Models.History.GetAllHistory().Count;
            if (_historyC > 2)
            {
                Random random = new Random();
                Song Rsong = Models.History.GetAllHistory()[random.Next(_historyC)];
                Thread getRecommend = new Thread(new ThreadStart(async () =>
                {
                    string FileName = await util_Console.Regex(RecommendMethods.GetSongs(Rsong), util_Console_Action.Recommends);
                    List<Recommend>? recommends = JsonConvert.DeserializeObject<List<Recommend>>(File.ReadAllText(FileName));
                    for (int i = 0; i < 40; i++)
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            _Recommend.Children.Add(new MusicCard_Large(recommends[i]));
                        }));
                    }
                }));
                getRecommend.Start();
            }
        }
    }
}

using BiliMusic.Controls;
using BiliMusic.Controls.Flyout;
using BiliMusic.Models;
using BiliMusic.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace BiliMusic.Pages
{
    /// <summary>
    /// Videolist.xaml 的交互逻辑
    /// </summary>
    public partial class Videolist : Page
    {
        public Videolist()
        {
            InitializeComponent();
        }

        int pagenum = 1;
        int pagemax = 0;

        string keyword = "";
        Models.User User { get; set; }
        public Videolist(Models.User user)
        {
            InitializeComponent();
            
            User = user;
            _Title.Text = user.Name;
            _Back.Visibility = Visibility.Visible;
            _Search_Following.Visibility = Visibility.Visible;
            LoadUserVideo(user);
            _Next.Click += _Next_Click;
            _Previous.Click += _Previous_Click;
            _Back.Click += _Back_Following_Click;
            _Search_Following.Click += _Search_Following_Click;
        }

        private void LoadUserVideo(Models.User user)
        {
            videos.Children.Clear();
            Mask.Visibility = Visibility.Visible;
            _Loading.Visibility = Visibility.Visible;
            Thread t = new Thread(new ThreadStart(() => {
                string result = util_WebReq.Post(EnvironmentVariables.url_GetVideos + user.mid + "&pn=" + pagenum + "&keywords=" + keyword, util_Cookie.GetCookies());
                int i = int.Parse(util_Regex.Regex_dK(result, "\"total\".*?}", 8));
                pagemax = i / 20;
                double pgmax = Convert.ToDouble(i) / Convert.ToDouble(20);
                List<Song> songs = util_Regex.regexSearchSongs(result);
                foreach (var item in songs)
                    this.Dispatcher.Invoke(new Action(() => {
                        videos.Children.Add(new MusicCard_Small(item));
                    }));
                this.Dispatcher.Invoke(new Action(() => {
                    if (pgmax > pagemax)
                        pagemax++;
                    if (pagemax == 1)
                        _Next.IsEnabled = false;
                    else
                        _Next.IsEnabled = true;
                    Mask.Visibility = Visibility.Collapsed;
                    _Loading.Visibility = Visibility.Collapsed;
                    _Count.Content = pagenum + " / " + pagemax;
                }));
            }));
            t.Start();
        }

        Models.CollectFolder CollectFolder { get; set; }
        public Videolist(Models.CollectFolder collect)
        {
            InitializeComponent();

            CollectFolder = collect;
            _Title.Text = collect.User.Name;
            _Back.Visibility = Visibility.Visible;
            LoadCF(CollectFolder);
            _Next.Click += _Next_Click;
            _Previous.Click += _Previous_Click;
            _Back.Click += _Back_Following_Click;
        }

        private void LoadCF(Models.CollectFolder collect)
        {
            videos.Children.Clear();
            Mask.Visibility = Visibility.Visible;
            _Loading.Visibility = Visibility.Visible;
            Thread t = new Thread(new ThreadStart(() => {
                string result = "";
                if (collect.isMine)
                    result = util_WebReq.Post(EnvironmentVariables.url_GetFavlist_video_mine + collect.ID + "&pn=" + pagenum, util_Cookie.GetCookies());
                else
                    result = util_WebReq.Post(EnvironmentVariables.url_GetFavlist_video_other + collect.ID + "&pn=" + pagenum, util_Cookie.GetCookies());
                pagemax = int.Parse(collect.Count) / 20;
                double pgmax = Convert.ToDouble(int.Parse(collect.Count)) / Convert.ToDouble(20);
                List<Song> songs = util_Regex.regexCollectSongs(result);
                foreach (var item in songs)
                    this.Dispatcher.Invoke(new Action(() => {
                        videos.Children.Add(new MusicCard_Small(item));
                    }));
                this.Dispatcher.Invoke(new Action(() => {
                    if (pgmax > pagemax)
                        pagemax++;
                    if (pagemax == 1)
                        _Next.IsEnabled = false;
                    else
                        _Next.IsEnabled = true;
                    Mask.Visibility = Visibility.Collapsed;
                    _Loading.Visibility = Visibility.Collapsed;
                    _Count.Content = pagenum + " / " + pagemax;
                }));
            }));
            t.Start();
        }

        private void _Previous_Click(object sender, RoutedEventArgs e)
        {
            pagenum--;
            if (pagenum >= 1)
                if (User != null)
                    LoadUserVideo(User);
                else
                    LoadCF(CollectFolder);
            else
            _Previous.IsEnabled = false;
            _Next.IsEnabled = true;
        }

        private void _Next_Click(object sender, RoutedEventArgs e)
        {
            pagenum++;
            if (pagenum <= pagemax)
                if (User != null)
                    LoadUserVideo(User);
                else
                    LoadCF(CollectFolder);
            else
            _Next.IsEnabled = false;
            _Previous.IsEnabled = true;
        }

        private void _Back_Following_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.mainWindow.MFrame.GoBack();
        }

        private void _Search_Following_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase @base = new FlyoutBase(new SearchVideo(), 120, 220);
            @base.Closing += @base_Closing;
            @base.Show();
        }

        private void @base_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            FlyoutBase? @base = sender as FlyoutBase;
            SearchVideo? video = @base._Content as SearchVideo;
            if (video != null)
                keyword = video._KeyWord.Text;
            LoadUserVideo(User);
        }
    }
}

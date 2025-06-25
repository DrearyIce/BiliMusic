using BiliMusic.Controls;
using BiliMusic.Models;
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
    /// History.xaml 的交互逻辑
    /// </summary>
    public partial class History : Page
    {
        public History()
        {
            InitializeComponent();

            Loaded += History_Loaded;
        }

        private void History_Loaded(object sender, RoutedEventArgs e)
        {
            LoadC(Models.History.TodayHistory, HistoryDate.Today);
            LoadC(Models.History.GetWeeklyHistory(), HistoryDate.ThisWeek);
            LoadC(Models.History.GetAllHistory(), HistoryDate.All);
        }

        public void LoadC(List<Song> songs,HistoryDate hd)
        {
            if (songs.Count <= 30)
                foreach (var item in songs)
                    AddMusicCard(item, hd);
            else
                for (int i = 0; i < 30; i++)
                    AddMusicCard(songs[i], hd);
        }

        public void AddMusicCard(Song song, HistoryDate hd)
        {
            switch (hd)
            {
                case HistoryDate.Today:
                    Today.Children.Add(new MusicCard_Large(song));
                    break;
                case HistoryDate.ThisWeek:
                    ThisWeek.Children.Add(new MusicCard_Large(song));
                    break;
                case HistoryDate.All:
                    AllTime.Children.Add(new MusicCard_Large(song));
                    break;
                default:
                    break;
            }
        }
    }

    public enum HistoryDate
    {
        Today,
        ThisWeek,
        All
    }
}

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
    /// Favorite.xaml 的交互逻辑
    /// </summary>
    public partial class Favorite : Page
    {
        public Favorite()
        {
            InitializeComponent();

            Loaded += Favorite_Loaded;
            _PlayAll.Click += _PlayAll_Click;
            _Refresh.Click += _Refresh_Click;
        }

        private void _Refresh_Click(object sender, RoutedEventArgs e)
        {
            Fav.Children.Clear();
            foreach (Song song in Models.Favorite.FavoriteSongs)
                Fav.Children.Add(new BiliMusic.Controls.MusicCard_Small(song));
            Fav.Children.Add(new Border() { Height = 100 });
        }

        private void _PlayAll_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.ReplaceList(Models.Favorite.FavoriteSongs);
        }

        private void Favorite_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Song song in Models.Favorite.FavoriteSongs)
                Fav.Children.Add(new BiliMusic.Controls.MusicCard_Small(song));
            Fav.Children.Add(new Border() { Height = 100 });
        }
    }
}

using AdonisUI.Controls;
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
using System.Windows.Shapes;

namespace BiliMusic.Windows
{
    /// <summary>
    /// AddToPlaylist.xaml 的交互逻辑
    /// </summary>
    public partial class AddToPlaylist : AdonisWindow
    {
        Song _song;
        public AddToPlaylist(Song song)
        {
            InitializeComponent();

            _song = song;
            _Cancel.Click += _Cancel_Click;
            Loaded += AddToPlaylist_Loaded;
        }

        private void AddToPlaylist_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in Models.PlayLists.Playlists)
            {
                var pc = new PlaylistCard_Small(item, _song);
                pc.PreviewMouseUp += AddToPlaylist_PreviewMouseUp;
                playlists.Children.Add(pc);
            }
        }

        private void AddToPlaylist_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void _Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

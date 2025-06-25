using BiliMusic.Controls;
using BiliMusic.Controls.Flyout;
using BiliMusic.Models;
using BiliMusic.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Playlists.xaml 的交互逻辑
    /// </summary>
    public partial class Playlists : Page
    {
        public Playlists()
        {
            InitializeComponent();

            _NewList.Click += _NewList_Click;
            Loaded += Playlists_Loaded;
        }

        private void _NewList_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase flyoutBase = new FlyoutBase(new NewPlaylist(), 170, 240);
            flyoutBase.Show();
        }

        private void Playlists_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in Models.PlayLists.Playlists)
                playlists.Children.Add(new PlaylistCard_Small(item));

            Thread thread = new(new ThreadStart(() => {
                List<CollectFolder> cf_m = Models.CollectFolder_Method.GetMineCollectFolder(new Models.User() { mid = Models.Account.mid });
                List<CollectFolder> cf_o = Models.CollectFolder_Method.GetOtherCollectFolder(new Models.User() { mid = Models.Account.mid });
                foreach (var item in cf_m)
                {
                    this.Dispatcher.Invoke(new Action(() => {
                        Myplaylist.Children.Add(new CollectFolder_Small(item));
                    }));
                }
                foreach (var item in cf_o)
                {
                    this.Dispatcher.Invoke(new Action(() => {
                        Othersplaylist.Children.Add(new CollectFolder_Small(item));
                    }));
                }
            }));
            thread.Start();
        }
    }
}

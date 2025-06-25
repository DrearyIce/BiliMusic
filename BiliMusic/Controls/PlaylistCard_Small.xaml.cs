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
    /// PlaylistCard_Small.xaml 的交互逻辑
    /// </summary>
    public partial class PlaylistCard_Small : UserControl
    {
        public PlaylistCard_Small()
        {
            InitializeComponent();
        }

        Models.Playlist _playlist = null;
        public PlaylistCard_Small(Models.Playlist pl)
        {
            InitializeComponent();

            Cursor = Cursors.Hand;
            _playlist = pl;
            Title.Text = _playlist.Title;
            Desc.Text = _playlist.Description;
            if (Desc.Text == "")
                Desc.Text = "No introduction.";

            MouseUp += PlaylistCard_Small_MouseUp;
            MouseDown += PlaylistCard_Small_MouseDown;
        }

        Song _song ;
        public PlaylistCard_Small(Models.Playlist pl, Song song)
        {
            InitializeComponent();

            Cursor = Cursors.Hand;
            _playlist = pl;
            Title.Text = _playlist.Title;
            Desc.Text = _playlist.Description;
            if (Desc.Text == "")
                Desc.Text = "No description.";

            MouseUp += PlaylistCard_Small_MouseUp1;
            MouseDown += PlaylistCard_Small_MouseDown;
            _song = song;
        }

        private void PlaylistCard_Small_MouseUp1(object sender, MouseButtonEventArgs e)
        {
            if (isDown)
            {
                int index = PlayLists.Playlists.IndexOf(_playlist);
                if (index != -1)
                {
                    PlayLists.Playlists[index].songs.Add(_song);
                    ActionTip actionTip = new ActionTip() { Text = "Addition successful, click Refresh to refresh page." };
                    actionTip.Show();
                }
            }

        }

        bool isDown = false;
        private void PlaylistCard_Small_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDown = true;
        }

        private void PlaylistCard_Small_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDown)
                PlayingList.mainWindow.MFrame.Navigate(new Pages.Playlist(_playlist));
            isDown = false;
        }

        private void More_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase @base = new FlyoutBase(new Playlist_Menu(_playlist), 131, 130);
            @base.Show();
        }
    }
}

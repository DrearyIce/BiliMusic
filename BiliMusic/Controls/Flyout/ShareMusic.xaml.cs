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

namespace BiliMusic.Controls.Flyout
{
    /// <summary>
    /// ShareMusic.xaml 的交互逻辑
    /// </summary>
    public partial class ShareMusic : UserControl
    {
        public ShareMusic(Song song)
        {
            InitializeComponent();

            _Copy.Click += _Copy_Click;
            _Link.Text = "https://www.bilibili.com/video/" + util_BvConverter.Encode(long.Parse(song.AId));
        }

        public ShareMusic(List<Song> song)
        {
            InitializeComponent();

            _Copy.Click += _Copy_Click;
            foreach (var item in song)
                _Link.Text += "https://www.bilibili.com/video/" + util_BvConverter.Encode(long.Parse(item.AId)) + "\r\n";
        }

        private void _Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_Link.Text);
            PlayingList.mainWindow.Activate();
        }
    }
}

using BiliMusic.Models;
using BiliMusic.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
    /// NewPlaylist.xaml 的交互逻辑
    /// </summary>
    public partial class NewPlaylist : UserControl
    {
        public NewPlaylist()
        {
            InitializeComponent();

            _Ok.Click += _Ok_Click;
        }

        private void _Ok_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text != null)
            {
                FileInfo fi = new FileInfo(EnvironmentVariables.PlaylistPath + "\\" + name.Text + ".p_l");
                try
                {
                    Models.Playlist playlist = new Models.Playlist() { Title = name.Text, Description = desc.Text, Path = fi.FullName };
                    using (FileStream fs = File.Create(fi.FullName))
                    {
                        byte[] buffer = new UTF8Encoding(true).GetBytes(JsonConvert.SerializeObject(playlist));
                        fs.Write(buffer);
                        fs.Close();
                        fs.Dispose();
                    }
                    PlayLists.preLoad();
                    PlayingList.mainWindow.MFrame.Refresh();
                    PlayingList.mainWindow.MFrame.Navigate(new Pages.Playlists());
                    PlayingList.mainWindow.Activate();
                }
                catch
                {
                    FlyoutBase flyoutBase = new FlyoutBase("Invalid Name", 26,100);
                    flyoutBase.Show();
                }
            }
        }
    }
}

using AdonisUI.Controls;
using BiliMusic.Controls;
using BiliMusic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// RankSongWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RankSongWindow : AdonisWindow
    {
        public RankSongWindow()
        {
            InitializeComponent();

            Loaded += RankSongWindow_Loaded;
        }

        private void RankSongWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Thread getRank = new Thread(new ThreadStart(async () =>
            {
                string FileName = await util_Console.Regex(RankSongMethods.GetToday(), util_Console_Action.RankSong);
                List<RankSong>? rankSongs = JsonConvert.DeserializeObject<List<RankSong>>(File.ReadAllText(FileName));
                for (int i = 0; i < 100; i++)
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        RankSongs.Children.Add(new MusicCard_Large(rankSongs[i]));
                    }));
            }));
            getRank.Start();
        }
    }
}

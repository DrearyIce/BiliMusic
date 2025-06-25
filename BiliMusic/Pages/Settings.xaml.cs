using BiliMusic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BiliMusic.Pages
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();

            Loaded += Settings_Loaded;
        }

        private void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            if (!BiliMusic.Settings.Default.Theme_IsDark)
                LightMode.IsChecked = true;
            else
                DarkMode.IsChecked = true;
            CacheSize.Children.Add(new TextBlock() { Text = "Saves :" + GetDirectorySize(EnvironmentVariables.SavesPath).ToString() });
            CacheSize.Children.Add(new TextBlock() { Text = "Downloads :" + GetDirectorySize(EnvironmentVariables.DownloadsPath).ToString() });
            CacheSize.Children.Add(new TextBlock() { Text = "History :" + GetDirectorySize(EnvironmentVariables.HistoryPath).ToString() });
            CacheSize.Children.Add(new TextBlock() { Text = "Recommend :" + GetDirectorySize(EnvironmentVariables.RecommendPath).ToString() });
            CacheSize.Children.Add(new TextBlock() { Text = "Other :" + GetDirectorySize(EnvironmentVariables.RankSongPath).ToString() });

            _OpenFolder.Click += _OFolder_Click;
        }

        private void _OFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "/select," + EnvironmentVariables.FavoritePath);
        }

        //https://stackoverflow.org.cn/questions/1118568
        private static long GetDirectorySize(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }

        private void LightMode_Checked(object sender, RoutedEventArgs e)
        {
            PlayingList.mainWindow.IsDark = false;
            BiliMusic.Settings.Default.Theme_IsDark = false;
            BiliMusic.Settings.Default.Save();
        }

        private void DarkMode_Checked(object sender, RoutedEventArgs e)
        {
            PlayingList.mainWindow.IsDark = true;
            BiliMusic.Settings.Default.Theme_IsDark = true;
            BiliMusic.Settings.Default.Save();
        }
    }
}

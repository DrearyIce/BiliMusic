using BiliMusic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using System.Windows.Threading;

namespace BiliMusic.Pages
{
    /// <summary>
    /// Download.xaml 的交互逻辑
    /// </summary>
    public partial class Download : Page
    {
        public Download()
        {
            InitializeComponent();

            _Download.Click += _Download_Click;
            Phone_login.Click += Phone_login_Click;
            TV_login.Click += TV_login_Click;
        }

        private async void TV_login_Click(object sender, RoutedEventArgs e)
        {
            await util_Console.BBDownLogin(true);
        }

        private async void Phone_login_Click(object sender, RoutedEventArgs e)
        {
            await util_Console.BBDownLogin(false);
        }

        private async void _Download_Click(object sender, RoutedEventArgs e)
        {
            //Download Group
            string DownOnly = "";
            if ((bool)r_VideoOnly.IsChecked)
                DownOnly = " --video-only";
            else if ((bool)r_AudioOnly.IsChecked)
                DownOnly = " --audio-only";
            else if ((bool)r_SubtitleOnly.IsChecked)
                DownOnly = " --sub-only";
            else if ((bool)r_DanmakuOnly.IsChecked)
                DownOnly = " --danmaku-only";
            else if ((bool)r_CoverOnly.IsChecked)
                DownOnly = " --cover-only";

            //Skip & util
            string other = "";
            if ((bool)SkipMux.IsChecked)
                other = " --skip-mux";
            if ((bool)SkipSubtitle.IsChecked)
                other = " --skip-subtitle";
            if ((bool)SkipCover.IsChecked)
                other = " --skip-cover";
            if ((bool)SkipAISubtitle.IsChecked)
                other = " --skip-ai";
            if ((bool)UseMP4Box.IsChecked)
                other = " --use-mp4box";

            //Path
            string paths = "";
            if (WorkDir.Text.Length > 3)
                paths += " --work-dir " + WorkDir.Text;
            else
                paths += " --work-dir " + EnvironmentVariables.DownloadsPath;
            paths += " --ffmpeg-path " + EnvironmentVariables.FFMPEGPath;
            if (MP4BoxDir.Text.Length > 3)
                paths += " --mp4box-path " + MP4BoxDir;
            if (Aria2cDir.Text.Length > 3)
                paths += " --aria2c-path " + Aria2cDir.Text;

            //api
            string apiu = "";
            if ((bool)_TVAPI.IsChecked)
                apiu = " -tv";
            else if ((bool)_APPAPI.IsChecked)
                apiu = " -app";
            else if ((bool)_InteAPI.IsChecked)
                apiu = " -intl";

            //videoset
            string video = "";
            if (Encoding.Text.Length > 3)
                video += " -e " + Encoding.Text;
            if (Quality.Text.Length > 3)
                video += " -q " + Quality.Text;
            if (Cookie.Text.Length > 3)
                video += " -c " + Cookie.Text;
            if (AccessToken.Text.Length > 3)
                video += " -token " + AccessToken.Text;
            Mask.Visibility = Visibility.Visible;
            _Downloading.Visibility = Visibility.Visible;
            string args = DownOnly + other + paths + apiu + video;
            string[] strs = Regex.Split(bvs.Text, ";", RegexOptions.IgnoreCase);
            foreach (string str in strs)
            {
                await util_Console.Download(str + args);
            }
            _Downloading.Visibility = Visibility.Collapsed;
            Mask.Visibility = Visibility.Collapsed;
            //Clipboard.SetText(strs[0] + args);
        }
    }
}

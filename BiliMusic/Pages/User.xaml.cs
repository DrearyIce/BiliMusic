using BiliMusic.Models;
using BiliMusic.Windows;
using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Permissions;
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

namespace BiliMusic.Pages
{
    /// <summary>
    /// User.xaml 的交互逻辑
    /// </summary>
    public partial class User : Page
    {
        public User()
        {
            InitializeComponent();

            Loaded += User_Loaded;
        }

        private void User_Loaded(object sender, RoutedEventArgs e)
        {
            FileInfo cookieF = new FileInfo(EnvironmentVariables.CookiesPath);
            if (cookieF.Exists && File.ReadAllText(cookieF.FullName) != "")
            {
                _Login.IsEnabled = false;
                _SignOut.IsEnabled = true;
                List<Cookie>? cookies = JsonConvert.DeserializeObject<List<Cookie>>(File.ReadAllText(cookieF.FullName));
                if (cookies != null)
                {
                    string p = util_WebReq.Post(EnvironmentVariables.url_UserInfo, cookies);
                    _img.Source = BitmapFrame.Create(new Uri(util_Regex.Regex_dY(p, "\"face\":\".*?\"", 8) + "@80w_80h_1c"));
                    _name.Text = util_Regex.Regex_dY(p, "\"uname\":\".*?\"", 9);
                    _mid.Text = util_Regex.Regex_dD(p, "\"mid\".*?,", 6);
                }
            }
        }

        private void _Login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = Account.Login();
            login.Closed += Login_Closed; ;
            login.ShowDialog();
        }

        private void Login_Closed(object? sender, EventArgs e)
        {
            PlayingList.mainWindow.MFrame.Refresh();
            PlayingList.mainWindow.MFrame.Navigate(new User());
        }

        private void _SignOut_Click(object sender, RoutedEventArgs e)
        {
            util_File.DeleteFile(EnvironmentVariables.CookiesPath);
            PlayingList.mainWindow.MFrame.Refresh();
            PlayingList.mainWindow.MFrame.Navigate(new User());
        }
    }
}

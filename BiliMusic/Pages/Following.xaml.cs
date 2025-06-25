using BiliMusic.Controls;
using BiliMusic.Models;
using System;
using System.Collections.Generic;
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

namespace BiliMusic.Pages
{
    /// <summary>
    /// Following.xaml 的交互逻辑
    /// </summary>
    public partial class Following : Page
    {
        public Following()
        {
            InitializeComponent();

            Loaded += Following_Loaded;
            _Next.Click += _Next_Click;
            _Previous.Click += _Previous_Click;
        }

        int pagenum = 1;
        int pagemax = 0;
        private void Following_Loaded(object sender, RoutedEventArgs e)
        {
            Mask.Visibility = Visibility.Visible;
            _Loading.Visibility = Visibility.Visible;
            LoadFollowing();
        }

        private void LoadFollowing()
        {
            ups.Children.Clear();
            Thread t = new Thread(new ThreadStart(() => {
                if (util_Cookie.GetCookies() != null)
                {
                    string following = util_WebReq.Post(EnvironmentVariables.url_GetFollowing + Models.Account.mid + "&pn=" + pagenum, util_Cookie.GetCookies());
                    int i = int.Parse(util_Regex.Regex_dK(following, "\"total\".*?}", 8));
                    pagemax = i / 20;
                    double pgmax = Convert.ToDouble(i) / Convert.ToDouble(20);
                    List<Models.User> users = util_Regex.GetFollowingData(following);
                    foreach (var item in users)
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            ups.Children.Add(new UserCard_Small(item));
                        }));
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        if (pgmax > pagemax)
                            pagemax++;
                        if (pagemax == 1)
                            _Next.IsEnabled = false;
                        else
                            _Next.IsEnabled = true;
                        Mask.Visibility = Visibility.Collapsed;
                        _Loading.Visibility = Visibility.Collapsed;
                        _Count.Content = pagenum + " / " + pagemax;
                    }));
                }
            }));
            t.Start();
        }


        private void _Previous_Click(object sender, RoutedEventArgs e)
        {
            pagenum--;
            if (pagenum >= 1)
                LoadFollowing();
            else
                _Previous.IsEnabled = false;
            _Next.IsEnabled = true;
        }

        private void _Next_Click(object sender, RoutedEventArgs e)
        {
            pagenum++;
            if (pagenum <= pagemax)
                LoadFollowing();
            else
                _Next.IsEnabled = false;
            _Previous.IsEnabled = true;
        }
    }
}

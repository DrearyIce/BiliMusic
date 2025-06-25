using BiliMusic.Controls;
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

namespace BiliMusic.Pages
{
    /// <summary>
    /// Search.xaml 的交互逻辑
    /// </summary>
    public partial class Search : Page
    {
        public Search()
        {
            InitializeComponent();

        }

        public void SearchMusic(string str)
        {
            Mask.Visibility = Visibility.Visible;
            _Searching.Visibility = Visibility.Visible;
            _Result.Children.Clear();
            Thread t = new Thread(new ThreadStart(async () => {
                string result = await Models.Search.GetResult(str);

                if (result == null)
                {
                    this.Dispatcher.Invoke(new Action(() => {
                        ActionTip actionTip = new ActionTip() { Text = "Try again after logging in." };
                        actionTip.Show();
                    }));
                }
                else
                    foreach (var item in util_Regex.regexSearchSongs(result))
                        this.Dispatcher.Invoke(new Action(() => {
                            _Result.Children.Add(new MusicCard_Small(item));
                        }));
                this.Dispatcher.Invoke(new Action(() => {
                    Mask.Visibility = Visibility.Collapsed;
                    _Searching.Visibility = Visibility.Collapsed;
                }));
            }));
            t.Start();
        }

        private void _Keyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SearchMusic(_Keyword.Text);
        }

        private void _Search_Click(object sender, RoutedEventArgs e)
        {
            SearchMusic(_Keyword.Text);
        }
    }
}

using BiliMusic.Pages;
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
    /// SearchVideo.xaml 的交互逻辑
    /// </summary>
    public partial class SearchVideo : UserControl
    {
        public SearchVideo()
        {
            InitializeComponent();

            _Search.Click += _Search_Click;
        }

        private void _Search_Click(object sender, RoutedEventArgs e)
        {
            PlayingList.mainWindow.Activate();
        }
    }
}

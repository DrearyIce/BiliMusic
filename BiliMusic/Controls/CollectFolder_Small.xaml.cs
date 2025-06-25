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

namespace BiliMusic.Controls
{
    /// <summary>
    /// CollectFolder_Small.xaml 的交互逻辑
    /// </summary>
    public partial class CollectFolder_Small : UserControl
    {
        public CollectFolder_Small()
        {
            InitializeComponent();
        }

        CollectFolder Collect { get; set; }
        public CollectFolder_Small(CollectFolder collect)
        {
            InitializeComponent();

            Cover.Source = BitmapFrame.Create(new Uri(collect.Cover + "@64w_64h_1c"));
            Title.Text = collect.Title;
            Upname.Text = collect.User.Name;
            Collect = collect;
            Cursor = Cursors.Hand;
            MouseDown += CollectFolder_Small_MouseDown;
            MouseUp += CollectFolder_Small_MouseUp;
        }

        private void CollectFolder_Small_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDown)
                PlayingList.mainWindow.MFrame.Navigate(new Pages.Videolist(Collect));
        }

        bool isDown = false;
        private void CollectFolder_Small_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDown = true;
        }
    }
}

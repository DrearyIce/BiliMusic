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
using System.Xml.Linq;

namespace BiliMusic.Controls
{
    /// <summary>
    /// UserCard_Small.xaml 的交互逻辑
    /// </summary>
    public partial class UserCard_Small : UserControl
    {
        User User { get; set; }
        public UserCard_Small(User u)
        {
            InitializeComponent();

            User = u;
            Pic.Source = BitmapFrame.Create(new Uri(User.Pic + "@64w_64h_1c"));
            Name.Text = User.Name;
            if (Name.Text == "")
                Name.Text = "这个人没有填简介啊~~~";
            Desc.Text = User.Sign;
            Cursor = Cursors.Hand;

            MouseUp += UserCard_Small_MouseUp;
            MouseDown += UserCard_Small_MouseDown;
        }

        bool isDown = false;
        private void UserCard_Small_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDown = true;
        }

        private void UserCard_Small_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDown)
                PlayingList.mainWindow.MFrame.Navigate(new Pages.Videolist(User));
        }
    }
}

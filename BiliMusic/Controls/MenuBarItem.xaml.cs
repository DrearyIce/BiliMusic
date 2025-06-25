using System.Windows;
using System.Windows.Controls;

namespace BiliMusic.Controls
{
    /// <summary>
    /// MenuBarItem.xaml 的交互逻辑
    /// </summary>
    public partial class MenuBarItem : UserControl
    {
        public MenuBarItem()
        {
            InitializeComponent();
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(MenuBarItem), new PropertyMetadata(""));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(MenuBarItem), new PropertyMetadata(""));

        public bool ShowAll
        {
            get { return (bool)GetValue(ShowAllProperty); }
            set { SetValue(ShowAllProperty, value); 
                if (value)
                    _Text.Visibility = Visibility.Visible;
                else
                    _Text.Visibility = Visibility.Collapsed;
            }
        }

        // Using a DependencyProperty as the backing store for ShowAll.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowAllProperty =
            DependencyProperty.Register("ShowAll", typeof(bool), typeof(MenuBarItem), new PropertyMetadata(true));
    }
}

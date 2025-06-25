using AdonisUI;
using AdonisUI.Controls;
using BiliMusic.Controls;
using BiliMusic.Controls.Flyout;
using BiliMusic.Pages;
using BiliMusic.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace BiliMusic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            MainMenu.SelectionChanged += MainMenu_SelectionChanged;
            SubMenu.SelectionChanged += SubMenu_SelectionChanged;
            UserMenu.SelectionChanged += UserMenu_SelectionChanged;
            _ClearPlayingList.Click += _ClearPlayingList_Click;
            _Add.Click += _Add_Click;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUserName();
            IsDark = Settings.Default.Theme_IsDark;
            ChangeTheme(!IsDark);
        }

        public bool IsDark
        {
            get => (bool)GetValue(IsDarkProperty);
            set => SetValue(IsDarkProperty, value);
        }

        public static readonly DependencyProperty IsDarkProperty = DependencyProperty.Register("IsDark", typeof(bool), typeof(MainWindow), new PropertyMetadata(false, OnIsDarkChanged));

        private static void OnIsDarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MainWindow)d).ChangeTheme((bool)e.OldValue);
        }

        private void ChangeTheme(bool oldValue)
        {
            ResourceLocator.SetColorScheme(Application.Current.Resources, oldValue ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme);
        }

        public void LoadUserName()
        {
            try
            {
                string userName = Models.Account.GetUserName();
                if (userName != null)
                    _account.Header = userName;
            }
            catch { }
        }

        private void _Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FlyoutBase @base = new FlyoutBase(new AddMusic(true), 140, 300);
            @base.Show();
        }

        private void _ClearPlayingList_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PlayingList.ClearList();
        }

        private void MainMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MainMenu.SelectedIndex != -1)
            {
                ListBoxItem? menuItem = MainMenu.SelectedItem as ListBoxItem;
                MenuBarItem? barItem = menuItem.Content as MenuBarItem;
                PageName.Text = barItem.Header;

                switch (MainMenu.SelectedIndex)
                {
                    case 0:
                        MFrame.Navigate(new Home());
                        break;
                    case 1:
                        MFrame.Navigate(new Download());
                        break;
                    case 2:
                        MFrame.Navigate(new Favorite());
                        break;
                    case 3:
                        MFrame.Navigate(new History());
                        break;
                    case 4:
                        MFrame.Navigate(new Following());
                        break;
                    case 5:
                        MFrame.Navigate(new Playlists());
                        break;
                    default:
                        break;
                }

                SubMenu.SelectedIndex = -1;
                UserMenu.SelectedIndex = -1;
            }
        }

        private void UserMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserMenu.SelectedIndex != -1)
            {
                ListBoxItem? menuItem = UserMenu.SelectedItem as ListBoxItem;
                MenuBarItem? barItem = menuItem.Content as MenuBarItem;
                PageName.Text = barItem.Header;

                switch (UserMenu.SelectedIndex)
                {
                    case 0:
                        MFrame.Navigate(new User());
                        break;
                    case 1:
                        MFrame.Navigate(new Search());
                        break;
                    default:
                        break;
                }

                SubMenu.SelectedIndex = -1;
                MainMenu.SelectedIndex = -1;
            }
        }

        private void SubMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SubMenu.SelectedIndex != -1)
            {
                ListBoxItem? menuItem = SubMenu.SelectedItem as ListBoxItem;
                MenuBarItem? barItem = menuItem.Content as MenuBarItem;
                PageName.Text = barItem.Header;

                switch (SubMenu.SelectedIndex)
                {
                    case 0:
                        MFrame.Navigate(new Pages.Settings());
                        break;
                    case 1:
                        MFrame.Navigate(new About());
                        break;
                    default:
                        break;
                }

                MainMenu.SelectedIndex = -1;
                UserMenu.SelectedIndex = -1;
            }
        }

        private void GridSplitter_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (LeftBar.Width.Value <= 170)
            {
                _Logo.Margin = new System.Windows.Thickness(10, 0, 10, 0);
                _LogoText.Visibility = System.Windows.Visibility.Collapsed;
                SetListboxItemStyle(false);
            }
            else if (LeftBar.Width.Value > 170)
            {
                _Logo.Margin = new System.Windows.Thickness(30, 0, 30, 0);
                _LogoText.Visibility = System.Windows.Visibility.Visible;
                SetListboxItemStyle(true);
            }
        }

        private void SetListboxItemStyle(bool Showall)
        {
            foreach (ListBoxItem item in MainMenu.Items)
            {
                MenuBarItem? m = item.Content as MenuBarItem;
                if (m != null)
                    m.ShowAll = Showall;
            }
            foreach (ListBoxItem item in SubMenu.Items)
            {
                MenuBarItem? m = item.Content as MenuBarItem;
                if (m != null)
                    m.ShowAll = Showall;
            }
            foreach (ListBoxItem item in UserMenu.Items)
            {
                MenuBarItem? m = item.Content as MenuBarItem;
                if (m != null)
                    m.ShowAll = Showall;
            }
        }

        private void GridSplitter_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (LeftBar.Width.Value <= 170)
                LeftBar.Width = new System.Windows.GridLength(90);
        }

        public void RefreshPlayingMusic()
        {
            if (_PlayingList.Visibility == System.Windows.Visibility.Visible)
            {
                _Playinglist.Children.Clear();
                foreach (var item in PlayingList.PlayList)
                    _Playinglist.Children.Add(new MusicCard_Small(item, true));
            }
        }
    }
}
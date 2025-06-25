using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BiliMusic.Windows
{
    /// <summary>
    /// ActionTip.xaml 的交互逻辑
    /// </summary>
    public partial class ActionTip : Window
    {
        public ActionTip()
        {
            InitializeComponent();

            Loaded += ActionTip_Loaded;
        }

        private void Dt_Tick(object? sender, EventArgs e)
        {
            Close();
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Stop();
        }

        private void ActionTip_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(3);
            dt.Tick += Dt_Tick;
            dt.Start();
            BeginStoryboard((Storyboard)TryFindResource("Show"));
            System.Windows.Point p = GetMousePosition();
            Left = p.X / GetDpiRatio() - ActualWidth / 2;
            Top = p.Y / GetDpiRatio() - ActualHeight / 2;
            if (Top < 0)
                Top = 0;
            if (Left < 0)
                Left = 0;
        }

        public static double GetDpiRatio(Window window)
        {
            var currentGraphics = Graphics.FromHwnd(new WindowInteropHelper(window).Handle);
            return currentGraphics.DpiX / 96;
        }

        public static double GetDpiRatio()
        {
            return GetDpiRatio(Application.Current.MainWindow);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static System.Windows.Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            return new System.Windows.Point(w32Mouse.X, w32Mouse.Y);
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ActionTip), new PropertyMetadata(""));

    }
}

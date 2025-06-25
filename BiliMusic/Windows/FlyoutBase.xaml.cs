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

namespace BiliMusic.Windows
{
    /// <summary>
    /// FlyoutBase.xaml 的交互逻辑
    /// </summary>
    public partial class FlyoutBase : Window
    {
        public FlyoutBase()
        {
            InitializeComponent();

            Loaded += FlyoutBase_Loaded;
            Deactivated += FlyoutBase_Deactivated;
        }

        public FlyoutBase(object Content, double _h, double _w)
        {
            InitializeComponent();

            ConP.Height = _h;
            ConP.Width = _w;
            Height = _h + 50;
            Width = _w + 40;
            Loaded += FlyoutBase_Loaded;
            Deactivated += FlyoutBase_Deactivated;
            _Content = Content;
        }

        public object _Content
        {
            get { return (object)GetValue(_ContentProperty); }
            set { SetValue(_ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _ContentProperty =
            DependencyProperty.Register("_Content", typeof(object), typeof(FlyoutBase), new PropertyMetadata(null));

        private void FlyoutBase_Deactivated(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void FlyoutBase_Loaded(object sender, RoutedEventArgs e)
        {
            BeginStoryboard((Storyboard)TryFindResource("Show"));
            System.Windows.Point p = GetMousePosition();
            Left = p.X / GetDpiRatio() - ActualWidth / 2;
            Top = p.Y / GetDpiRatio() - ActualHeight;
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
    }
}

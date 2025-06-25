using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BiliMusic.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:BiliMusic.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:BiliMusic.Controls;assembly=BiliMusic.Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:IScrollViewer/>
    ///
    /// </summary>
    public class IScrollViewer : ScrollViewer
    {
        private double _lastLocation;

        private DispatcherTimer _scrollTemplateTimer = null;
        public IScrollViewer() : base()
        {
            _scrollTemplateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _scrollTemplateTimer.Tick += OnScrollTemplateLoaded;
            _scrollTemplateTimer.Start();
        }

        private void OnScrollTemplateLoaded(object sender, EventArgs e)
        {
            ScrollBar verScrollBar = (ScrollBar)this.Template.FindName("PART_VerticalScrollBar", this);
            if (verScrollBar != null)
            {
                verScrollBar.MouseMove += VerScrollBarMouseMove; ;
                _scrollTemplateTimer.Stop();
                _scrollTemplateTimer = null;
            }
        }

        private void VerScrollBarMouseMove(object sender, MouseEventArgs e)
        {
            _lastLocation = this.VerticalOffset;
        }

        public new void ScrollToHome()
        {
            BeginAnimation(ScrollViewerBehavior.VerticalOffsetProperty, null);

            _lastLocation = 0;
            base.ScrollToHome();
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            double wheelChange = e.Delta;
            var newOffset = _lastLocation - wheelChange * 2;
            ScrollToVerticalOffset(_lastLocation);
            if (newOffset < 0)
                newOffset = 0;
            if (newOffset > ScrollableHeight)
                newOffset = ScrollableHeight;

            AnimateScroll(newOffset);
            _lastLocation = newOffset;
            e.Handled = true;
        }

        private void AnimateScroll(double toValue)
        {
            BeginAnimation(ScrollViewerBehavior.VerticalOffsetProperty, null);
            var animation = new DoubleAnimation
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                From = VerticalOffset,
                To = toValue,
                Duration = TimeSpan.FromMilliseconds(400)
            };
            BeginAnimation(ScrollViewerBehavior.VerticalOffsetProperty, animation);
        }
    }

    public static class ScrollViewerBehavior
    {
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.RegisterAttached("HorizontalOffset", typeof(double), typeof(ScrollViewerBehavior),
                new UIPropertyMetadata(0.0, OnHorizontalOffsetChanged));

        public static void SetHorizontalOffset(FrameworkElement target, double value) =>
            target.SetValue(HorizontalOffsetProperty, value);

        public static double GetHorizontalOffset(FrameworkElement target) =>
            (double)target.GetValue(HorizontalOffsetProperty);

        private static void OnHorizontalOffsetChanged(DependencyObject target, DependencyPropertyChangedEventArgs e) =>
            (target as ScrollViewer)?.ScrollToHorizontalOffset((double)e.NewValue);

        public static readonly DependencyProperty VerticalOffsetProperty =
    DependencyProperty.RegisterAttached("VerticalOffset", typeof(double), typeof(ScrollViewerBehavior),
    new UIPropertyMetadata(0.0, OnVerticalOffsetChanged));

        public static void VerticalOffset(FrameworkElement target, double value) =>
            target.SetValue(VerticalOffsetProperty, value);

        public static double GetVerticalOffset(FrameworkElement target) =>
            (double)target.GetValue(VerticalOffsetProperty);

        private static void OnVerticalOffsetChanged(DependencyObject target, DependencyPropertyChangedEventArgs e) =>
            (target as ScrollViewer)?.ScrollToVerticalOffset((double)e.NewValue);

    }
}

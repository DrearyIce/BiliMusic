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
    /// ChangeVolume.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeVolume : UserControl
    {
        public ChangeVolume()
        {
            InitializeComponent();

            Loaded += ChangeVolume_Loaded;
        }

        private void ChangeVolume_Loaded(object sender, RoutedEventArgs e)
        {
            _Slider.Value = PlayingList.mainWindow.Playing.Volume * 100;
            _Slider.ValueChanged += _Slider_ValueChanged;
        }

        private void _Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PlayingList.mainWindow.Playing.Volume = _Slider.Value / 100;
        }
    }
}

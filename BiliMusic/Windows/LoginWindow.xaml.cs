using BiliMusic.Models;
using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BiliMusic.Windows
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        string url { get; set; }
        string prk { get; set; }

        public LoginWindow(string _url,string _prk)
        {
            InitializeComponent();

            Loaded += LoginWindow_Loaded;
            url = _url; prk = _prk;    
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData data = qRCodeGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Default);
            QRCode qRCode = new QRCode(data);
            QRCode.Source = ToBitmapSource(qRCode.GetGraphic(5));
            Thread thread = new Thread(new ThreadStart(async () =>
            {
                int Count = 0;
                while (true)
                {
                    try
                    {
                        List<Cookie> cookies = await util_WebReq.LoginGetCookieAsync(EnvironmentVariables.url_LoginState + prk);
                        if (cookies.Count > 0)
                        {
                            FileInfo cookieF = new FileInfo(EnvironmentVariables.CookiesPath);
                            if (cookieF.Exists)
                                util_File.DeleteFile(cookieF.FullName);
                            util_File.CreateWriteFile(JsonConvert.SerializeObject(cookies), EnvironmentVariables.CookiesPath);
                            Dispatcher.Invoke(new Action(() => { Close(); PlayingList.mainWindow.LoadUserName(); }));
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    Count++;
                    if (Count >= 100)
                    {
                        Dispatcher.Invoke(new Action(() => { Close(); }));
                        break;
                    }
                    Thread.Sleep(3000);
                }
            }));
            thread.Start();
        }

        private ImageSource ToBitmapSource(Bitmap bitmap)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Bmp);
            stream.Position = 0;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            return bitmapImage;
        }

    }
}

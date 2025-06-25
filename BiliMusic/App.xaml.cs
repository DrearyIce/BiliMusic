using BiliMusic.Models;
using System.Windows;

namespace BiliMusic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            PlayingList.pre_Main(mw);
            History.LoadHistory();
            Favorite.LoadFav();
            PlayLists.preLoad();
            Account.pre_account();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Favorite.SaveFav();
            History.SaveHistory();
            PlayLists.SavePlaylists();
            BiliMusic.Settings.Default.Save();
        }
    }

}

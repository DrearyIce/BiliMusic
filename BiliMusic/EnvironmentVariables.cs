using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BiliMusic
{
    public static class EnvironmentVariables
    {
        #region Path
        public static string AppPath = System.Environment.CurrentDirectory;
        public static string RankSongPath = AppPath + "\\Data\\RankSong";
        public static string SavesPath = AppPath + "\\Data\\Saves";
        public static string DownloadsPath = AppPath + "\\Data\\Downloads";
        public static string FFMPEGPath = AppPath + "\\Data\\Utils";
        public static string BBDownPath = AppPath + "\\Data\\Utils";
        public static string FavoritePath = AppPath + "\\Data\\Favorite.json";
        public static string HistoryPath = AppPath + "\\Data\\History";
        public static string RecommendPath = AppPath + "\\Data\\Recommends";
        public static string PlaylistPath = AppPath + "\\Data\\Playlists";
        public static string CookiesPath = AppPath + "\\Data\\Cookies.cki";
        #endregion

        #region Url
        public static string url_RankSong = "https://api.bilibili.com/x/web-interface/ranking/v2?rid=1003";
        public static string url_WebView = "https://api.bilibili.com/x/web-interface/view?aid=";
        public static string url_OnlineListeners = "https://api.bilibili.com/x/player/online/total?aid=";
        public static string url_Recommend = "https://api.bilibili.com/x/web-interface/archive/related?aid=";
        public static string url_VideoInformation = "https://api.bilibili.com/x/web-interface/wbi/view?aid=";
        public static string url_LoginGenerate = "https://passport.bilibili.com/x/passport-login/web/qrcode/generate";
        public static string url_LoginState = "https://passport.bilibili.com/x/passport-login/web/qrcode/poll?qrcode_key=";
        public static string url_LoginPassport = "https://account.bilibili.com/h5/account-h5/auth/scan-web?qrcode_key=";
        public static string url_UserInfo = "https://api.bilibili.com/x/web-interface/nav";
        public static string url_Search = "https://api.bilibili.com/x/web-interface/wbi/search/all/v2?keyword=";
        public static string url_GetBuvid = "https://api.bilibili.com/x/web-frontend/getbuvid";
        public static string url_GetFollowing = "https://api.bilibili.com/x/relation/followings?ps=20&vmid=";
        public static string url_GetVideos = "https://api.bilibili.com/x/series/recArchivesByKeywords?mid=";
        public static string url_GetFavlist_mine = "https://api.bilibili.com/x/v3/fav/folder/created/list-all?up_mid=";
        public static string url_GetFavlist_others = "https://api.bilibili.com/x/v3/fav/folder/collected/list?pn=1&ps=1000&platform=web&up_mid=";
        public static string url_GetFavlist_info = "https://api.bilibili.com/x/v3/fav/folder/info?media_id=";
        public static string url_GetFavlist_video_mine = "https://api.bilibili.com/x/v3/fav/resource/list?ps=20&media_id=";
        public static string url_GetFavlist_video_other = "https://api.bilibili.com/x/space/fav/season/list?ps=20&season_id=";
        #endregion
    }
}

using BiliMusic.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliMusic.Models
{
    public class CollectFolder
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Count { get; set; }
        public string Cover { get; set; }
        public bool isMine { get; set; }
        public User User { get; set; }
    }

    public static class CollectFolder_Method
    {
        public static List<CollectFolder> GetMineCollectFolder(User user)
        {
            return util_Regex.regexGotCollects_mine(util_WebReq.Post(EnvironmentVariables.url_GetFavlist_mine + user.mid, util_Cookie.GetCookies()));
        }

        public static List<CollectFolder> GetOtherCollectFolder(User user)
        {
            return util_Regex.GetCollectFolderInformations(util_WebReq.Post(EnvironmentVariables.url_GetFavlist_others + user.mid, util_Cookie.GetCookies()), true);
        }
    }
}

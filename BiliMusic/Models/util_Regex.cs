using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace BiliMusic.Models
{
    public static class util_Regex
    {
        public static Song GetVideoInformation(string avid)
        {
            string Data = util_WebReq.Post(EnvironmentVariables.url_VideoInformation + avid);
            //"aid":114658765709270,
            Regex Aid_R = new Regex("\"aid\".*?,");
            MatchCollection Aid = Aid_R.Matches(Data);
            List<string> Aids = new List<string>();
            //"pic":"http://i2.hdslb.com/bfs/archive/538b3ca731bbb279a7b944c1bf60b64f65a8b971.jpg"
            Regex Cover_R = new Regex("\"pic\":\".*?\"");
            MatchCollection Cover = Cover_R.Matches(Data);
            //"face":"https://i0.hdslb.com/bfs/face/57c02f89b264a290d62f88b432de9c887137f130.jpg"
            Regex UPPic_R = new Regex("\"face\":\".*?\"");
            MatchCollection UPPic = UPPic_R.Matches(Data);
            //"title":"莫愁乡--（OfficialMusicVideo）亚细亚旷世奇才"
            Regex Title_R = new Regex("\"title\":\".*?\"");
            MatchCollection Title = Title_R.Matches(Data);
            //"desc":"-"
            Regex Desc_R = new Regex("\"desc\":\".*?\"");
            MatchCollection Desc = Desc_R.Matches(Data);
            //"mid":2010145557,
            Regex UPID_R = new Regex("\"mid\".*?,");
            MatchCollection UPID = UPID_R.Matches(Data);
            //"name":"亚细亚旷世奇才"
            Regex UPname_R = new Regex("\"name\":\".*?\"");
            MatchCollection UPName = UPname_R.Matches(Data);

            string aid = DelD(Aid[0].Value, 6);
            string cover = DelY(Cover[0].Value, 7);
            string uppic = DelY(UPPic[0].Value, 8);
            string title = DelY(Title[0].Value, 9);
            string desc = DelY(Desc[0].Value, 8);
            string upid = DelD(UPID[0].Value, 6);
            string upname = DelY(UPName[0].Value, 8);

            title.Replace("“", "");
            title.Replace("”", "");
            title.Replace("\"", "");

            Song s= new Song()
            {
                AId = aid,
                Pic = cover,
                UPPic = uppic,
                Title = title,
                UPID = upid,
                UPName = upname,
                Description = desc
            };

            return s;
        }

        public static List<CollectFolder> GetCollectFolderInformations(string str, bool isCreatedbyothers)
        {
            string Data = "";
            if (isCreatedbyothers)
                Data = str;
            else
                Data = util_WebReq.Post(EnvironmentVariables.url_GetFavlist_info + str, util_Cookie.GetCookies());
            //"id":114658765709270,
            Regex ID_R = new Regex("\"id\".*?,");
            MatchCollection ID = ID_R.Matches(Data);
            //"cover":"http://i2.hdslb.com/bfs/archive/538b3ca731bbb279a7b944c1bf60b64f65a8b971.jpg"
            Regex Cover_R = new Regex("\"cover\":\".*?\"");
            MatchCollection Cover = Cover_R.Matches(Data);
            //"face":"https://i0.hdslb.com/bfs/face/57c02f89b264a290d62f88b432de9c887137f130.jpg"
            Regex UPPic_R = new Regex("\"face\":\".*?\"");
            MatchCollection UPPic = UPPic_R.Matches(Data);
            //"title":"默认收藏夹"
            Regex Title_R = new Regex("\"title\":\".*?\"");
            MatchCollection Title = Title_R.Matches(Data);
            //"mid":2010145557,
            Regex UPID_R = new Regex("\"mid\".*?,");
            MatchCollection UPID = UPID_R.Matches(Data);
            //"name":"亚细亚旷世奇才"
            Regex UPname_R = new Regex("\"name\":\".*?\"");
            MatchCollection UPName = UPname_R.Matches(Data);
            //"media_count": 34
            Regex mc_R = new Regex("\"media_count\".*?,");
            MatchCollection MC = mc_R.Matches(Data);
            List<CollectFolder> lcf = new List<CollectFolder>();
            int i = 0;
            while (true)
            {
                try
                {
                    string id = DelD(ID[i].Value, 5);
                    string cover = DelY(Cover[i].Value, 9);
                    string uppic = DelY(UPPic[i].Value, 8);
                    string title = DelY(Title[i].Value, 9);
                    string upid = DelD(UPID[i].Value, 6);
                    string upname = DelY(UPName[i].Value, 8);
                    string count = DelD(MC[i].Value, 14);

                    title.Replace("“", "");
                    title.Replace("”", "");
                    title.Replace("\"", "");

                    CollectFolder cf = new CollectFolder()
                    {
                        ID = id,
                        Cover = cover,
                        User = new User() { Pic = uppic, mid = upid, Name = upname },
                        Title = title,
                        Count = count,
                        isMine = !isCreatedbyothers
                    };

                    lcf.Add(cf);
                    if (!isCreatedbyothers)
                        break;
                    i++;
                }
                catch
                {
                    break;
                }
            }

            return lcf;
        }

        public static List<User> GetFollowingData(string Data)
        {
            //"mid":2010145557,
            Regex UPID_R = new Regex("\"mid\".*?,");
            MatchCollection UPID = UPID_R.Matches(Data);
            //"face":"https://i0.hdslb.com/bfs/face/57c02f89b264a290d62f88b432de9c887137f130.jpg"
            Regex UPPic_R = new Regex("\"face\":\".*?\"");
            MatchCollection UPPic = UPPic_R.Matches(Data);
            //"sign":"..."
            Regex UPSign_R = new Regex("\"sign\":\".*?\"");
            MatchCollection UPSign = UPSign_R.Matches(Data);
            //"name":"亚细亚旷世奇才"
            Regex UPname_R = new Regex("\"uname\":\".*?\"");
            MatchCollection UPName = UPname_R.Matches(Data);
            List<User> users = new List<User>();
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    string upname = DelY(UPName[i].Value, 9);
                    string sign = DelY(UPSign[i].Value, 8);
                    string upid = DelD(UPID[i].Value, 6);
                    string face = DelY(UPPic[i].Value, 8);
                    users.Add(new User() { mid = upid, Pic = face, Sign = sign, Name = upname });
                }
                catch
                { continue; }
            }
            return users;
        }

        public static List<Song> regexSearchSongs(string result)
        {
            List<Song> songs = new List<Song>();
            //"aid":114658765709270,
            Regex Aid_R = new Regex("\"aid\".*?,");
            MatchCollection Aid = Aid_R.Matches(result);

            for (int i = 0; i < 20; i++)
            {
                try
                {
                    string aid = DelD(Aid[i].Value, 6);
                    Song s = GetVideoInformation(aid);
                    songs.Add(s);
                }
                catch
                { continue; }
            }

            return songs;
        }

        public static List<Song> regexCollectSongs(string result)
        {
            List<Song> songs = new List<Song>();
            Regex Aid_R = new Regex("\"id\".*?,");
            MatchCollection Aid = Aid_R.Matches(result);

            for (int i = 1; i < 21; i++)
            {
                try
                {
                    string aid = DelD(Aid[i].Value, 5);
                    Song s = GetVideoInformation(aid);
                    songs.Add(s);
                }
                catch
                { continue; }
            }

            return songs;
        }

        public static List<CollectFolder> regexGotCollects_mine(string result)
        {
            List<CollectFolder> cfs = new List<CollectFolder>();
            //"id":114514,
            Regex id_R = new Regex("\"id\".*?,");
            MatchCollection id = id_R.Matches(result);
            for (int i = 0; i < id.Count; i++)
            {
                try
                {
                    string media_id = DelD(id[0].Value, 5);
                    CollectFolder cf = GetCollectFolderInformations(media_id, false)[0];
                    cfs.Add(cf);
                }
                catch
                { break; }
            }
            return cfs;
        }

        public static List<CollectFolder> regexGotCollects_other(User user)
        {
            List<CollectFolder> cf = GetCollectFolderInformations(util_WebReq.Post(EnvironmentVariables.url_GetFavlist_others + user.mid, util_Cookie.GetCookies()), true);
            return cf;
        }

        public static string Regex_dY(string a, string regex, int n)
        {
            Regex reg = new Regex(regex);
            return DelY(reg.Match(a).Value, n);
        }

        public static string Regex_dD(string a, string regex, int n)
        {
            Regex reg = new Regex(regex);
            return DelD(reg.Match(a).Value, n);
        }

        public static string Regex_dK(string a, string regex, int n)
        {
            Regex reg = new Regex(regex);
            return DelK(reg.Match(a).Value, n);
        }

        public static string DelY(string o, int sub)
        {
            string a = o.Substring(sub);
            return a.Replace("\"", "");
        }

        public static string DelD(string o, int sub)
        {
            string a = o.Substring(sub);
            return a.Replace(",", "");
        }

        public static string DelK(string o, int sub)
        {
            string a = o.Substring(sub);
            return a.Replace("}", "");
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Igex.Models
{
    public class RankSong
    {
        public string? AId { get; set; }
        public string? Pic { get; set; }
        public string? UPPic { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? UPID { get; set; }
        public string? UPName { get; set; }
    }

    public static class RankSong_gex
    {
        public static FileInfo gex_Song(string FileName)
        {
            FileInfo info = new FileInfo(FileName + "j");
            if (info.Exists)
                return info;

            string Data = File.ReadAllText(FileName);
            Data = Data.Replace("“", "");
            Data = Data.Replace("”", "");
            List<RankSong> rankSongs = new List<RankSong>();
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
            for (int i = 0; i < 100; i++)
            {
                string aid = DelD(Aid[2 * i].Value, 6);
                string cover = DelY(Cover[i].Value, 7);
                string uppic = DelY(UPPic[i].Value, 8);
                string title = DelY(Title[i].Value, 9);
                string desc = DelY(Desc[i].Value, 8);
                string upid = DelD(UPID[i].Value, 6);
                string upname = DelY(UPName[i].Value, 8);

                title.Replace("“","");
                title.Replace("”", "");
                title.Replace("\"", "");

                rankSongs.Add(new RankSong()
                {
                    AId = aid,
                    Pic = cover,
                    UPPic = uppic,
                    Title = title,
                    UPID = upid,
                    UPName = upname,
                    Description = desc
                });
            }

            return util_File.SaveFile(JsonConvert.SerializeObject(rankSongs), info.FullName);
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
    }
}

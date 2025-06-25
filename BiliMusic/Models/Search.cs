using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BiliMusic.Models
{
    public static class Search
    {
        public static async Task<string> GetResult(string keyword)
        {
            if (util_Cookie.GetCookies() != null)
            {
                string q = await util_WBI.Get();
                List<Cookie> cookies = util_Cookie.GetCookies();
                string buvid  = util_Regex.Regex_dY(util_WebReq.Post(EnvironmentVariables.url_GetBuvid), "\"buvid\":\".*?\"", 9);
                return await util_WebReq.GetData(EnvironmentVariables.url_Search + keyword + "&" + q + "&" + "buvid=" + buvid);
            }
            return null;
        }
    }
}

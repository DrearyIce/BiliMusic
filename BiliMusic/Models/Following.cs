using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BiliMusic.Models
{
    public static  class Following
    {
        public static async Task<string> GetResult(string mid)
        {
            if (util_Cookie.GetCookies() != null)
                return await util_WebReq.GetData(EnvironmentVariables.url_GetFollowing + mid);
            else
                return null;
        }
    }
}

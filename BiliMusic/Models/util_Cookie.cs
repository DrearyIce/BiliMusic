using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BiliMusic.Models
{
    public static class util_Cookie
    {
        public static List<Cookie>? GetCookies()
        {
            FileInfo cookieF = new FileInfo(EnvironmentVariables.CookiesPath);
            if (cookieF.Exists && File.ReadAllText(cookieF.FullName) != "")
            {
                List<Cookie>? cookies = JsonConvert.DeserializeObject<List<Cookie>>(File.ReadAllText(cookieF.FullName));
                return cookies;
            }
            return null;
        }
    }
}

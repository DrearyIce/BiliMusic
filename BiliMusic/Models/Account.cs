using BiliMusic.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BiliMusic.Models
{
    public static class Account
    {
        public static string mid = null;

        public static void pre_account()
        {
            List<Cookie>? cookies = util_Cookie.GetCookies();
            if (cookies != null)
            {
                string p = util_WebReq.Post(EnvironmentVariables.url_UserInfo, cookies);
                mid = util_Regex.Regex_dD(p, "\"mid\".*?,", 6);
            }
        }

        public static string GetUserName()
        {
            List<Cookie>? cookies = util_Cookie.GetCookies();
            if (cookies != null)
            {
                string p = util_WebReq.Post(EnvironmentVariables.url_UserInfo, cookies);
                return util_Regex.Regex_dY(p, "\"uname\":\".*?\"", 9);
            }
            return null;
        }

        public static LoginWindow Login()
        {
            string post = util_WebReq.Post(EnvironmentVariables.url_LoginGenerate);
            Regex key_r = new Regex("\"qrcode_key\":\".*?\"");
            string qrcode_key = util_Regex.DelY(key_r.Match(post).Value, 14);
            LoginWindow login = new LoginWindow(EnvironmentVariables.url_LoginPassport + qrcode_key, qrcode_key);
            return login;
        }
    }
}

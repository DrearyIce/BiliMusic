using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BiliMusic.Models
{
    public static class util_WebReq
    {
        public static string Post(string url)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
                reader.Dispose();
            }
            resp.Dispose();
            stream.Dispose();
            return result;
        }

        public static string Post(string url,List<Cookie> cookies)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.CookieContainer = new CookieContainer();
            foreach (var item in cookies)
                req.CookieContainer.Add(item);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
                reader.Dispose();
            }
            resp.Dispose();
            stream.Dispose();
            return result;
        }

        public static async Task<List<Cookie>> LoginGetCookieAsync(string url)
        {
            CookieContainer cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler();
            handler.CookieContainer = cookieContainer;
            HttpClient client = new HttpClient(handler);
            await client.GetAsync(url);
            List<Cookie> cookies = cookieContainer.GetCookies(new Uri(url)).Cast<Cookie>().ToList();
            return cookies;
        }

        public static async Task<string> GetData(string url)
        {
            List<Cookie>? cookies = util_Cookie.GetCookies();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            httpClient.DefaultRequestHeaders.Referrer = new Uri("https://www.bilibili.com/");
            httpClient.DefaultRequestHeaders.Add("Cookie", cookies.Find(c => c.Name.Contains("SESSDATA")).Value);
            HttpResponseMessage responseMessage = await httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            });
            return await responseMessage.Content.ReadAsStringAsync();
        }
    }
}

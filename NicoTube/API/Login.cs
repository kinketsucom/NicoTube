using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;


namespace NicoTube.API
{
    class Login
    {
        /// ログイン時のセッションID
        public static CookieContainer LoginCookie = new CookieContainer();


        public static CookieContainer LoginToNicovideo(string mail, string passwd)
        {

            var req = (HttpWebRequest)HttpWebRequest.Create("https://secure.nicovideo.jp/secure/login?site=niconico");
            req.Method = "POST";

            var args = new Dictionary<string, string>
                           {
                               {"mail", mail},
                               {"password", passwd}
                           };

            var s = args.Select(x => x.Key + "=" + x.Value).Aggregate((x, y) => x + "&" + y);
            var bs = Encoding.UTF8.GetBytes(s);
            req.ContentLength = bs.Length;
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = LoginCookie;
            req.GetRequestStream().Write(bs, 0, bs.Length);

            string result;
            try
            {
                var resp = (HttpWebResponse)req.GetResponse();
                result = new StreamReader(resp.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception("Error during login proccess.", ex);
            }
            if (result.Contains("wrongPass"))
            {
                throw new Exception("Wrong ID or Password. Login Failed.");
            }


            Console.WriteLine("ログイン中");
            // クッキー確認
            foreach (Cookie c in LoginCookie.GetCookies(new Uri("https://secure.nicovideo.jp/")))
            {
                Console.WriteLine("クッキー名:" + c.Name.ToString());
                Console.WriteLine("値:" + c.Value.ToString());
                Console.WriteLine("ドメイン名:" + c.Domain.ToString());
            }

            return LoginCookie;
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.IO;

namespace NicoTube.API
{
    class NicoVideo
    {
        /// <summary>
        /// ログイン時のセッションID
        /// </summary>
        public static CookieContainer LoginCookie = new CookieContainer();

        /// <summary>
        /// 指定された動画を視聴できるクッキーをセットされたWebClientを取得する.
        /// </summary>
        /// <param name="id">動画ID</param>
        /// <returns>クッキーを使用するCustomWebClientk</returns>
        public static CustomWebClient GetWebClientForFlv(string id, CookieContainer cc)
        {
            var client = new CustomWebClient();
            client.CookieContainer.Add(GetWatchCookie(id,cc));

            return client;
        }

        /// <summary>
        /// 指定された動画の動画視聴ページのクッキーを取得する.
        /// </summary>
        /// <param name="id">動画ID</param>
        /// <returns>動画視聴ページのクッキー</returns>
        private static CookieCollection GetWatchCookie(string id, CookieContainer cc)
        {
            var req = (HttpWebRequest)HttpWebRequest.Create(String.Format("http://www.nicovideo.jp/watch/{0}", id));
            req.CookieContainer = cc;

            HttpWebResponse resp;
            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving watch cookie.", ex);
            }

            return resp.Cookies;
        }

        public static string GetComment(string url, string thread,CookieContainer cc)
        {

            url += "thread?version=20090904" +
                "&thread=" + thread +
                "&res_from=-1000";

            url = HttpUtility.UrlDecode(url);

            var req = (HttpWebRequest)HttpWebRequest.Create(String.Format(url));
            req.CookieContainer = cc;
            StreamReader sr;

            try
            {
                WebResponse resp = req.GetResponse();
                sr = new StreamReader(resp.GetResponseStream());
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving FlvInfo.", ex);
            }
            Console.WriteLine("コメント："+url);
            string result_comment = sr.ReadToEnd();
            return result_comment;
        }
    }
}

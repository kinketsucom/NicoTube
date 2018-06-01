using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

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
        public static CustomWebClient GetWebClientForFlv(string id)
        {
            var client = new CustomWebClient();
            client.CookieContainer.Add(GetWatchCookie(id));

            return client;
        }

        /// <summary>
        /// 指定された動画の動画視聴ページのクッキーを取得する.
        /// </summary>
        /// <param name="id">動画ID</param>
        /// <returns>動画視聴ページのクッキー</returns>
        private static CookieCollection GetWatchCookie(string id)
        {
            var req = (HttpWebRequest)HttpWebRequest.Create(String.Format("http://www.nicovideo.jp/watch/{0}", id));
            req.CookieContainer = LoginCookie;

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
    }
}

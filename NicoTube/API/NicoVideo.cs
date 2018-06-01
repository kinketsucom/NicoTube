using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicoTube.API
{
    class NicoVideo
    {
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
    }
}

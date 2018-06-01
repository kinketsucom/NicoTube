using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NicoTube.API
{
    class CustomWebClient: WebClient
    {
        /// <summary>
        /// クッキーを用いる、WebClientの継承クラス
        /// </summary>
        /// 
        /// <summary>
        /// このWebClientでのリクエストで用いるクッキー
        /// </summary>
        public CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// CookieContainerを初期化するためコンストラクタをオーバーライド
        /// </summary>
        public CustomWebClient()
        {
            CookieContainer = new CookieContainer();
        }

        /// <summary>
        /// クッキーを利用したリクエストをさせるため、オーバーライド
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = CookieContainer;
            }
            return request;
        }
       
    }
}

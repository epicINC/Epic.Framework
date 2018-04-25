using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Epic.Net
{
    public class HttpWebClient : WebClient
    {
        CookieContainer cookieContainer;

        public HttpWebClient()
            : this(new CookieContainer())
        {
        }

        public HttpWebClient(CookieContainer cookies)
        {
            this.cookieContainer = cookies;
        }

        public CookieContainer Cookies
        {
            get { return this.cookieContainer; }
            set { this.cookieContainer = value; }
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                var httpRequest = request as HttpWebRequest;
                httpRequest.ServicePoint.Expect100Continue = false;
                httpRequest.CookieContainer = cookieContainer;
            }
            return request;
        }
    }
}

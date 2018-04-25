using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components.RSS
{
    /// <summary>
    /// 定义支持 RSS Cloud 接口的 Web Service
    /// 支持 HTTP-POST, XML-RPC or SOAP 1.1
    /// 参看 RssCloud API http://www.rssboard.org/rsscloud-interface
    /// </summary>
    public class RSSCloud
    {
        public string domain
        {
            get;
            set;
        }

        public string port
        {
            get;
            set;
        }

        public string path
        {
            get;
            set;
        }

        public string registerProcedure
        {
            get;
            set;
        }

        public string protocol
        {
            get;
            set;
        }
    }
}

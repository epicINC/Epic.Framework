using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Epic.Web.Http
{

    /// <summary>
    /// 居于 IIS 的 长连接实现
    /// </summary>
    public class ServerPushHandler : IHttpAsyncHandler
    {
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            return new ServerPushResult(context, cb, extraData);
        }

        public void EndProcessRequest(IAsyncResult result)
        {

        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}

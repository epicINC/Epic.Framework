using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace Epic.Web.Http
{
    internal class ServerPushResult : IAsyncResult
    {
        HttpContext context;
        AsyncCallback cb;
        object extraData;

        bool isCompleted;

        internal ServerPushResult(HttpContext context, AsyncCallback cb, object extraData)
        {
            this.context = context;
            this.cb = cb;
            this.extraData = extraData;
            Send();
        }


        public void Send()
        {


            this.context.Response.Write(DateTime.Now);

            this.isCompleted = true;

            if (this.cb != null) this.cb(this);



        }

        public object AsyncState
        {
            get { return null; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { return null; }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted
        {
            get { return this.isCompleted; }
        }
    }
}

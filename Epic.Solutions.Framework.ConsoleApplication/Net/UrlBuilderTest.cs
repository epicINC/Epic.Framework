using Epic.Net;
using Epic.Testing;
using Epic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Solutions.Framework.ConsoleApplication.Net
{
    internal class UrlBuilderTest
    {
        public static void Normal()
        {
            CountDown.Watch(() =>
                {
                    var url = new UrlBuilder("http://qq.jd.com/new/qq/callback.action?a=b");

                    url.QueryString.Add("client_id", Guid.NewGuid().ToString("N"));
                    url.QueryString.Add("client_id", Guid.NewGuid().ToString("N"));

                    Console.WriteLine(url.ToString());
                });
        }

        public static void TypeTest()
        {
            CountDown.Watch(() =>
            {
                var url = new UrlBuilder("http://qq.jd.com/new/qq/callback.action?a=b");

                url.AddQueryString(new { Double = DateTime.Now.Subtract(DateTimeUtility.Unix.Zero).TotalSeconds });

                Console.WriteLine(url.ToString());
            });
        }

    }
}

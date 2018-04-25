using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Epic.Net
{
    public static class DownloadHeader
    {
 
        static void Accept(WebRequest request)
        {
            request.Headers.Add(HttpRequestHeader.Accept, "*/*");
        }

        static void ZhCN(WebRequest request)
        {
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-cn");
        }

        static void Encoding(WebRequest request)
        {
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
        }

        static void KeepAlive(WebRequest request)
        {
            request.Headers.Add(HttpRequestHeader.Connection, "Keep-Alive");
        }

        static void NoCache(WebRequest request)
        {
            request.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");
        }

        public static void Charset(WebRequest request)
        {
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "GB2312,utf-8;q=0.7,*;q=0.7");
        }


        public static void IE7(WebRequest request)
        {
            request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (MSIE 7.0; Windows NT 5.1)");
        }

        public static void IE8Compatible(WebRequest request)
        {
            request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; Trident/4.0)");
        }

        public static void IE8(WebRequest request)
        {
            request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (MSIE 8.0; Windows NT 5.2; Trident/4.0)");
        }

        public static void Firefox3(WebRequest request)
        {
            request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows; U; Windows NT 5.2; zh-CN; rv:1.9.2.3) Gecko/20100401 Firefox/3.6.3");
        }

        public static void Chrome4(WebRequest request)
        {
            request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.1.249.1045 Safari/532.5");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;
using System.Web;

namespace Epic.Net
{
    /// <summary>
    /// UrlBuilder Uri 生成类, 略做修改
    /// http://www.codeproject.com/Articles/11041/A-useful-UrlBuilder-class
    /// </summary>
    public class UrlBuilder : UriBuilder
    {

        #region Constructor

        public UrlBuilder() : base() { }

        public UrlBuilder(string uri) : base(uri)
        {
            this.ParseQueryString();
        }

        public UrlBuilder(Uri uri) : base(uri)
        {
            this.ParseQueryString();
        }

        public UrlBuilder(string schemeName, string hostName) : base(schemeName, hostName)
        {
        }

        public UrlBuilder(string scheme, string host, int portNumber) : base(scheme, host, portNumber)
        {
        }

        public UrlBuilder(string scheme, string host, int port, string pathValue) : base(scheme, host, port, pathValue)
        {
        }

        public UrlBuilder(string scheme, string host, int port, string path, string extraValue) : base(scheme, host, port, path, extraValue)
        {
        }

        public UrlBuilder(System.Web.UI.Page page) : base(page.Request.Url.AbsoluteUri)
        {
            this.ParseQueryString();
        }


        #endregion




        NameValueCollection queryString;
        // StringDictionary
        public NameValueCollection QueryString
        {
            get
            {
                if (this.queryString != null) return this.queryString;
                return this.queryString = new NameValueCollection();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="removeEmptyEntry">默认 true</param>
        public void AddQueryString<T>(T value, bool removeEmptyEntry = true) where T : class
        {
            this.AddQueryString(Epic.Converter.ObjectConverter.AsDictionary(value), removeEmptyEntry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="removeEmptyEntry">默认 true</param>
        public void AddQueryString<T>(Dictionary<string, T> collection, bool removeEmptyEntry = true)
        {
            if (removeEmptyEntry)
                collection.RemoveEmptyEntry().ForEach(e => this.QueryString.Add(e.Key, e.Value.ToString()));
            else
                collection.ForEach(e => this.QueryString.Add(e.Key, e.Value != null ?  e.Value.ToString() : null));
        }


        internal static string UrlEncode(string value)
        {
            return HttpUtility.UrlEncode(value);
        }


        public string PageName
        {
            get { return this.FindPageName(base.Path); }
            set
            {
                base.Path = String.Concat(SetPageName(base.Path), "/", value);
            }
        }

        string FindPageName(string value)
        {
            return value.Substring(value.LastIndexOf("/") + 1);
        }

        string SetPageName(string value)
        {
            return value.Substring(0, value.LastIndexOf("/"));
        }

        void ParseQueryString()
        {
            var query = base.Query;

            if (String.IsNullOrEmpty(query)) return;

            if (this.queryString != null && this.queryString.Count > 0) this.queryString.Clear();

            this.queryString = System.Web.HttpUtility.ParseQueryString(query);
        }

        void BuildQueryString()
        {
            if (this.queryString == null || this.queryString.Count == 0) return;

            base.Query = String.Join("&", this.queryString.SelectIndex((i) => BuildPair(this.queryString.GetKey(i), this.queryString.GetValues(i))));
        }

        string BuildPair(string key, string[] values)
        {
            if (values == null || values.Length == 0) return key + "=";
            if (values.Length == 1) return key + "=" + UrlEncode(values[0]);

            return this.BuildSubPair(key, values);
        }

        string BuildSubPair(string key, string[] values)
        {
            return String.Join("&", values.Select(e => key + "=" + UrlEncode(e)));
        }




        public void Navigate()
        {
            this.Navigate(true);
        }

        public void Navigate(bool endResponse)
        {
            HttpContext.Current.Response.Redirect(this.ToString(), endResponse);
        }


        public override string ToString()
        {
            this.BuildQueryString();
            return base.ToString();
            //return base.Uri.AbsoluteUri;
        }

    }
}

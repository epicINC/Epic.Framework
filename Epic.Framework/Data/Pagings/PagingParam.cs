using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Epic.Web;
using Epic.Xml.Extensions;

namespace Epic.Data
{
    public sealed class PagingParam : Epic.Xml.XmlConvertibleBase
    {
   

        internal PagingParam() { }


        Dictionary<string, object> dictionary;
        Dictionary<string, object> Dictionary
        {
            get
            {
                if (this.dictionary == null)
                    this.dictionary = new Dictionary<string, object>();
                return this.dictionary;
            }
        }


        public void Add<T>(string key, T value)
        {
            this.Dictionary[key] = value;
        }

        public void Add(HttpParam value)
        {
            if (value == null || value.State != HttpParamStateType.Vaild || !value.IsParam)
                return;

            this.Dictionary[value.Name] = value.GetValueOrOriginal();
        }

        public int AbsolutePage
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public int RecordCount
        {
            get;
            set;
        }

        public int PageCount
        {
            get
            {
                return Convert.ToInt32(Math.Ceiling((double)this.RecordCount / this.PageSize));
            }

        }

        public object this[string key]
        {
            get
            {
                if (this.dictionary == null) return null;
                object result;
                this.dictionary.TryGetValue(key, out result);
                return result;
            }
        }

        public string ToQueryString()
        {
                if (this.dictionary == null || this.dictionary.Count == 0) return String.Empty;
                return String.Join("&", this.dictionary.Select(e => e.Key + "=" + HttpUtility.UrlEncodeUnicode(e.Value.ToString())));
        }

        public override void WriteXml(XmlWriter writer)
        {
            this.WriteXml(writer, "Paging");
        }

        public override void WriteContentXml(XmlWriter writer)
        {
            writer.WriteAttributeString("AbsolutePage", this.AbsolutePage);
            writer.WriteAttributeString("PageSize", this.PageSize);
            writer.WriteAttributeString("RecordCount", this.RecordCount);
            writer.WriteAttributeString("PageCount", this.PageCount);
            writer.WriteAttributeString("QueryString", this.ToQueryString());

            if (this.dictionary != null && this.dictionary.Count > 0)
            {
                Epic.Xml.XmlConvertibleWriter.Write(writer, this.dictionary);
            }



        }
    }
}

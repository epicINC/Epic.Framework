using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Epic.Data.V2.Pagings
{
    public class PageList<T> : List<T>, Epic.Xml.IXmlConvertible
    {
        public PagingParam Paging
        {
            get;
            set;
        }
        public void WriteXml(XmlWriter writer)
        {
            this.WriteXml(writer, typeof(T).Name+"s");
        }

        public void WriteXml(XmlWriter writer, string root)
        {
            writer.WriteStartElement(root);
            this.WriteContentXml(writer);
            writer.WriteEndElement();
        }

        public void WriteContentXml(XmlWriter writer)
        {
            if (this.Paging != null)
                this.Paging.WriteXml(writer);
            Epic.Xml.XmlConvertibleWriter.Write(writer, (System.Collections.IEnumerable)this);
        }
    }
}

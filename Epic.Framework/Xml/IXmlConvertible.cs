using System;
using System.Xml;

namespace Epic.Xml
{
    public interface IXmlConvertible
    {

        
        void WriteXml(XmlWriter writer);
        void WriteXml(XmlWriter writer, string root);
        void WriteContentXml(XmlWriter writer);
    }


    public abstract class XmlConvertibleBase : IXmlConvertible
    {

        public abstract void WriteXml(XmlWriter writer);

        public void WriteXml(XmlWriter writer, string root)
        {
            writer.WriteStartElement(root);
            this.WriteContentXml(writer);
            writer.WriteEndElement();
        }

        public abstract void WriteContentXml(XmlWriter writer);
    }



    public static class XmlConvertibleExt
    {


    }
}

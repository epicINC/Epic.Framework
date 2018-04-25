using System;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

namespace Epic.Xml
{
    public class XmlConvertibleWriter
    {
        public static void Write(XmlWriter writer, IXmlConvertible o, string root = null)
        {
            if (!String.IsNullOrWhiteSpace(root))
                o.WriteXml(writer, root);
            else
                o.WriteXml(writer);
        }

        public static void Write(XmlWriter writer, IEnumerable o, string root = null)
        {
            if (o == null) return;
            var writeRoot = !String.IsNullOrWhiteSpace(root);
            if (writeRoot) writer.WriteStartElement(root);
            foreach (var item in o)
            {
                Write(writer, item, "Item");
            }
            if (writeRoot) writer.WriteEndElement();
        }

        public static void Write(XmlWriter writer, IDictionary o, string root = null)
        {
            if (o == null || o.Count == 0) return;
            var writeRoot = !String.IsNullOrWhiteSpace(root);

            if (writeRoot) writer.WriteStartElement(root);
            foreach (DictionaryEntry item in o)
            {
                writer.WriteStartElement("Item");
                writer.WriteAttributeString("Key", item.Key.ToString());
                Write(writer, item.Value);
                writer.WriteEndElement();
                
            }
            if (writeRoot) writer.WriteEndElement();
        }

        public static void Write(XmlWriter writer, KeyValuePair<string, object> o, string root = null)
        {
            var writeRoot = !String.IsNullOrWhiteSpace(root);
            if (writeRoot) writer.WriteStartElement(root);

            writer.WriteAttributeString("Key", o.Key.ToString());
            Write(writer, o.Value);

            if (writeRoot) writer.WriteEndElement();
        }

        public static void Write(XmlWriter writer, string o, string root = null)
        {
            if (!String.IsNullOrWhiteSpace(root))
                writer.WriteAttributeString(root, o);
            else
                writer.WriteString(o);
        }

        public static void Write(XmlWriter writer, object o, string root = null)
        {
            if (o is IXmlConvertible)
            {
                Write(writer, o as IXmlConvertible, root);
                return;
            }

            if (o is string)
            {
                Write(writer, o as string, root);
                return;
            }

            if (o is int)
            {
                Write(writer, o.ToString(), root);
                return;
            }

            if (o is Enum)
            {
                Write(writer, o.ToString(), root);
                return;
            }

            if (o is DateTime)
            {
                Write(writer, o.ToString(), root);
                return;
            }

            if (o is IDictionary)
            {
                Write(writer, o as IDictionary, root);
                return;
            }

            if (o is IEnumerable)
            {
                Write(writer, o as IEnumerable, root);
                return;
            }

            if (o is KeyValuePair<string, object>)
            {
                Write(writer, (KeyValuePair<string, object>)o, root);
                return;
            }
        }
    }
}

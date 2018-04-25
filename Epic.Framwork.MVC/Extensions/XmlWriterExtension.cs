using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Epic.MVC.Extensions
{
    public static class XmlWriterExtension
    {
        public static void WriteAttributeString(this XmlWriter writer, string name, int value)
        {
            writer.WriteAttributeString(name, value.ToString());
        }

        public static void WriteAttributeString(this XmlWriter writer, string name, DateTime value)
        {
            writer.WriteAttributeString(name, value.ToString());
        }

        public static void WriteAttributeString(this XmlWriter writer, string name, decimal value)
        {
            writer.WriteAttributeString(name, value.ToString());
        }

        public static void WriteAttributeString(this XmlWriter writer, string name, double value)
        {
            writer.WriteAttributeString(name, value.ToString());
        }

        public static void WriteAttributeString(this XmlWriter writer, string name, bool value)
        {
            writer.WriteAttributeString(name, value ? "1" : "0");
        }
        
    }
}

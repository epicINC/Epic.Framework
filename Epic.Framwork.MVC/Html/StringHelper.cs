using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using Epic.MVC;
using System.Web.Mvc;

namespace Epic.MVC.Html
{
    public class StringHelper
    {
        public StringHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
        {
        }



        string ConvertToString(XPathNavigator format)
        {
            var current = format.Select("/*").Current;
            if (current == null)
                return null;
            return current.OuterXml;
        }

        public string splitnode(string value, string separator, XPathNavigator format)
        {
            return split(value, separator, ConvertToString(format));

        }

        public string split(string value, string separator, string format)
        {
            if (String.IsNullOrWhiteSpace(format)) format = "{0}";

            if (value.IndexOf(separator) == -1)
                return String.Format(format, value);

            var array = value.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var i in array)
            {
                sb.AppendFormat(format, i);
            }
            return sb.ToString();
        }

        public XPathNodeIterator split(string value, string separator)
        {
            var array = value.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            sb.Append("<Epic>");
            foreach (string i in array)
            {
                sb.AppendFormat("<Item>{0}</Item>", i.Trim());
            }
            sb.Append("</Epic>");
            return XmlHelper.ParseXPathNavigator(sb.ToString()).Select("/Epic/Item");
        }

        public string Format(string format, object arg0)
        {
            return String.Format(format, arg0);
        }


        public string Format(string format, object arg0, object arg1)
        {
            return String.Format( format, arg0, arg1);

        }
        public string Format(string format, object arg0, object arg1, object arg2)
        {
            return String.Format(format, arg0, arg1, arg2);
        }




    }
}

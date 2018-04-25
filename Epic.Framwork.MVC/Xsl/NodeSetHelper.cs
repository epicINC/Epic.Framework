using System;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Web.Mvc;
using Epic.MVC.Html;

namespace Epic.MVC
{
    public class NodeSetHelper
    {
        public NodeSetHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
        { }
        public System.Xml.XPath.XPathNodeIterator For(int start, int end)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append("<root>");
            for (int i = start; i <= end; i++)
            {
                sb.Append("<i>" + i + "</i>");
            }
            sb.Append("</root>");

            return XmlHelper.ParseXPathNodeIterator(sb.ToString());

        }

    }
}

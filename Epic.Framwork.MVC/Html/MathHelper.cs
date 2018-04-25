using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using Epic.MVC;
using System.Web.Mvc;
namespace Epic.MVC.Html
{
    public class MathHelper
    {
        public MathHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
        {
        }
        public double Round(double d, int decimals)
        {
            return Math.Round(d, decimals);
        }

        public XPathNodeIterator For(int start, int end)
        {
            var sb = new StringBuilder();
            sb.Append("<Epic>");
            for (var i = start; i <= end; i++)
            {
                sb.AppendFormat("<Item>{0}</Item>", i);
            }
            sb.Append("</Epic>");
            return XmlHelper.ParseXPathNavigator(sb.ToString()).Select("/Epic/Item");
        }
    }
}

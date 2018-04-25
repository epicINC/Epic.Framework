using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using Epic.MVC;
using System.Web.Mvc;

namespace Epic.MVC.Html
{
    public class DateTimeHelper
    {
        public DateTimeHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
        {
        }

        public string Format(string s, string format)
        {
            if (String.IsNullOrWhiteSpace(s)) return String.Empty;


            DateTime result;
            if (!DateTime.TryParse(s, out result))
                return String.Empty;
            return result.ToString(format);
        }
    }
}

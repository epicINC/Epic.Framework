using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.IO;

namespace Epic.MVC
{
    public class XslView : XmlView, IViewDataContainer
    {
        public XslView(XslTemplate tpl)
            : base(tpl)
        {
        }



        ViewContext viewContext;

        public ViewDataDictionary ViewData
        {
            get { return viewContext.ViewData; }
            set { throw new NotImplementedException(); }

        }


        public override void Render(ViewContext viewContext, System.IO.TextWriter writer)
        {
            this.viewContext = viewContext;


            var ms = new MemoryStream();
            var xmlWriter = XmlWriter.Create(ms);
            this.RenderXml(viewContext, xmlWriter);
            xmlWriter.Flush();
            ms.Position = 0;
            
            //var xxxxt = new StreamReader(ms).ReadToEnd();

            this.tpl.Transform(viewContext, this, ms);
        }

    }
}

using System;
using System.Web.Mvc;
using System.Web.Hosting;

namespace Epic.MVC
{
    /// <summary>
    /// xsl 视图引擎
    /// S
    /// 20101214
    /// </summary>
    public class XslViewEngine : VirtualPathProviderViewEngine
    {

        public XslViewEngine() : this(null)
		{

		}

        public XslViewEngine(VirtualPathProvider provider)
        {
            if (provider != null)
                this.VirtualPathProvider = provider;

            this.AreaMasterLocationFormats = new string[0];
            this.AreaViewLocationFormats = new string[]{ "~/Areas/{2}/Views/{1}/{0}.xsl", "~/Areas/{2}/Views/{1}/{0}.xslt", "~/Areas/{2}/Views/Shared/{0}.xsl", "~/Areas/{2}/Views/Shared/{0}.xslt" };
            this.AreaPartialViewLocationFormats = new string[]{ "~/Areas/{2}/Views/{1}/{0}.xsl", "~/Areas/{2}/Views/{1}/{0}.xslt", "~/Areas/{2}/Views/Shared/{0}.xsl", "~/Areas/{2}/Views/Shared/{0}.xslt" };

            this.ViewLocationFormats = new[]{ "~/Views/{1}/{0}.xsl", "~/Views/{1}/{0}.xslt", "~/Views/Shared/{0}.xsl", "~/Views/Shared/{0}.xslt" };
            this.MasterLocationFormats = new string[0];
            this.PartialViewLocationFormats = this.ViewLocationFormats;
            this.FileExtensions = new string[] { "xsl", "xslt" };
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return CreateView(controllerContext, partialPath, null);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var tpl = new XslTemplate(this.VirtualPathProvider, viewPath);

            if (controllerContext.HttpContext.Request.Params["output"] != "xml")
                return new XslView(tpl);
            else
                return new XmlView(tpl);
        }
    }
}

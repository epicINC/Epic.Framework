using System;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Web.Mvc;
using Epic.MVC.Html;

namespace Epic.MVC
{
    public class XslTemplate
    {
        VirtualPathProvider provider;
        string viewPath;

        public XslTemplate(VirtualPathProvider provider, string viewPath)
        {
            if (provider == null)
                throw new ArgumentNullException("virtualPathProvider");

            this.provider = provider;
            this.viewPath = viewPath;
        }


        public string PhysicalPath
        {
            get { return HostingEnvironment.MapPath(this.viewPath); }
        }


        public VirtualFile File
        {
            get
            {
                return this.provider.GetFile(this.viewPath);
            }
        }


        public void Transform(ViewContext viewContext, IViewDataContainer view,  Stream xml)
        {
            var xsl = new XslCompiledTransform();


            //var resolver = new XmlSecureResolver(new XmlUrlResolver(), new System.Security.PermissionSet(System.Security.Permissions.PermissionState.Unrestricted));
            //resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;

            //using (var steam = this.File.Open())
            //{
            //    using (var xmlReader = XmlReader.Create(steam, new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore })) 
            //    {
            //        xsl.Load(xmlReader, new XsltSettings(true, true), resolver);
            //    }
            //}

            //using (var xmlReader = XmlReader.Create(this.PhysicalPath, new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore }))
            //{
            //    xsl.Load(xmlReader, new XsltSettings(true, true), resolver);
            //}

            xsl.Load(this.PhysicalPath, new XsltSettings(true, true), new XmlUrlResolver());

            var args = new XsltArgumentList();
            args.AddExtensionObject("urn:HtmlHelper", new HtmlHelperWrapper(viewContext, view));
            args.AddExtensionObject("urn:UrlHelper", new UrlHelper(viewContext.RequestContext));
            args.AddExtensionObject("urn:AjaxHelper", new AjaxHelper(viewContext, view));

            args.AddExtensionObject("urn:StringHelper", new StringHelper(viewContext, view));
            args.AddExtensionObject("urn:DateTimeHelper", new DateTimeHelper(viewContext, view));
            args.AddExtensionObject("urn:MathHelper", new MathHelper(viewContext, view));
            args.AddExtensionObject("urn:NodeSetHelper", new NodeSetHelper(viewContext, view));

            // 

            xsl.Transform(XmlHelper.ParseXPathNavigator(xml), args, viewContext.HttpContext.Response.OutputStream);


        }


    }



}

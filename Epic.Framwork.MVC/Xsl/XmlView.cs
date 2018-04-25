using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Collections;
using Epic.Xml;


namespace Epic.MVC
{
    public class XmlView : IView
    {
        protected XslTemplate tpl;
        public XmlView(XslTemplate tpl)
        {
            this.tpl = tpl;
        }

        #region 转移到 Epic.Framwork.MVC.XmlConvertibleWriter

        /*
        void Writeable(XmlWriter writer, IXmlConvertible context, string root = null)
        {
            if (context == null) return;

            if (String.IsNullOrWhiteSpace(root))
                context.WriteXml(writer);
            else
                context.WriteXml(writer, root);
  
        }

        void WriteList(XmlWriter writer, IEnumerable context, string root = null)
        {
            foreach (var item in context)
            {
                Writeable(writer, item as  IXmlConvertible, "Item"); 
            }
        }

        void WriteEnum(XmlWriter writer)
        {
        }

        void WriteElement(XmlWriter writer, string root, object value)
        {
            writer.WriteElementString(root, value.ToString());
        }


        void WriteModel(XmlWriter writer, object o)
        {
            if (o != null)
            {
                writer.WriteStartElement("Model");
                this.WriteList(writer, o as IEnumerable);
                writer.WriteEndElement();
            }
        }

        void WriteViewData(XmlWriter writer, ViewContext viewContext)
        {
            if (viewContext.ViewData.Count == 0) return;
            writer.WriteStartElement("ViewData");
            WriteDictionary(writer, viewContext.ViewData);
            writer.WriteEndElement();
        }

        void WriteTempData(XmlWriter writer, ViewContext viewContext)
        {
            if (viewContext.TempData.Count == 0) return;
            writer.WriteStartElement("TempData");
            WriteDictionary(writer, viewContext.TempData);
            writer.WriteEndElement();
        }



        void WriteDictionary(XmlWriter writer, IDictionary<string, object> dic)
        {
            if (dic == null || dic.Count == 0) return;

            foreach (KeyValuePair<string, object> item in dic)
            {
                if (item.Value is IXmlConvertible)
                {
                    this.Writeable(writer, item.Value as IXmlConvertible, item.Key);
                    continue;
                }

                var type = item.Value.GetType();
                if (type == typeof(string) || type == typeof(int))
                {
                    WriteElement(writer, item.Key, item.Value);
                    continue;
                }
            }
        }
        */
        #endregion

        void WriteModelState(XmlWriter writer, IDictionary<string, ModelState> state)
        {
            if (state == null || state.Count == 0) return;

            writer.WriteStartElement("ModelState");
            foreach (KeyValuePair<string, ModelState> item in state)
            {
                WriteModeStateItem(writer, item);
            }
            writer.WriteEndElement();
        }

        void WriteModeStateItem(XmlWriter writer, KeyValuePair<string, ModelState> state)
        {
            writer.WriteStartElement("Item");
            writer.WriteElementString("Title", state.Key);

            writer.WriteStartElement("Errors");
            foreach (var item in state.Value.Errors)
            {
                writer.WriteStartElement("Item");
                writer.WriteElementString("Message", item.ErrorMessage);
                if (item.Exception != null)
                {
                    writer.WriteElementString("ExceptionMessage", item.Exception.Message);
                }
                
                writer.WriteEndElement();
                
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
        }


        protected void RenderXml(ViewContext viewContext, XmlWriter writer)
        {
            writer.WriteStartDocument();

            //if (viewContext.HttpContext.Request["output"] != "xml")
            //writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + this.tpl.File.VirtualPath + "\"");

            writer.WriteStartElement("Epic");

        

            writer.WriteStartElement("ViewContext");

            XmlConvertibleWriter.Write(writer, viewContext.ViewData.Model, "Model");
            XmlConvertibleWriter.Write(writer, viewContext.ViewData, "ViewData");
            XmlConvertibleWriter.Write(writer, viewContext.TempData, "TempData");
            this.WriteModelState(writer, viewContext.ViewData.ModelState);
            writer.WriteEndElement();

            writer.WriteEndDocument();
        }

        public virtual void Render(ViewContext viewContext, TextWriter writer)
        {
            viewContext.HttpContext.Response.ContentType = "text/xml";
            using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { OmitXmlDeclaration = true, Encoding= Encoding.UTF8 }))
            {
                RenderXml(viewContext, xmlWriter);
            }
        }

    }
}

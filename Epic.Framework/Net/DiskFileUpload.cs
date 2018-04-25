using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Specialized;

namespace Epic.Net
{
    public class DiskFileUpload
    {


        int sizeMax = 100 * 1024 * 1024;
        int sizeThreshold = 4096;
        List<FileItem> fileItems = new List<FileItem>();


        public DiskFileUpload()
        {

        }

        public int SizeMax
        {
            get { return this.sizeMax; }
            set { this.sizeMax = value; }
        }

        public int SizeThreshold
        {
            get { return this.sizeThreshold; }
            set { this.sizeThreshold = value; }
        }

        public List<FileItem> FileItems
        {
            get { return this.fileItems; }
        }

        public void AddField(string name, string value)
        {
            this.fileItems.Add(new FileItem(name, value));
        }

        public void AddField(IDictionary<string, string> dic)
        {
            var t = dic.Select(d => new FileItem(d.Key, d.Value));
            this.fileItems = this.fileItems.Intersect(t).ToList(); ;
        }

        public void AddFile(string name, string path)
        {
            this.fileItems.Add(new FileItem(name, path, true));
        }

        public void AddFile(IDictionary<string, string> dic)
        {
            var t = dic.Select(d => new FileItem(d.Key, d.Value, true));
            this.fileItems = this.fileItems.Intersect(t).ToList(); ;
        }


        public void ParseRequest(WebRequest request)
        {
            var fw = new FileItemWriter();
            fw.Write(this.fileItems);
            fw.Flush();

            request.ContentType = fw.ContentType;
            //request.ContentLength = fw.Length;

            var requestStream = request.GetRequestStream();
            fw.CopyTo(requestStream, sizeThreshold);

            requestStream.Close();
            fw.Close();
        }


        #region GetMimeInfo
        /*
        string GetMimeInfo(string name)
        {
            string result;
            switch (name)
            {
                case ".exe":
                    result = MediaTypeNames.Application.Octet;
                    break;
                case ".pdf":
                    result = MediaTypeNames.Application.Pdf;
                    break;
                case ".rtf":
                    result = MediaTypeNames.Application.Rtf;
                    break;
                case ".soap":
                    result = MediaTypeNames.Application.Soap;
                    break;
                case ".zip":
                    result = MediaTypeNames.Application.Zip;
                    break;

                case ".gif":
                    result = MediaTypeNames.Image.Gif;
                    break;
                case ".jpg":
                case ".jpeg":
                    result = MediaTypeNames.Image.Jpeg;
                    break;
                case ".tiff":
                    result = MediaTypeNames.Image.Tiff;
                    break;

                case ".htm":
                case ".html":
                    result = MediaTypeNames.Text.Html;
                    break;
                case ".txt":
                    result = MediaTypeNames.Text.Plain;
                    break;
                case ".rih":
                    result = MediaTypeNames.Text.RichText;
                    break;
                case ".xml":
                    result = MediaTypeNames.Text.Xml;
                    break;
                default:
                    result = MediaTypeNames.Application.Octet;
                    break;
            }
            return result;
        }
*/
        #endregion
    }



   





}

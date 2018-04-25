using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Epic.Net
{
    public sealed class FileItemWriter
    {
        const string boundaryFormat = "---------------------------{0}";
        const string contentTypeFormat = "multipart/form-data; boundary={0}";
        const string fieldFormat = "--{Boundary}\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}\r\n";
        const string fileFormat = "--{Boundary}\r\nContent-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";

        string boundary;
        string fieldPattern;
        string filePattern;

        internal StreamWriter stream;
        MemoryStream ms;

        public FileItemWriter()
        {
            this.boundary = String.Format(boundaryFormat, DateTime.Now.Ticks.ToString("x"));
            this.fieldPattern = fieldFormat.Replace("{Boundary}", this.boundary);
            this.filePattern = fileFormat.Replace("{Boundary}", this.boundary);
        }

        void Init()
        {
            if (this.stream == null)
                this.stream = this.stream ?? new StreamWriter(new MemoryStream());
        }


        public string ContentType
        {
            get { return String.Format(contentTypeFormat, this.boundary); }
        }

        public string Boundary
        {
            get { return this.boundary; }
        }

        public string FieldPattern
        {
            get { return this.fieldPattern; }
        }

        public string FilePattern
        {
            get { return this.filePattern; }
        }

        StreamWriter Stream
        {
            get { return this.stream; }
        }


        void InnerWrite(FileItem item)
        {
            if (item.IsFile)
            {
                this.stream.Write(String.Format(this.filePattern, item.Name, item.Value, item.Mime));
                this.stream.Flush();
                item.stream.CopyTo(this.Stream.BaseStream);
                item.stream.Close();
            }
            else
            {
                this.stream.Write(String.Format(this.fieldPattern, item.Name, item.Value));

            }
        }

        public void Write(FileItem item)
        {
            this.Init();
            this.InnerWrite(item);
            this.stream.Flush();

        }

        public void Write(List<FileItem> items)
        {
            this.Init();
            items.ForEach(this.InnerWrite);
            this.stream.Flush();
        }

        public void Flush()
        {
            this.stream.Write("--" + boundary + "--");
            this.stream.Flush();
        }


        public long Length
        {
            get { return this.stream == null ? 0 : this.stream.BaseStream.Length; }

        }

        public void CopyTo(Stream stream)
        {
            if (this.stream != null)
            {
                this.stream.BaseStream.Position = 0;
                this.stream.BaseStream.CopyTo(stream);
            }
        }

        public void CopyTo(Stream stream, int bufferSize)
        {
            if (this.stream != null)
            {
                this.stream.BaseStream.Position = 0;
                this.stream.BaseStream.CopyTo(stream, bufferSize);
                stream.Flush();
            }
        }

        public void Close()
        {
            if (this.stream != null)
                this.stream.Close();
        }
    }
}

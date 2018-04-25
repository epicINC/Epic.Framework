using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Epic.Net
{
    public sealed class FileItem
    {
        string name;
        string value;
        bool isFile;

        string mime;
        internal FileStream stream;

        public FileItem(string name, string value)
            : this(name, value, false)
        {
        }

        public FileItem(string name, string value, bool isFile)
        {
            this.name = name;
            this.value = value;
            this.isFile = isFile;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public bool IsFile
        {
            get { return this.isFile; }
            set { this.isFile = value; }
        }

        void LoadFile()
        {
            if (!this.isFile) return;

            if (this.stream == null)
                this.stream = new FileStream(value, FileMode.Open);
        }

        public string Mime
        {
            get
            {
                if (!this.isFile)
                    return System.Net.Mime.MediaTypeNames.Text.Plain;

                if (String.IsNullOrEmpty(mime))
                {
                    this.LoadFile();
                    this.mime = Epic.Mime.MediaType.MimeFromStream(this.value, this.stream);
                }
                return mime;
            }
        }


    }
}

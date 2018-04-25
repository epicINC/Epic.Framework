using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Epic.IO
{
    public class TextFileWriter
    {
        public TextFileWriter(string file)
            : this(file, false, Encoding.Default)
        {
        }

        public TextFileWriter(string file, bool append, Encoding encoding)
        {
            this.Writer = new StreamWriter(file, append, encoding);
        }

        public TextWriter Writer
        {
            get;
            set;
        }

        public void WriteLine(string text)
        {
            this.Writer.WriteLine(text);
        }

        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(String.Format(format, args));
        }


        //public async void WriteLineAsync(string text)
        //{
        //    await this.Writer.WriteLineAsync(text);
        //}


        public void WriteLineAsync(string format, params object[] args)
        {
            this.WriteLineAsync(String.Format(format, args));
        }

 
    }
}

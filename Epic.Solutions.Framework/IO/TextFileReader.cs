using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.IO
{
    public class TextFileReader : IDisposable
    {


        public TextFileReader(string file) : this(file, Encoding.Default)
        {
        }

        public TextFileReader(string file, Encoding encoding)
        {
            this.Reader = new StreamReader(file, encoding);
        }


        public TextReader Reader
        {
            get;
            set;
        }

        public void ReadLine(Action<int, string> action)
        {
            var i = 1;
            string result;
            while ((result = this.Reader.ReadLine()) != null)
            {
                action(i, result);
                i++;
            }
        }

        //public async void ReadLineAsync(Action<int, string> action)
        //{
        //    var i = 1;
        //    string result;
        //    while ((result = await this.Reader.ReadLineAsync()) != null)
        //    {
        //        action(i, result);
        //        i++;
        //    }
        //}

        public string ReadToEnd()
        {
            return this.Reader.ReadToEnd();
        }

        //public async void ReadToEndAsync(Action<string> action)
        //{
        //    action(await this.Reader.ReadToEndAsync());
        //}

        public void Dispose()
        {
            if (this.Reader == null) return;
            this.Reader.Close();
            this.Reader.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Epic.Net
{
    public class NamedPipeMessageClient : IMessageClient, IDisposable
    {
        public NamedPipeMessageClient(string name) : this(name, ".")
        {
        }

        public NamedPipeMessageClient(string name, string address)
        {
            this.Name = name;
            this.Address = address;

            this.Init();
        }

        void Init()
        {
            this.Client = new NamedPipeClientStream(this.Address, this.Name, PipeDirection.InOut, PipeOptions.Asynchronous, TokenImpersonationLevel.None);
            this.Client.Connect();
            this.Writer = new StreamWriter(this.Client);
            this.Writer.AutoFlush = true;
        }


        public string Name
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }



        NamedPipeClientStream Client
        {
            get;
            set;
        }


        StreamWriter Writer
        {
            get;
            set;
        }


        public void Send(string value)
        {
            try
            {
                this.Writer.WriteLine(value);
            }
            catch (Exception)
            {
            }
        }

        public void Close()
        {

            this.Writer.Close();
            this.Writer.Dispose();

            this.Client.Close();
            this.Client.Dispose();
        }


        public void Dispose()
        {
            this.Close();
        }
    }
}

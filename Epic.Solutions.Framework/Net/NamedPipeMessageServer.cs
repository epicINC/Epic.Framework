using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Epic.Net
{
    public class NamedPipeMessageServer : IMessageServer, IDisposable
    {


        public NamedPipeMessageServer(string name)
        {
            this.Name = name;
            this.IsRunning = true;

        }

        public string Name
        {
            get;
            set;
        }



        public bool IsRunning
        {
            get;
            set;
        }

        Task Handler
        {
            get;
            set;
        }

        CancellationTokenSource Cancell
        {
            get;
            set;
        }

        #region Event

        public event Action<string> Receive;

        void OnReceive(string value)
        {
            if (this.Receive == null) return;
            this.Receive(value);
        }

        #endregion

        public void Start()
        {
            this.Cancell = new CancellationTokenSource();

            this.Handler = new Task(e => this.Task(this.Cancell.Token), this.Cancell.Token, TaskCreationOptions.LongRunning);
            this.Handler.Start();


        }


        void Task(CancellationToken token)
        {
            var Server = new NamedPipeServerStream(this.Name, PipeDirection.InOut, 10, PipeTransmissionMode.Message, PipeOptions.Asynchronous);

            Server.BeginWaitForConnection((ar) =>
            {
                var server = ar.AsyncState as NamedPipeServerStream;
                if (server == null) return;

                server.EndWaitForConnection(ar);

                var sr = new StreamReader(server);
                string result;

                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                        return;
                    }
                    if (!server.IsConnected) return;
                    result = sr.ReadLine();
                    if (result == null || result == "bye") break;
                    this.OnReceive(result);
                }
            }, Server);

        }



        public void Start1()
        {
            var Server = new NamedPipeServerStream(this.Name, PipeDirection.InOut, 10, PipeTransmissionMode.Message, PipeOptions.Asynchronous);

            ThreadPool.QueueUserWorkItem((state) =>
            {
                var handle = Server.BeginWaitForConnection((ar) =>
                {
                    var server = ar.AsyncState as NamedPipeServerStream;
                    if (server == null) return;

                    server.EndWaitForConnection(ar);

                    var sr = new StreamReader(server);
                    var sw = new StreamWriter(server);

                    var result = String.Empty;

                    while (true)
                    {
                        if (!server.IsConnected) return;
                        result = sr.ReadLine();
                        if (result == null || result == "bye") break;
                        this.OnReceive(result);
                    }


                }, Server);

            });

        }




        public void Stop()
        {
            this.Close();
        }


        public void Close()
        {
            if (!this.IsRunning) return;
            this.IsRunning = false;


            this.Cancell.Cancel();
            var server = this.Handler.AsyncState as NamedPipeServerStream;
            if (server == null) return;



            server.Disconnect();
            server.Dispose();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

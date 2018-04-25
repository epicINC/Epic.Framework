using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading;

namespace Epic.Solutions.Net
{
    public class RemotingMessageServer : IMessageServer
    {

        public RemotingMessageServer(int port, params Type[] types)
        {
            this.Types = types;
            this.Port = port;
        }

        public Type[] Types
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public void Start()
        {

            ThreadPool.QueueUserWorkItem(e =>
                {
                    var channel = new TcpServerChannel(this.Port);
                    ChannelServices.RegisterChannel(channel, true);
                    foreach (var item in this.Types)
                    {
                        RemotingConfiguration.RegisterWellKnownServiceType(item, item.Name, WellKnownObjectMode.SingleCall);
                    }
                    Console.Read();
                });





        }

        public void Stop()
        {

        }
    }
}

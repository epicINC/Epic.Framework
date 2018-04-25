using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Runtime.Serialization.Formatters;
using Epic.Components;

namespace Epic.Caching.Remoting
{


    public class EpicCacheServer
    {
      
        public static EpicCacheServer Create(string name, int port)
        {
            BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
            serverProvider.TypeFilterLevel = TypeFilterLevel.Full;  




            var result = new EpicCacheServer();




            result.Channel = new TcpServerChannel(name, port, serverProvider);

            return result;
        }


        IChannel Channel
        {
            get;
            set;
        }

        public void RegisterType(Type type, string objectUri)
        {
            RemotingConfiguration.RegisterWellKnownServiceType(type, objectUri, WellKnownObjectMode.Singleton);
        }


        public void Start()
        {
            Console.WriteLine("Start Epic Cache Server...", DateTime.Now);
            Console.WriteLine("Params:\r\n{{\r\nname: {0}\r\npriority: {1}", this.Channel.ChannelName, this.Channel.ChannelPriority);

            ChannelDataStore data = (ChannelDataStore)((TcpServerChannel)this.Channel).ChannelData;
            foreach (string uri in data.ChannelUris)
            {
                Console.WriteLine("uri: "+ uri);
            }
            Console.WriteLine("}");
            Console.WriteLine("Listening... {0}", DateTime.Now);


            ChannelServices.RegisterChannel(this.Channel, false);


        }

        public void Stop()
        {
            ChannelServices.UnregisterChannel(this.Channel);
        }
    }
}

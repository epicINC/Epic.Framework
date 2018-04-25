using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;

namespace Epic.Caching.Remoting
{
    public class EpicCacheClient
    {
        public static EpicCacheClient Create()
        {
            var result = new EpicCacheClient();
            result.Channel = new TcpClientChannel();
            return result;
        }


        public IChannel Channel
        {
            get;
            set;
        }

        public void RegisterType(Type type, string objectUri)
        {
            RemotingConfiguration.RegisterWellKnownClientType(type, objectUri);
        }


        public void Start()
        {
            ChannelServices.RegisterChannel(this.Channel, false);
        }

        public void Stop()
        {
            ChannelServices.UnregisterChannel(this.Channel);
        }
    }
}

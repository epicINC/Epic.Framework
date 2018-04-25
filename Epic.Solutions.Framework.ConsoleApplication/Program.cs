using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Caching;
using Epic.Components;
using Epic.Caching.Remoting;
using Epic.Utility;
using Epic.Extensions;

namespace Epic.Solutions.Framework.ConsoleApplication
{
    [Serializable]
    public class User
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }


        
    }

 

    class Program


    {
        static void WriteLine(byte[] value)
        {
            foreach (var item in value)
            {
                Console.WriteLine(item);
            }
        }
        static void Main(string[] args)
        {

            var a = new Epic.Components.RSS.RSSDcoument();


            a.channel.Add(new Components.RSS.RSSChannel() { title = "t1", link="t2", description = "t3" });

            //a.channel.First().item.Add(new Components.RSS.RSSItem());

            Console.WriteLine(a.Save());
            Console.ReadLine();

            Epic.Solutions.Framework.ConsoleApplication.TempTest.TypeConverterTest.Test();
            Console.ReadLine();

            return;
            
            var server = EpicCacheServer.Create("epicCache", 8741);
            server.RegisterType(typeof(EpicRuntimeCache), "EpicCache.rem");
            server.Start();

            //var client = EpicCacheClient.Create();
            //client.Start();
            //client.RegisterType(typeof(EpicCacheDictionary), "tcp://localhost:874/EpicCache.rem");

            //var remote = (EpicRuntimeCache)Activator.GetObject(typeof(EpicRuntimeCache), "tcp://localhost:8741/EpicCache.rem");


            //Console.WriteLine("server: static");
            //EpicRuntimeCache<User>.Value = new User() { ID = 1, Name = "server" };


            //Console.WriteLine("client: ");
            //Console.WriteLine("read server value: {0}, {1}", remote.Value<User>().ID, remote.Value<User>().Name);
            //remote.Value<User>().Name = "cicent";
            //Console.WriteLine(remote.Value<User>().Name);
            //Console.WriteLine("server changed: {0}, {1}", EpicRuntimeCache<User>.Value.ID, EpicRuntimeCache<User>.Value.Name);




            Console.ReadLine();
        }

        static void WhileRead()
        {
        }
    }
}

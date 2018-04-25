using Epic.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Epic.Solutions.Testing
{

    public class RssItem
    {
        public int ID
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }
    }


    class Program
    {


        static void UnsafeSwap()
        {
            var value1 = 1;
            var value2 = 2;
            unsafe
            {
                UnsafeSwap(&value1, &value2);
            }
        }


        public unsafe static void UnsafeSwap(int* value1, int* value2)
        {
            int temp = *value1;
            *value1 = *value2;
            *value2 = temp;
        }

        static void Swap()
        {
            var value1 = 1;
            var value2 = 2;
            Swap(ref value1, ref value2);
        }

        public static void Swap(ref int value1, ref int value2)
        {
            int temp = value1;
            value1 = value2;
            value2 = temp;
        }

	




        static void Main(string[] args)
        {
            do
            {
                TestingUtility.Loop(Swap, 1000000);
                TestingUtility.Loop(UnsafeSwap, 1000000);

            }
            while (String.IsNullOrWhiteSpace(Console.ReadLine()));

 



            Console.ReadLine();

            HexTest.TestMethod();


            return;
            var name = "EPIC.IDS.Testing1";
            var server = new NamedPipeMessageServer(name);
            server.Receive += e => Console.WriteLine(e);


            server.Start();



            var client = new NamedPipeMessageClient(name);

            do
            {
                client.Send(DateTime.Now.ToString());
                server.Close();

            }
            while (String.IsNullOrWhiteSpace(Console.ReadLine()));
        }

    }
}

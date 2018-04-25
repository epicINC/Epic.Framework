using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Epic.Solutions.Framework.ConsoleApplication
{
    internal class XXTeaTest
    {

        public static void Test()
        {

            Console.WriteLine(Guid.NewGuid().ToString("N").ToUpper());
            Console.WriteLine(Guid.NewGuid().ToString("N").ToUpper());

            var k = "6BF305594C9C4732B5F74CE7A1D51E02375C396A938A4665A62494152745E670";
            var v = Encode("6BF305594C9C4732B5F74CE7A1D51E02375C396A938A4665A62494152745E670", "ticket=1389622768&id=ynds001x&extend=中文测试");
            Console.WriteLine(v);
            var e = Decode(k, v);
            Console.WriteLine(e);
            Console.WriteLine(Epic.Utility.DateTimeUtility.Unix.From(1389372809));
            Console.WriteLine(Epic.Utility.DateTimeUtility.Unix.To(DateTime.Now));
            Console.WriteLine(DateTime.Now);
            Console.ReadLine();



   
            string key, value;


            Console.Write("input key:");
            while (String.IsNullOrWhiteSpace(key = Console.ReadLine()))
            {
                
            }

            do
            {
                Console.Write("input text:");
                while (String.IsNullOrWhiteSpace(value = Console.ReadLine()))
                {

                }

                Console.WriteLine();

                var encode = Encode(key, value);
                var decode = Decode(key, encode);
                Console.WriteLine("enocde: " + encode);
                Console.WriteLine("decode: " + decode);
                Console.WriteLine("assert: " + (value == decode));

                var encode2 = Encode2(key, value);
                var decode2 = Decode2(key, encode);
                Console.WriteLine("enocde2: " + encode2);
                Console.WriteLine("decode2: " + decode2);
                Console.WriteLine("assert2: " + (value == decode2));

            } while (String.IsNullOrWhiteSpace(Console.ReadLine()));


        }


        public static string Encode(string key, string value)
        {
            return Epic.Security.Utility.Hex.XXTEAEncode(key, value);
        }

        public static string Decode(string key, string value)
        {
            return Epic.Security.Utility.Hex.XXTEADecode(key, value);

        }

        public static string Encode2(string key, string value)
        {
            return Epic.Solutions.Framework.ConsoleApplication.XXTea.XXTEA.Encrypt(value, key);
        }

        public static string Decode2(string key, string value)
        {
            return Epic.Solutions.Framework.ConsoleApplication.XXTea.XXTEA.Decrypt(value, key);

        }
    }
}

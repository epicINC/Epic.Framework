using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Solutions.Testing
{
    static class HexTest
    {

        public static void TestMethod()
        {
            byte[] exp = File.ReadAllBytes(@"E:\Source\20110124 Epic.Framework\Source\Epic.Solutions.Testing\words.txt");
            System.Diagnostics.Stopwatch clock;
            long memory = 0;

            // repeat the test 10 times (I actually repeat it 1000 times)
            for (int n = 0; n < 10; n++)
            {
                // test for my implementation of toHexString
                clock = Stopwatch.StartNew(); // syntax compatible with older CLR versions
                memory = GC.GetTotalMemory(true);
                string s1 = Fast.ToHexString(exp, false);
                clock.Stop();
                memory = GC.GetTotalMemory(false) - memory;
                Console.Write("{0} [{1}] vs ", clock.Elapsed.TotalMilliseconds, memory);

                // test for bit converter
                clock = Stopwatch.StartNew();
                memory = GC.GetTotalMemory(true);
                string s2 = ToHexString(exp);
                clock.Stop();
                memory = GC.GetTotalMemory(false) - memory;
                Console.WriteLine("{0} [{1}] -> {2} ",
                        clock.Elapsed.TotalMilliseconds, memory, s1 == s2);


                // test for my implementation of fromHexString
                clock = Stopwatch.StartNew();
                byte[] b1 = Fast.FromHexString(s1);
                clock.Stop();
                Console.Write("fromHex: {0} vs ", clock.Elapsed.TotalMilliseconds);

                // test for MSDN blog peeked implementation
                clock = Stopwatch.StartNew();
                byte[] b2 = HexToBitArray(s1);
                clock.Stop(); Console.WriteLine(clock.Elapsed.TotalMilliseconds);





                Console.WriteLine("");
            }
            Console.ReadLine();
        }

        public static byte[] HexToBitArray(this string str)
        {
            if (str.Length == 0 || str.Length % 2 != 0)
                return new byte[0];

            byte[] buffer = new byte[str.Length / 2];
            char c;
            for (int bx = 0, sx = 0; bx < buffer.Length; ++bx, ++sx)
            {
                // Convert first half of byte
                c = str[sx];
                buffer[bx] = (byte)((c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0')) << 4);

                // Convert second half of byte
                c = str[++sx];
                buffer[bx] |= (byte)(c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0'));
            }

            return buffer;
        }

        public static string ToHexString(this byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];

            byte b;

            for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
            {
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
            }

            return new string(c);
        }
    }



    // class is sealed and not static in my personal complete version
    public unsafe sealed partial class Fast
    {
        public static byte[] FromHexString(string s)
        {
            return null;
        }

        public static string ToHexString(byte[] s, bool ss)
        {
            return null;
        }
    }
}

using Epic.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Solutions.Framework.ConsoleApplication.SpeedTest
{
    internal class GuidAndRNG
    {
        public static void Test()
        {

            CountDown.For(1, () =>
            {
                Console.WriteLine(Guid.NewGuid().ToString("N"));
            });

            CountDown.For(1, () =>
            {
                Console.WriteLine(Epic.Security.Utility.Salt(32));
            });

        }
    }
}

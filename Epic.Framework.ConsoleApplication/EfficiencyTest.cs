using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Framework.Testing;

namespace Epic.Framework.ConsoleApplication
{
    public static class EfficiencyTest
    {
        static string s = String.Empty;
        public static void Test()
        {

            var loop = 10000000;
            //TestingUtility.SpeedTest(Test1, loop);
            //TestingUtility.SpeedTest(Test2, loop);
            //TestingUtility.SpeedTest(Test3, loop);
        }

        static bool Test1()
        {
            return s.Length == 0;
        }

        static bool Test2()
        {
            return s == String.Empty;
        }

        static bool Test3()
        {
            return s == "";
        }
    }
}

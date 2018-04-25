using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Framework.Testing;
using System.Diagnostics;

namespace Epic.Framework.ConsoleApplication
{
    public class CodeTest
    {


        public static void TS(List<int> value, int loop)
        {
            TestingUtility.SpeedTest(StackTest, value, loop);
            TestingUtility.SpeedTest(ListTest, value, loop);
            TestingUtility.SpeedTest(ListReverseTest, value, loop);
        }


        public static void StackTest(List<int> value)
        {
            var s = new Stack<int>();

            value.ForEach(e => s.Push(e));

            Trace.Write(s.ToList().Count);

        }

        public static void ListTest(List<int> value)
        {
            var s = new List<int>();

            value.ForEach(e => s.Insert(0, e));

            Trace.Write(s.Count);

        }

        public static void ListReverseTest(List<int> value)
        {
            var s = new List<int>();

            value.ForEach(e => s.Add(e));

            s.Reverse();
            Trace.Write(s.Count);

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Epic.Framework.Testing
{
    public static class TestingUtility
    {
        public static K SpeedTest<T, K>(Func<T, K> func, T arg, int loop)
        {
            Console.WriteLine("--" + func.Method.Name + ": Start");
            GC.Collect();
            int gc0 = GC.CollectionCount(0),
                gc1 = GC.CollectionCount(1),
                gc2 = GC.CollectionCount(2);

            var result = default(K);

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < loop; i++)
            {
                result = func(arg);
            }
            watch.Stop();

            Console.WriteLine(watch.ElapsedMilliseconds + "ms");
            Console.WriteLine("GC 0:" + (GC.CollectionCount(0) - gc0));
            Console.WriteLine("GC 1:" + (GC.CollectionCount(1) - gc1));
            Console.WriteLine("GC 2:" + (GC.CollectionCount(2) - gc2));
            Console.WriteLine();
            return result;
        }

        public static void SpeedTest<T>(Action<T> func, T arg, int loop)
        {
            Console.WriteLine("--" + func.Method.Name + ": Start");
            GC.Collect();
            int gc0 = GC.CollectionCount(0),
                gc1 = GC.CollectionCount(1),
                gc2 = GC.CollectionCount(2);

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < loop; i++)
            {
                func(arg);
            }
            watch.Stop();

            Console.WriteLine(watch.ElapsedMilliseconds + "ms");
            Console.WriteLine("GC 0:" + (GC.CollectionCount(0) - gc0));
            Console.WriteLine("GC 1:" + (GC.CollectionCount(1) - gc1));
            Console.WriteLine("GC 2:" + (GC.CollectionCount(2) - gc2));
            Console.WriteLine();
        }



        public static void SpeedTest(Action action, int loop)
        {
            Console.WriteLine("--"+ action.Method.Name +": Start");
            GC.Collect();
            int gc0 = GC.CollectionCount(0),
                gc1 = GC.CollectionCount(1),
                gc2 = GC.CollectionCount(2);

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < loop; i++)
            {
                action();
            }
            watch.Stop();

            Console.WriteLine(watch.ElapsedMilliseconds + "ms");
            Console.WriteLine("GC 0:" + (GC.CollectionCount(0) - gc0));
            Console.WriteLine("GC 1:" + (GC.CollectionCount(1) - gc1));
            Console.WriteLine("GC 2:" + (GC.CollectionCount(2) - gc2));
            Console.WriteLine();
        }

        public static void SpeedTest<T>(Func<T> action, int loop)
        {
            Console.WriteLine("--" + action.Method.Name + ": Start");
            GC.Collect();
            int gc0 = GC.CollectionCount(0),
                gc1 = GC.CollectionCount(1),
                gc2 = GC.CollectionCount(2);

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < loop; i++)
            {
                action();
            }
            watch.Stop();

            Console.WriteLine(watch.ElapsedMilliseconds + "ms");
            Console.WriteLine("GC 0:" + (GC.CollectionCount(0) - gc0));
            Console.WriteLine("GC 1:" + (GC.CollectionCount(1) - gc1));
            Console.WriteLine("GC 2:" + (GC.CollectionCount(2) - gc2));
            Console.WriteLine();
        }

      
    }
}

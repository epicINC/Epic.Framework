using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Testing
{
    public class CountDown
    {

        public static T Watch<T>(Func<T> action)
        {
            return Watch<T>(action.Method.Name, action);
        }


        public static T Watch<T>(string title, Func<T> action)
        {

            GC.Collect();
            int gc0 = GC.CollectionCount(0), gc1 = GC.CollectionCount(1), gc2 = GC.CollectionCount(2);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}: Start", title);
            Console.ForegroundColor = ConsoleColor.White;

            var watcher = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                return action();
            }
            finally
            {
                watcher.Stop();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}: End, Elapsed {1}ms", title, watcher.ElapsedMilliseconds);
                Console.ForegroundColor = ConsoleColor.White;

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("GC 0:" + (GC.CollectionCount(0) - gc0));
                Console.WriteLine("GC 1:" + (GC.CollectionCount(1) - gc1));
                Console.WriteLine("GC 2:" + (GC.CollectionCount(2) - gc2));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }




        public static void Watch(Action action)
        {
            Watch(action.Method.Name, action);
        }

        public static void Watch(string title, Action action)
        {
            GC.Collect();
            int gc0 = GC.CollectionCount(0), gc1 = GC.CollectionCount(1), gc2 = GC.CollectionCount(2);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}: Start", title);
            Console.ForegroundColor = ConsoleColor.White;

            var watcher = System.Diagnostics.Stopwatch.StartNew();

            action();

            watcher.Stop();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}: End, Elapsed {1}ms", title, watcher.ElapsedMilliseconds);
            Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("GC 0:" + (GC.CollectionCount(0) - gc0));
            Console.WriteLine("GC 1:" + (GC.CollectionCount(1) - gc1));
            Console.WriteLine("GC 2:" + (GC.CollectionCount(2) - gc2));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }




        public static void For(int count, Action action)
        {
            For(action.Method.Name, count, action);
        }


        public static void For(string title, int count, Action action)
        {
            GC.Collect();
            int gc0 = GC.CollectionCount(0), gc1 = GC.CollectionCount(1), gc2 = GC.CollectionCount(2);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}: Start, Loop {1}", title, count);
            Console.ForegroundColor = ConsoleColor.White;

            var watcher = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
            {
                action();
                Console.WriteLine("Loop {0}, {1}", i, title);
                Console.WriteLine();
            }

            watcher.Stop();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}: End, Elapsed {1}ms", title, watcher.ElapsedMilliseconds);
            Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("GC 0:" + (GC.CollectionCount(0) - gc0));
            Console.WriteLine("GC 1:" + (GC.CollectionCount(1) - gc1));
            Console.WriteLine("GC 2:" + (GC.CollectionCount(2) - gc2));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }


    }
}

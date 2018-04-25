
using Epic.Framework.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Extensions;

namespace Epic.Framework.ConsoleApplication.DataProviders
{
    public static class ProviderTest
    {
        public static void Test()
        {


            var provider = TestDataProvider<RssItem>.Current;

            string command;
            while ((command = Console.ReadLine()) != null)
            {
                switch (command)
                {
                    case "i" :
                        //InsertTest(provider);
                        TestingUtility.SpeedTest(() => provider.Insert(new RssItem() { Title = "test "+ DateTime.Now }), 10000);
                        break;
                    case "e" :
                        EditTest(provider);
                        break;
                    case "d" :
                        DeleteTest(provider);
                        break;
                    case "p" :
                        Print(provider);
                        break;
                    case "q" :
                        QueryTest(provider);
                        break;
                    case "c" :
                        Clear(provider);
                        break;
                    default:
                        break;
                }
            }

        }

        static void InsertTest(TestDataProvider<RssItem> provider)
        {
            Console.Write("请输入要插入的数量: ");
            var loop = Int32.Parse(Console.ReadLine());
            for (int i = 0; i < loop; i++)
            {
                provider.Insert(new RssItem());
            }
        }

        static void EditTest(TestDataProvider<RssItem> provider)
        {
            Console.Write("请输入要修改编号: ");
            var result = provider.Find(e => e.ID == Int32.Parse(Console.ReadLine()));
            result.Title = "test" + DateTime.Now;
            Print(result);
            provider.Save(result);
        }

        static void DeleteTest(TestDataProvider<RssItem> provider)
        {
            Console.Write("请输入要删除编号: ");
            var item = provider.Find(e => e.ID == Int32.Parse(Console.ReadLine()));
            Print(item);
            provider.Delete(item);

        }

        static void QueryTest(TestDataProvider<RssItem> provider)
        {
            Console.Write("请输入要查询的关键字: ");
            var item = provider.FindAll(e => e.Title.Contains(Console.ReadLine().Trim()));
            Print(provider, item);
        }

        static void BatchDelete(TestDataProvider<RssItem> provider)
        {
            var item = provider.FindAll(e => e.ID < 10);
            Print(provider, item);
            provider.Delete(item);
        }


        static void Print(TestDataProvider<RssItem> provider, IEnumerable<RssItem> items = null)
        {
            if (items == null) items = provider.FindAll().OrderBy(e => e.ID);

            foreach (var item in items)
            {
                Print(item);
            }

        }

        static void Clear(TestDataProvider<RssItem> provider)
        {
            provider.Clear();
        }

        static void Print(RssItem value)
        {
            Console.WriteLine(value.ID +" "+ value.Title);
        }
    }
}

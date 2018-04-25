using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Data;
using Epic.Data.V2;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Epic.Web;
using System.Diagnostics;
using Epic.Extensions.Expressions;
using System.Text;

namespace Epic.Framework.ConsoleApplication
{


    public class Test
    {
        public int ID
        {
            get;
            set;
        }
    }

    public enum tEnum
    {
        Left,
        Right,
        Center
    }

    class Program
    {
        static void Main(string[] args)
        {

            do
            {

               
                //AOP.AOPTest.Test();
                //DataProviders.ProviderTest.Test();

                //新参数对象.测试.Test();




               // var watch = new Stopwatch();
               // watch.Start();



               ////var p = ExpressionExtensions.Parameter<RssItem>("e");
               ////var c = System.Linq.Expressions.Expression.Call(null, value.Method, p);
               ////var ex = System.Linq.Expressions.Expression.Lambda<Func<RssItem, int>>(c, p);
               ////var result = Epic.FluentAPI.FluentAPIExtensions.GetComplexPropertyAccess(ex);



               ////Console.WriteLine(result.First().Name);



               // watch.Stop();

               // Console.WriteLine("Elapsed: " + watch.ElapsedMilliseconds);
                




                //CodeFirstTest();
                //EfficiencyTest.Test();
                //OldDataProvider.InTest();
            }
            while (String.IsNullOrWhiteSpace(Console.ReadLine()));
        }


        static string ta()
        {
            
            Console.Write("1");
            return "2";
        }
        



        static void AzDGXXTEATest()
        {
            Epic.Framework.ConsoleApplication.AzDGXXTEATest.TS(Encoding.UTF8.GetBytes("adbcdeafeddadbcdeafeddadbcdeafeddadbcdeafedd"), 10000);
        }



        static void CodeFirstTest()
        {
            ModelBuilder.Entity<RssItem>().ToTable("BetaUN_RssItems");
            ModelBuilder.Entity<RssItem>().Column(e => e.ID)
                .Name("ID")
                .Order(0)
                .Type("int")
                .Generated()
                .IsRequired();
            ModelBuilder.Entity<RssItem>().PrimaryKeys(e => e.ID);



            var connection = new SqlConnection("Password=s123;Persist Security Info=True;User ID=sa;Initial Catalog=DB_BetaUN;Data Source=(local)");
            connection.Open();
            var provider = new Epic.Data.Query.DbQueryProvider(connection);
            var rssItems = provider.CreateQuery<RssItem>();

            var search = "WP7";
            var q = from item in rssItems where item.Title.Contains(search) select new { ID = item.ID, Title = item.Title };
            var list = q.Take(10).Skip(20);

            Console.WriteLine("Query: " + list);

            foreach (var item in list)
            {
                Console.WriteLine(item.Title);
            }

        }


        static void t1()
        {
            var list = new List<string>() { "a", "b", "c"};

        }


        static void Test1()
        {
            var p = new ObjectDataProvider<RssItem>("Password=s123;Persist Security Info=True;User ID=sa;Initial Catalog=DB_BetaUN;Data Source=(local)");

            // var query = p.CreateQuery().And(e => e.ID > 0).Paged(2, 10).OrderBy("ID", SortDirection.Desc);
            var query = p.CreateQuery();
            //var query = p.ExecuteSP("BitLab_CommonPaging", new { AbsolutePage = 1, PageSize = 20, Fields = "*", Tables = "BetaUN_RssItems", Order="ID Desc" });
          
            //Console.WriteLine(query);

            foreach (var item in query)
            {
                Console.WriteLine(item.Title);
                System.Threading.Thread.Sleep(200);
            }

            var value = query.FirstOrDefault();
            if (value != null)
                p.Delete(value);
        }


        static void PagingWithParam()
        {
            var list = new List<RssItem>() { new RssItem(){ FeedID=4}, new RssItem(){FeedID=20}, new RssItem(){FeedID=26} };

            var p1 = Epic.Web.HttpParam<int>.Valid("categoryID", 0).QueryParam();
            var p = new ObjectDataProvider<RssItem>("Password=s123;Persist Security Info=True;User ID=sa;Initial Catalog=DB_BetaUN;Data Source=(local)");
            var query = p.CreateQuery().And(p1, e => e.CategoryID == p1.Value)
                .And(e => e.Title.Contains("asfsa")).Paged(1,20);
                
                
            //query.And(e => e.ID.In(list.Select(y => y.FeedID))).OrderByDescending(e => e.ID);

            foreach (var item in query)
            {
                Console.WriteLine(item.Title);
            }
            Console.WriteLine("RecordCount:{0}, AbsolutePage:{1}, PageCount:{2}, PageSize:{3}", query.Paging.RecordCount, query.Paging.AbsolutePage, query.Paging.PageCount, query.Paging.PageSize);

        }




        static int InsertTest(DbCommand command, RssItem item)
        {
            return 0;
            //command.Parameters["ID"].Value = item.ID;
            //command.Parameters["Title"].Value = item.Title;
            //command.Parameters["CreateDate"].Value = item.CreateDate;

           // var result = command.ExecuteNonQuery();
           // item.ID = (int)command.Parameters["ID"].Value;
          //  return result;

        }

        public static int UpdateTest(DbCommand command, RssItem item)
        {
            command.Parameters["@ID"].Value = item.ID;
            command.Parameters["@Title"].Value = item.Title;
            command.Parameters["@CreateDate"].Value = item.CreateDate;
            return command.ExecuteNonQuery();
        }

        public static int DeleteTest(DbCommand command, RssItem item)
        {
            command.Parameters["@ID"].Value = item.ID;
            return command.ExecuteNonQuery();
        }
    }
}

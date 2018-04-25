using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.Data;
using Epic.FluentAPI;
using Epic.Extensions;
using Epic.Web;
using Epic.Components;
using Epic.Paging;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Epic.Data.Mongo;
using Epic.Extensions.Expressions;

namespace Epic.Framework.ConsoleApplication
{
    class 委托测试
    {

        public static void Test()
        {

            var param = new TestParam();

            //var filter = new PagingFilter<TestClass, TestParam>(param);

            //var where = filter.Where((e, p) => e.ID > 10 && e.ID < 1000);


            //Expression<Func<TestClass, TestParam, bool>> func = (e, p) => e.ID.In(1000, 10000, 345, 34534, p.ID);



            //var server = MongoServer.Create("mongodb://localhost/?safe=true");

            //server.Connect();

            //var database = server.GetDatabase("BetaUN");

            //var collection = database.GetCollection<RssItem>("RssItem");


            //var node = new RemoveParamExpressionVisitor(param).Visit(func);
            //var result = new MongoBsonExpressionVisitor();
            //var node2 = result.Visit(node);

            //var query = result.GenerateQuery();

            //Console.WriteLine("Remove: "+ node);
            //Console.WriteLine("Bson: " + query);



            //var result1 = collection.Find(query);
            //Console.WriteLine(result1.Count());
            

            //query = Query.In("ID", MongoDB.Bson.BsonArray.Create(new int[] { 1000, 10000, 345, 34534 }));
            //Console.WriteLine(query);

            //var result2 = collection.Find(query);
            //Console.WriteLine(result2.Count());




            //server.Disconnect();





            return;


            var lambda = Expression.Lambda<Func<int, int, int>>(
                ExpressionExtensions.Add(ExpressionExtensions.Multiply<int>("a", "b"), 2),
                ExpressionExtensions.Parameter<int>("a"),
                ExpressionExtensions.Parameter<int>("b")
                );

            Console.WriteLine(lambda.ToString());




        }



        public class TestClass
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

        public class TestParam
        {
            public TestParam()
            {
                this.ID = HttpParam<int>.Error(10);
                this.Title = HttpParam<string>.Error("QQ");
            }


            public HttpParam<int> ID
            {
                get;
                set;
            }

            public HttpParam<string> Title
            {
                get;
                set;
            }
        }
    }



    public static class MongoExtensions
    {
        public static MongoCursor<T> Find<T>(this MongoCollection<T> value, string query)
        {
            return value.Find(Query.Where(query));
        }
    }
}

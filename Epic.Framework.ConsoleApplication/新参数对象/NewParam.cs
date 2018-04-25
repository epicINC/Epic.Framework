using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.NewParam;
using Epic.Extensions.Expressions;
using System.Linq.Expressions;

namespace Epic.Framework.ConsoleApplication.新参数对象
{
    public static class testc<T, K>
    {
        public static string Name
        {
            get;
            set;
        }
    }



    public enum Token
    {
        Create,
        Update,
        Delete
    }
    


    public static class 测试
    {
        public static void Method<T, K>(Func<T, K> func)
        {
            Console.WriteLine(typeof(K));
        }


        /// <summary>
        /// 表达式 测试
        /// </summary>
        public static void Test2()
        {

        }

        public static void Test()
        {



            //return;
            //Method<RssItem, int[]>(e => e.IDS);


            //return;
            var p = new WebParam<RssItem>();


            p.RuleFor(e => e.FeedID).Original("0").ID();
            p.RuleFor(e => e.PubDate).Original("1981 08/07");
            //p.RuleFor(e => e.IDS).Original("0,2,1").Required();
            p.RuleForArray<Token>("test").Original("2,1");




        }
    }
    
    public class User
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public DateTime CreateDate
        {
            get;
            set;
        }
    }



}

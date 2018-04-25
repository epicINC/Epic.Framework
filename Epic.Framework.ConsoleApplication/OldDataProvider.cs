using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Data;
using Epic.Data.V2;

namespace Epic.Framework.ConsoleApplication
{
    public static class OldDataProvider
    {
        static string connectionString = "Password=s123;Persist Security Info=True;User ID=sa;Initial Catalog=DB_BetaUN;Data Source=(local)";
        public static void InTest()
        {
            var provider = new Epic.Data.V2.ObjectDataProvider<RssItem>(connectionString);
            //var query = provider.CreateQuery().And(e =>( e.ID &  1) == 1);

            //foreach (var item in query)
           // {
           //     Console.WriteLine(item.Title);
           // }

        }

        public static void TestSelectAll()
        {
            var provider = new Epic.Data.V2.ObjectDataProvider<RssItem>(connectionString);
            var query = provider.SelectAll();
            foreach (var item in query)
            {
                Console.WriteLine(item.Title);
            }
        }
    }
}

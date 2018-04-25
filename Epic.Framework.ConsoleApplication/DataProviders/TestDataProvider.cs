using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Data.Mongo;
using Epic.Data.Mongo.Samus.FluentAPI;

namespace Epic.Framework.ConsoleApplication.DataProviders
{
    public class TestDataProvider<T> : Epic.Data.Mongo.Gen10.Gen10DataProvider<T> where T :  class
    {
        protected const string ProviderName = "MongoDataProvider";

        static TestDataProvider<T> context;

        static TestDataProvider()
        {
            MongoAPI<RssItem>.Config.TableAlias("Epic");

            context = new TestDataProvider<T>();
            context.Initialize(ProviderName);
        }

        public static TestDataProvider<T> Current
        {
            get { return context; }
        }

    }
}

using Epic.Data.Mongo.Gen10;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Framework.Data.ConsoleApplication.Gen10Test
{

    public class User
    {
        public ObjectId Id { get; set; }

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



    public class Index
    {

        public static void Insert()
        {
            Gen10DataProvider<User>.Current.Insert(new User() { ID = 1, Name = "test", CreateDate = DateTime.Now });
        }

        public static void Read(int id)
        {
            var result = Gen10DataProvider<User>.Current.FindAll();

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }


        public static void Test()
        {
            //Insert();
            Read(1);
        }
    }
}

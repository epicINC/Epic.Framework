using Epic.Data.Mongo.Gen10;
using Epic.Data.Mongo.Gen10.FluentAPI;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Data.Mongo.Gen10
{
    internal class TableCounterManager<T>
    {
        internal TableCounterManager(Gen10Connection connection)
        {
            this.Table = connection.GetDatabase().GetCollection<TableCounter>("Epic.Data.Mongo.TabCounter");
            this.Name = MongoAPI<T>.FullName;
        }


        public MongoCollection<TableCounter> Table
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public int Counter
        {
            get
            {
                var result = this.Table.FindAndModify(
                    Query.EQ("Name", this.Name),
                    null,
                    Update.Inc("Value", 1),
                    true
                );
                var counter = result.GetModifiedDocumentAs<TableCounter>();

                if (!result.Ok || counter == null)
                {
                    counter = new TableCounter(){ Name = this.Name, Value = 1 };
                    this.Table.Insert(counter);
                }

                return counter.Value;



                //var result = this.Table.FindOne("{Name:" + this.Name + "}");
                //if (result == null)
                //{
                //    result = new TableCounter { Name = this.Name };
                //    result.Value++;
                //    this.Table.Insert(result);
                //}
                //else
                //{
                //    result.Value++;
                //    var update = new UpdateDocument { { "$set", new BsonDocument("Value", result.Value) } };
                //    this.Table.Update(Query.EQ("Name", this.Name), update);
                //}
                //return result.Value;


                //var result = this.Connection.GetDatabase().GetCollection<TableCounter>(MongoAPI<TableCounter>.FullName).FindOne(Query.EQ("Name", MongoAPI<T>.FullName));
                //if (result == null) result = new TableCounter { Name = MongoAPI<T>.FullName };
                //result.Value++;
                //this.Connection.GetDatabase().GetCollection<TableCounter>(MongoAPI<TableCounter>.FullName).Save(result);
                //return result.Value;
            }
        }





        public void Reset()
        {
            this.Table.Remove(Query.EQ("Name", this.Name));
        }
    }
}

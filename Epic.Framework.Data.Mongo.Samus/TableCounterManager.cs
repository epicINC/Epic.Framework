using Epic.Data.Mongo.Samus.FluentAPI;
using MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Data.Mongo.Samus
{
    internal class TableCounterManager<T>
    {
        internal TableCounterManager(SamusConnection connection)
        {
            this.Table = connection.GetDatabase().GetCollection<TableCounter>("Epic.Data.Mongo.TabCounter");
            this.Name = MongoAPI<T>.FullName;
        }


        public IMongoCollection<TableCounter> Table
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
                // new Document("$inc", new Document("Value", 1))
                // 
                var result = this.Table.FindAndModify(
                    new Document {{"$inc", new Document {{"Value", 1}}}},
                    new Document("Name", this.Name),
                    true
                    );

                if (result == null)
                {
                    result = new TableCounter() { Name = this.Name, Value = 1 };
                    this.Table.Insert(result);
                }

                return result.Value;




                //var result = this.Table.FindOne(e => e.Name == this.Name);
                //if (result == null)
                //{
                //    result = new TableCounter { Name = this.Name };
                //    result.Value++;
                //    this.Table.Insert(result);
                //}
                //else
                //{
                //    result.Value++;
                //    //this.Table.Update(result, new Document("Name", this.Name));
                //    this.Table.Update(new Document("$set", new Document("Value", result.Value)), new Document("Name", this.Name), UpdateFlags.Upsert);
                //}
                //return result.Value; 
            }
        }


        public void Reset()
        {
            this.Table.Remove(new Document("Name", this.Name));
        }
    }
}

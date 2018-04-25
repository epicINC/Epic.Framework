using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data.Schema
{
    public class TableName
    {

        public TableName(string name) : this(null, name)
        {
        }

        public TableName(string schema, string name)
        {
            this.schema = schema;
            this.name = name;
        }


        string schema;
        string name;

        public string Schema
        {
            get { return this.schema; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public override string ToString()
        {
            if (!String.IsNullOrWhiteSpace(this.schema))
                return this.schema + "." + this.name;
            return this.name;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data.Schema
{
    public class TableSchemaAttribute : Attribute
    {

        public TableSchemaAttribute(string name) : this(null, name)
        {
        }

        public TableSchemaAttribute(string schema, string name)
        {
            this.Schema = schema;
            this.Name = name;
            this.HasSchema = !String.IsNullOrWhiteSpace(schema);
        }


        /// <summary>
        /// 架构
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        public bool HasSchema { get; private set; }

    }
}

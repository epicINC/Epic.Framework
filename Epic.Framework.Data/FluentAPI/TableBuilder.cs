using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.Data.Schema;
using System.Reflection;
using Epic.FluentAPI;

namespace Epic.Data
{

    public class TableBuilder<T>
    {
        public TableBuilder<T> ToTable(string tableName)
        {
            return ToTable("dbo", tableName);
        }

        public TableBuilder<T> ToTable(string schemaName, string tableName)
        {
            TableDefinition<T>.Init(schemaName, tableName);
            return this;
        }

        public ColumnBuilder<T, K> Column<K>(Expression<Func<T, K>> func)
        {
            var result = TableDefinition<T>.Property(func);
            return new ColumnBuilder<T, K>(result);
        }

        public TableBuilder<T> PrimaryKeys<K>(Expression<Func<T, K>> func)
        {
            TableDefinition<T>.Key(func.GetSimplePropertyAccessList().Select(e => e.Single()));

            return this;
        }




    }
}

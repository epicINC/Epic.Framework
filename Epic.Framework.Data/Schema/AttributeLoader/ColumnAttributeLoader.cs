using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Epic.Extensions;
using Epic.FluentAPI;

namespace Epic.Data.Schema.AttributeLoader
{
    internal static class ColumnAttributeLoader
    {
        internal static void Init(TableDefinition table)
        {
            table.PrimaryKeys = new List<ColumnDefinition>();
            table.Columns = new Dictionary<PropertyPath, ColumnDefinition>();

            foreach (var item in table.Type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attr = item.GetCustomAttributes<ColumnSchemaAttribute>();

                var column = new ColumnDefinition(item, table);

                if (attr != null)
                {
                    column.ColumnName = attr.Name ?? item.Name;
                    column.Generated = attr.IsDbGenerated;
                    column.Optional = attr.IsNullable;

                    if (attr.IsPrimaryKey)
                        table.PrimaryKeys.Add(column);
                }
                else
                {
                    column.ColumnName = item.Name;
                }

                table.Columns.Add(new PropertyPath(item), column);
            }


            if (table.PrimaryKeys.Count == 0)
            {
                var column = table.Columns.Values.SingleOrDefault(e => e.ColumnName == "ID");
                if (column != null)
                    table.PrimaryKeys.Add(column);

            }
        }


    }
}

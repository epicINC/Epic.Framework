using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Extensions;

namespace Epic.Data.Schema.AttributeLoader
{
    internal class TableAttributeLoader
    {

        internal static void Init(TableDefinition table)
        {
            var attr = table.Type.GetCustomAttributes<TableSchemaAttribute>();

            if (attr != null && String.IsNullOrWhiteSpace(attr.Name))
                table.TableName = new TableName(attr.Schema, attr.Name);
            else
                table.TableName = new TableName(table.Type.Name);

        }



    }
}
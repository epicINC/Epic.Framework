using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Data.Schema;

namespace Epic.Data
{

    public enum DatabaseGeneratedOption
    {
        None,
        Identity,
        Computed
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Table 实体对象</typeparam>
    /// <typeparam name="K">属性对象</typeparam>
    public class ColumnBuilder<T, K>
    {
        ColumnDefinition column;
        internal ColumnBuilder(ColumnDefinition column)
        {
            this.column = column;
        }

        public ColumnBuilder<T, K> Name(string columnName)
        {
            this.column.ColumnName = columnName;
            return this;
        }

        public ColumnBuilder<T, K> Order(int? columnOrder)
        {
            this.column.Order = columnOrder;
            return this;
        }

        public ColumnBuilder<T, K> Type(string columnType)
        {
            this.column.Type = columnType;
            return this;
        }

        public ColumnBuilder<T, K> Generated()
        {
            this.column.Generated = true;
            return this;
        }

        public ColumnBuilder<T, K> IsOptional()
        {
            this.column.Optional = true;
            return this;
        }

        public ColumnBuilder<T, K> IsRequired()
        {
            this.column.Required = true;
            return this;
        }

        public void Ignore()
        {

        }


    }
}

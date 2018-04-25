using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Data.Schema;
using Epic.Components;

namespace Epic.Data.Expressions
{

    internal class OrderColumn
    {
        internal ColumnDefinition Column
        {
            get;
            set;
        }

        internal SortDirectionType Direction
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.Direction == SortDirectionType.Default ? this.Column.ColumnName : this.Column.ColumnName + this.Direction;
        }
    }

    internal class TranslateResult
    {
        internal TranslateResult()
        {
            this.Parameters = new List<string>();
            this.Values = new List<object>();
        }

        internal int Limit
        {
            get;
            set;
        }

        internal string LimitToken
        {
            get { return this.Limit > 0 ? "TOP " + this.Limit : String.Empty; }
        }

        internal List<ColumnDefinition> Columns
        {
            get;
            set;
        }

        internal string ColumnsToken
        {
            get
            {
                if (this.Columns == null || this.Columns.Count == 0) return String.Empty;
                return String.Join(", ", this.Columns.Select(e => e.ColumnName));
            }
        }

        internal int Skip
        {
            get;
            set;
        }

        internal List<TableDefinition> Tables
        {
            get;
            set;
        }

        internal string TablesToken
        {
            get
            {
                var tableNames = this.Tables.Select(e => e.TableName.ToString()).Distinct();
                return String.Join(",", tableNames);
            }
        }

        internal string Condition
        {
            get;
            set;
        }

        internal List<OrderColumn> Orders
        {
            get;
            set;
        }

        internal string OrderToken
        {
            get
            {
                if (this.Orders != null || this.Orders.Count > 0)
                    return String.Join(", ", this.Orders);

                var pk = this.Tables.Select(e => String.Join(",", e.PrimaryKeys.Select(x => x.ColumnName)));
                return String.Join(", ", pk);

            }
        }


        internal List<string> Parameters
        {
            get;
            set;
        }

        internal List<object> Values
        {
            get;
            set;
        }

        internal string CommandText
        {
            get
            {
                if (this.Limit > 0)
                    return String.Format("SELECT TOP {0} * FROM {1} {2}", this.Limit, this.Tables, this.Condition);
                return String.Format("SELECT * FROM {0} {1}", this.Tables, this.Condition);
            }

        }

        public override string ToString()
        {
            return this.CommandText;
        }
    }

}

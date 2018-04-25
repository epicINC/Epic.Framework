using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Epic.Extensions;

namespace Epic.Data.Schema
{
    public class ColumnDefinition
    {
        PropertyInfo property;


        internal ColumnDefinition(PropertyInfo value, TableDefinition table)
        {
            this.property = value;
            this.Table = table;
        }

        public PropertyInfo Property
        {
            get { return this.property; }
        }

        public string ColumnName
        {
            get;
            internal set;
        }

        
        public int? Order
        {
            get;
            internal set;
        }

        public string Type
        {
            get;
            internal set;
        }

        public bool Generated
        {
            get;
            internal set;
        }

        public byte Precision
        {
            get;
            internal set;
        }

        public bool Optional
        {
            get;
            internal set;
        }

        public bool Required
        {
            get;
            internal set;
        }

        public TableDefinition Table
        {
            get;
            set;
        }

    }
}

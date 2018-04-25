using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Data.Schema;
using System.Data;

namespace Epic.Data.V2
{




    public sealed class ObjectParameter
    {

        public ObjectParameter(string name, object value) : this(name, null, value)
        {
        }



        public ObjectParameter(string name, string source, object value) : this(name, null, ParameterDirection.Input , value)
        {
            this.parameterName = name;
            this.sourceColumn = source;
            this.value = value;
        }

        public ObjectParameter(string name, string source, ParameterDirection direction, object value)
        {
            this.parameterName = name;
            this.sourceColumn = source;
            this.direction = direction;
            this.value = value;
        }

        internal ObjectParameter(ColumnSchema source)
        {
            this.parameterName = "@" + source.DbName;
            this.sourceColumn = source.DbName;
            this.Source = source;
        }

        public ObjectParameter(string name, string source, DbType dbType, ParameterDirection direction, int size, object value)
        {
            this.parameterName = name;
            this.sourceColumn = source;
            this.dbType = dbType;
            this.direction = direction;
            this.size = size;
            this.value = value;
        }


        string parameterName;
        public string ParameterName
        {
            get { return this.parameterName; }
            set { this.parameterName = value; }
        }

        string sourceColumn;
        public string SourceColumn
        {
            get { return this.sourceColumn; }
            set { this.sourceColumn = value; }
        }

        internal ColumnSchema Source
        {
            get;
            private set;
        }

        DbType dbType;
        public DbType DbType
        {
            get { return this.dbType; }
            set { this.dbType = value; }
        }

        ParameterDirection direction;
        public ParameterDirection Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }

        bool isNullable;
        public bool IsNullable
        {
            get { return this.isNullable; }
            set { this.isNullable = value; }
        }


        int size;
        public int Size
        {
            get { return this.size; }
            set { this.size = value; }
        }


        object value;
        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

    }
}

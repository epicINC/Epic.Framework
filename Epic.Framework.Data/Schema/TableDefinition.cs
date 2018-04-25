using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Epic.Extensions;
using System.Linq.Expressions;
using Epic.Data.Schema.AttributeLoader;
using Epic.FluentAPI;

namespace Epic.Data.Schema
{

    internal static class TableDefinitionUtility
    {





    }

    public class TableDefinition
    {
        static Dictionary<Type, TableDefinition> Tables = new Dictionary<Type, TableDefinition>();


        internal static bool Exists(Type type)
        {
            return Tables.ContainsKey(type);
        }


        internal static TableDefinition Find(Type type)
        {
            TableDefinition result;
            if (!Tables.TryGetValue(type, out result))
            {
                result = new TableDefinition(type);
                Tables.Add(type, result);
            }
            return result;
        }

        internal TableDefinition(Type type)
        {

            this.Type = type;

            TableAttributeLoader.Init(this);
            ColumnAttributeLoader.Init(this);
        }

        public Type Type
        {
            get;
            internal set;
        }

        public TableName TableName
        {
            get;
            internal set;
        }

        public List<ColumnDefinition> PrimaryKeys
        {
            get;
            internal set;
        }


        public Dictionary<PropertyPath, ColumnDefinition> Columns
        {
            get;
            internal set;
        }


        internal ColumnDefinition Property(PropertyPath value)
        {
            ColumnDefinition result;
            if (!Columns.TryGetValue(value, out result))
                Columns.Add(value, result = new ColumnDefinition(value.Single(), this));
            return result;
        }

        internal ColumnDefinition Property(LambdaExpression lambdaExpression)
        {
            return Property(lambdaExpression.GetComplexPropertyAccess());
        }


        internal void Key(IEnumerable<PropertyInfo> value)
        {
            ClearKey();
            foreach (var item in value)
            {
                PrimaryKeys.Add(Property(new PropertyPath(item)));

            }
        }

        internal void ClearKey()
        {
            PrimaryKeys.Clear();
        }


    }

    internal static class TableDefinition<T>
    {
        static TableDefinition Table;

        static TableDefinition()
        {
            Table = TableDefinition.Find(typeof(T));
        }

        internal static void Init(string schema, string name)
        {
            Table.TableName = new TableName(schema, name);

        }

        public static TableName TableName
        {
            get { return Table.TableName; }
            private set { Table.TableName = value; }
        }

        public static List<ColumnDefinition> PrimaryKeys
        {
            get { return Table.PrimaryKeys; }
            private set { Table.PrimaryKeys = value; }
        }

        public static Dictionary<PropertyPath, ColumnDefinition> Columns
        {
            get { return Table.Columns; }
            private set { Table.Columns = value; }
        }



        internal static ColumnDefinition Property(PropertyPath value)
        {
            return Table.Property(value);
        }

        internal static ColumnDefinition Property(LambdaExpression lambdaExpression)
        {
            return Table.Property(lambdaExpression);
        }


        internal static void Key(IEnumerable<PropertyInfo> value)
        {
            Table.Key(value);
        }

        internal static void ClearKey()
        {
            Table.ClearKey();
        }

    }
}

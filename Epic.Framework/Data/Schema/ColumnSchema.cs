using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Epic.Extensions;
using System.Data;

namespace Epic.Data.Schema
{
    internal class ColumnSchema
    {


        internal static List<ColumnSchema> Parse(Type type)
        {
            var result = new List<ColumnSchema>();
            foreach (var item in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attr = item.GetCustomAttributes<ColumnSchemaAttribute>();
                if (attr != null)
                    result.Add(new ColumnSchema(attr, item));
            }
            foreach (var item in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attr = item.GetCustomAttributes<ColumnSchemaAttribute>();
                if (attr != null)
                    result.Add(new ColumnSchema(attr, item));
            }
            return result;
        }

        ColumnSchema(ColumnSchemaAttribute column, MemberInfo member)
        {
            this.Column = column;
            this.Member = member;
            this.Name = member.Name;
            this.DbName = String.IsNullOrWhiteSpace(column.Name) ? member.Name : column.Name;
            this.IsNull = column.IsNullable;
            this.IsPrimaryKey = column.IsPrimaryKey;
            this.IsDbGenerated = column.IsDbGenerated;
            

        }

        ColumnSchema(ColumnSchemaAttribute column, PropertyInfo member) : this(column, (MemberInfo)member)
        {
            this.Property = member;
            this.Type = member.PropertyType;
            this.SetMethod = member.GetSetMethod();
            this.GetMethod = member.GetGetMethod();
            ParseDbTypeSize(column, this.Type);
        }

        ColumnSchema(ColumnSchemaAttribute column, FieldInfo member) : this(column, (MemberInfo)member)
        {
            this.Field = member;
            this.Type = member.FieldType;
            ParseDbTypeSize(column, this.Type);
        }


        void ParseDbTypeSize(ColumnSchemaAttribute column, MemberInfo member)
        {
            
            if (!String.IsNullOrWhiteSpace(column.DbType))
            {
                string result;
                var offset = column.DbType.IndexOf('(');
                if (offset > 0)
                {
                    result = column.DbType.Substring(0, offset);
                    this.Size = column.DbType.Substring(offset + 1, column.DbType.Length - offset - 2).ToInt();
                }
                else
                    result = column.DbType;

                this.DbType = Epic.Data.DbTypeConverter.ToDbType(result);
            }
            else
            {
                this.DbType = Epic.Data.DbTypeConverter.ToDbType(this.Type);
            }
        }




    
 

        
        public string Name { get; private set; }
        public string DbName { get; private set; }
        public System.Data.DbType DbType { get; private set; }

        public int Size {get;private set;}
        public Type Type { get; private set; }

        internal PropertyInfo Property { get; private set; }
        internal FieldInfo Field { get; private set; }

        public MethodInfo SetMethod { get; private set; }
        public MethodInfo GetMethod { get; private set; }
        

        public bool IsPrimaryKey { get; private set; }
        public bool IsNull { get; private set; }
        public bool IsDbGenerated { get; private set; }


        internal ColumnSchemaAttribute Column { get; private set; }

        internal MemberInfo Member { get; private set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Epic.Data.Schema
{

    internal class TableSchema
    {
        Type type;
        string fullName;
        string fullDBName;
        string sPFormat;
        TableSchemaAttribute attribute;

        static Dictionary<Type, TableSchema> current = new Dictionary<Type, TableSchema>();

        internal static TableSchema Get(Type key)
        {
            TableSchema result;
            if (!current.TryGetValue(key, out result))
            {
                result = Init(key);
                current.Add(key, result);
            }
            return result;
        }

        static TableSchema Init(Type type)
        {
            var result = new TableSchema();
            result.type = type;
            result.attribute = type.GetCustomAttributes<TableSchemaAttribute>();

            if (result.attribute == null)
                result.attribute = new TableSchemaAttribute(type.Name);


            if (result.attribute.HasSchema)
            {
                result.fullName = result.attribute.Schema + "." + result.attribute.Name;
                result.fullDBName = "[" + result.attribute.Schema + "].[" + result.attribute.Name + "]";
                result.sPFormat = "[{3}].[{0}{1}{2}]";
            }
            else
            {
                result.fullName = result.attribute.Name;
                result.fullDBName = "[" + result.attribute.Name + "]";
                result.sPFormat = "[{0}{1}{2}]";
            }
            result.Columns = ColumnSchema.Parse(type).AsReadOnly();
            result.PrimaryKeys = result.Columns.Where(e => e.IsPrimaryKey).ToList().AsReadOnly();
            return result;
        }


        public string Schema
        {
            get { return attribute.Schema; }
        }

        public string Name
        {
            get { return attribute.Name; }
        }

        public string FullName
        {
            get { return fullName; }
        }

        public string FullDBName
        {
            get { return fullDBName; }
        }

        string SPFormat
        {
            get { return SPFormat; }
        }


        public string SPSelectName
        {
            get { return String.Format(SPFormat, attribute.Name, "SelectBy", "ID", attribute.Schema); }
        }

        public string SPInsertName
        {
            get { return String.Format(SPFormat, attribute.Name, "Insert", String.Empty, attribute.Schema); }
        }

        public string SPUpdateName
        {
            get { return String.Format(SPFormat, attribute.Name, "UpdateBy", "ID", attribute.Schema); }
        }

        public string SPDeleteName
        {
            get { return String.Format(SPFormat, attribute.Name, "DeleteBy", "ID", attribute.Schema); }
        }


        public ReadOnlyCollection<ColumnSchema> PrimaryKeys { get; private set; }
        public ReadOnlyCollection<ColumnSchema> Columns { get; private set; }
    }

    internal static class TableSchema<T>
    {
        static Type type;
        static string fullName;
        static string fullDBName;
        static string sPFormat;
        static TableSchemaAttribute attribute;

        static TableSchema()
        {
            type = typeof(T);
            var attribute = type.GetCustomAttributes<TableSchemaAttribute>();

            if (attribute == null)
                attribute = new TableSchemaAttribute(type.Name);


            if (attribute.HasSchema)
            {
                fullName = attribute.Schema + "." + attribute.Name;
                fullDBName = "[" + attribute.Schema + "].[" + attribute.Name + "]";
                sPFormat = "[{3}].[{0}{1}{2}]";
            }
            else
            {
                fullName = attribute.Name;
                fullDBName = "["+ attribute.Name +"]";
                sPFormat = "[{0}{1}{2}]";
            }

            Columns = ColumnSchema.Parse(type).AsReadOnly();
            CustomColumns = Columns.Where(e => e.Name != e.DbName).ToList().AsReadOnly();

            DbGeneratedColumns = Columns.Where(e => e.IsDbGenerated).ToList().AsReadOnly();
            NoneDbGeneratedColumns = Columns.Where(e => !e.IsDbGenerated).ToList().AsReadOnly();
            ColumnDictionary = Columns.ToDictionary(e => e.Property.Name);
            PrimaryKeys = Columns.Where(e => e.IsPrimaryKey).ToList().AsReadOnly();
            
        }

        static void Init()
        {

        }


        public static string Schema
        {
            get { return attribute.Schema; }
        }

        public static string Name
        {
            get { return attribute.Name; }
        }

        public static string FullName
        {
            get { return fullName; }
        }

        public static string FullDBName
        {
            get { return fullDBName; }
        }

        static string SPFormat
        {
            get { return SPFormat; }
        }


        public static string SPSelectName
        {
            get { return String.Format(SPFormat, attribute.Name, "SelectBy", "ID", attribute.Schema); }
        }

        public static string SPInsertName
        {
            get { return String.Format(SPFormat, attribute.Name, "Insert", String.Empty, attribute.Schema); }
        }

        public static string SPUpdateName
        {
            get { return String.Format(SPFormat, attribute.Name, "UpdateBy", "ID", attribute.Schema); }
        }

        public static string SPDeleteName
        {
            get { return String.Format(SPFormat, attribute.Name, "DeleteBy", "ID", attribute.Schema); }
        }

        /// <summary>
        /// 主键
        /// </summary>
        public static ReadOnlyCollection<ColumnSchema> PrimaryKeys { get; private set; }

        /// <summary>
        /// 所有列
        /// </summary>
        public static ReadOnlyCollection<ColumnSchema> Columns { get; private set; }

        public static ReadOnlyCollection<ColumnSchema> CustomColumns { get; private set; }

        /// <summary>
        /// 非数据库生成列
        /// </summary>
        public static ReadOnlyCollection<ColumnSchema> DbGeneratedColumns { get; private set; }

        /// <summary>
        /// 非数据库生成列
        /// </summary>
        public static ReadOnlyCollection<ColumnSchema> NoneDbGeneratedColumns { get; private set; }

        /// <summary>
        /// 所有列 名称 字典
        /// </summary>
        public static Dictionary<string, ColumnSchema> ColumnDictionary { get; private set; }

    }

}

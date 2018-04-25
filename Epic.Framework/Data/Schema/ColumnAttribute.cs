using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Epic.Data.Schema
{
    // 摘要:
    //     指示运行时如何在执行插入或更新操作后检索值。
    public enum AutoSync
    {
        // 摘要:
        //     自动选择值。
        Default = 0,
        //
        // 摘要:
        //     始终返回值。
        Always = 1,
        //
        // 摘要:
        //     从不返回值。
        Never = 2,
        //
        // 摘要:
        //     仅在执行插入操作后返回值。
        OnInsert = 3,
        //
        // 摘要:
        //     仅在执行更新操作后返回值。
        OnUpdate = 4,
    }

    public class ColumnSchemaAttribute : Attribute
    {

        public ColumnSchemaAttribute()
        {
        }


        public ColumnSchemaAttribute(bool isNullable) : this(null, null, isNullable)
        {

        }

        public ColumnSchemaAttribute(string name, string dbType)
        {
        }

        public ColumnSchemaAttribute(string name, string dbType, bool isNullable) : this(name, dbType, AutoSync.Default, false, false, isNullable)
        {
        }


        public ColumnSchemaAttribute(string name, string dbType, AutoSync autoSync, bool isPrimaryKey, bool isDbGenerated, bool isNullable)
        {
            this.Name = name;
            this.DbType = dbType;
            this.AutoSync = autoSync;
            this.IsPrimaryKey = isPrimaryKey;
            this.IsDbGenerated = isDbGenerated;
            this.IsNullable = isNullable;
        }



        public string Name { get; set; }
        public string DbType { get; set; }
        public AutoSync AutoSync { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsDbGenerated { get; set; }
        public bool IsNullable { get; set; }

    }
}

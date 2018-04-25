using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Epic.Data.Mapper
{
    public abstract class DbTypeDefinition
    {
        internal BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;


        #region Base
        
        internal Type Boolean = typeof(bool);
        internal Type SByte = typeof(sbyte);
        internal Type UInt16 = typeof(UInt16);
        internal Type UInt32 = typeof(UInt32);
        internal Type UInt64 = typeof(UInt64);
        internal Type Single = typeof(Single);
        internal Type Double = typeof(Double);
        internal Type Int = typeof(int);

        internal Type String = typeof(string);
        internal Type Object = typeof(object);
        internal Type Guid = typeof(Guid);
        internal Type ByteArray = typeof(byte[]);
        internal Type DateTimeOffset = typeof(DateTimeOffset);

        #endregion


        #region Command

        internal Type Command;
        internal Type ParameterCollection;
        internal Type Parameter;

        internal MethodInfo GetParameters;
        internal MethodInfo ExecuteNonQuery;
        internal MethodInfo ParameterCollectionGetItem;
        internal MethodInfo ParameterGetValue;
        internal MethodInfo ParameterSetValue;

        #endregion

        #region Reader
        
        internal Type Reader;
        internal MethodInfo FieldCount;
        internal MethodInfo GetBoolean;
        internal MethodInfo GetByte;
        internal MethodInfo GetChar;
        internal MethodInfo GetDataTypeName;
        internal MethodInfo GetDateTime;
        internal MethodInfo GetDecimal;
        internal MethodInfo GetDouble;
        internal MethodInfo GetFieldType;
        internal MethodInfo GetFloat;
        internal MethodInfo GetGuid;
        internal MethodInfo GetInt16;
        internal MethodInfo GetInt32;
        internal MethodInfo GetInt64;
        internal MethodInfo GetName;
        internal MethodInfo GetOrdinal;
        internal MethodInfo GetString;
        internal MethodInfo GetValue;
        internal MethodInfo IsDBNull;

        #endregion

        protected void InitCommand()
        {
            this.GetParameters = this.Command.GetMethod("get_Parameters", this.Flags, null, new Type[] { }, null);
            this.ExecuteNonQuery = this.Command.GetMethod("ExecuteNonQuery", this.Flags, null, new Type[] { }, null);

            this.ParameterCollectionGetItem = this.ParameterCollection.GetMethod("get_Item", this.Flags, null, new Type[] { this.String }, null);

            this.ParameterGetValue = this.Parameter.GetMethod("get_Value", this.Flags, null, new Type[] { }, null);
            this.ParameterSetValue = this.Parameter.GetMethod("set_Value", this.Flags, null, new Type[] { this.Object }, null);
        }

        protected void InitReader()
        {
            this.GetBoolean = this.Reader.GetMethod("GetBoolean", this.Flags, null, new Type[] { this.Int }, null);
            this.GetByte = this.Reader.GetMethod("GetByte", this.Flags, null, new Type[] { this.Int }, null);
            this.GetChar = this.Reader.GetMethod("GetChar", this.Flags, null, new Type[] { this.Int }, null);
            this.GetDataTypeName = this.Reader.GetMethod("GetDataTypeName", this.Flags, null, new Type[] { this.Int }, null);
            this.GetDateTime = this.Reader.GetMethod("GetDateTime", this.Flags, null, new Type[] { this.Int }, null);
            this.GetDecimal = this.Reader.GetMethod("GetDecimal", this.Flags, null, new Type[] { this.Int }, null);
            this.GetDouble = this.Reader.GetMethod("GetDouble", this.Flags, null, new Type[] { this.Int }, null);
            this.GetFieldType = this.Reader.GetMethod("GetFieldType", this.Flags, null, new Type[] { this.Int }, null);
            this.GetFloat = this.Reader.GetMethod("GetFloat", this.Flags, null, new Type[] { this.Int }, null);
            this.GetGuid = this.Reader.GetMethod("GetGuid", this.Flags, null, new Type[] { this.Int }, null);
            this.GetInt16 = this.Reader.GetMethod("GetInt16", this.Flags, null, new Type[] { this.Int }, null);
            this.GetInt32 = this.Reader.GetMethod("GetInt32", this.Flags, null, new Type[] { this.Int }, null);
            this.GetInt64 = this.Reader.GetMethod("GetInt64", this.Flags, null, new Type[] { this.Int }, null);
            this.GetName = this.Reader.GetMethod("GetName", this.Flags, null, new Type[] { this.Int }, null);
            this.GetOrdinal = this.Reader.GetMethod("GetOrdinal", this.Flags, null, new Type[] { this.String }, null);
            this.GetString = this.Reader.GetMethod("GetString", this.Flags, null, new Type[] { this.Int }, null);
            this.GetValue = this.Reader.GetMethod("GetValue", this.Flags, null, new Type[] { this.Int }, null);
            this.IsDBNull = this.Reader.GetMethod("IsDBNull", this.Flags, null, new Type[] { this.Int }, null);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Epic.Data.Schema;

namespace Epic.Data.V2
{
    public class ObjectParameterDictionary : Dictionary<string, ObjectParameter>
    {
        public ObjectParameter Add(string name, DbType dbType, ParameterDirection direction)
        {
            var result = new ObjectParameter(name, null, dbType, direction, 0, null);
            base.Add(name, result);
            return result;
        }

        public ObjectParameter Add(string name, object value)
        {
            var result = new ObjectParameter(name, value);
            base.Add(name, result);
            return result;
        }

        public ObjectParameter Add(string name, string source, object value)
        {
            var result = new ObjectParameter(name, source, value);
            base.Add(name, result);
            return result;
        }

        internal ObjectParameter  Add(ColumnSchema source)
        {
            var result = new ObjectParameter(source);
            base.Add(result.ParameterName, result);
            return result;
        }

        public void Fill<T>(DbCommand command)
        {
            var dic = TableSchema<T>.ColumnDictionary;

            foreach (var item in this.Values)
            {
                var column = QueryColumn(dic, item.SourceColumn);
                var parameter = new System.Data.SqlClient.SqlParameter();
                parameter.ParameterName = item.ParameterName;
                parameter.Direction = item.Direction;
                parameter.Value = item.Value;
                if (column != null)
                {
                    parameter.DbType = column.DbType;
                    parameter.Size = column.Size;

                    //switch (column.DbType)
                    //{
                    //    case "varchar":
                    //    case "nvarchar":
                    //    case "char":
                    //    case "nchar":
                    //        parameter.Size = column.Size;
                    //        break;
                    //    default:
                    //        break;
                    //}


                }
                else
                {
                    if (item.Value == null)
                        parameter.DbType = item.DbType;
                }

                command.Parameters.Add(parameter);

            }
        }

        public void FillValue(DbCommand command)
        {
            foreach (var item in this.Values.Where(e => e.Direction != ParameterDirection.Input))
            {
                item.Value = command.Parameters[item.ParameterName].Value;
            }
        }

        bool HasOutput
        {
            get
            {
                return this.Any(e => e.Value.Direction != ParameterDirection.Input);
            }
        }

        ColumnSchema QueryColumn(Dictionary<string, ColumnSchema> dic , string key)
        {
            if (String.IsNullOrWhiteSpace(key)) return null;
            ColumnSchema column;
            dic.TryGetValue(key, out column);
            return column;
        }


        public void Merge(IDictionary<string, ObjectParameter> attributes)
        {
            bool replaceExisting = false;
            if (attributes == null) return;

            foreach (KeyValuePair<string, ObjectParameter> pair in attributes)
            {
                this.Merge(pair.Key, pair.Value, replaceExisting);
            }


        }

        public void Merge(string key, object value)
        {
            this.Merge(key, new ObjectParameter(key, value), false);
        }



        public void Merge(string key, ObjectParameter value, bool replaceExisting)
        {
            if (String.IsNullOrEmpty(key))
                Error.ArgumentNull("key");

            if (replaceExisting || !this.ContainsKey(key))
            {
                this[key] = value;
            }
        }


    }
}

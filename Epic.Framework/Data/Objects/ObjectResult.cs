using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Data.Common;
using Epic.Components;

namespace Epic.Data.Objects
{
    public abstract class ObjectResult : IEnumerable, IDisposable
    {


        public abstract void Dispose();
        internal abstract IEnumerator GetEnumeratorInternal();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumeratorInternal();
        }


        public abstract Type ElementType { get; }
    }


    public class ObjectResult<T> : ObjectResult, IEnumerable<T>
    {
        DbDataReader reader;
        ObjectContext context;


        internal ObjectResult(ObjectContext context, string commondText, List<object> parameters)
        {
            this.context = context;

            context.EnsureConnection();


            var command = context.Connection.CreateCommand();
            command.CommandText = commondText;
            command.CommandType = System.Data.CommandType.Text;
            for (int i = 0; i < parameters.Count; i++)
            {
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@p" + i, parameters[i])); 
            }
            this.reader = command.ExecuteReader();
        }

        public override void Dispose()
        {
            if (this.reader != null)
                this.reader.Dispose();
            this.reader = null;
            this.context.ReleaseConnection();
            this.context = null;
        }

        public bool IsSimple
        {
            get
            {
                switch (Type.GetTypeCode(typeof(T)))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Byte:
                    case TypeCode.Char:
                    case TypeCode.DBNull:
                    case TypeCode.DateTime:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Empty:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.SByte:
                    case TypeCode.Single:
                    case TypeCode.String:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        return true;
                        break;
                    case TypeCode.Object:
                        break;
                        return false;
                    default:
                        return false;
                        break;
                }
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.IsSimple)
                return new SimpleEnumerator<T>(this.reader);
            return Emit.EmitReader.GetEnumerator<T>(this.reader).GetEnumerator();
        }

        internal override IEnumerator GetEnumeratorInternal()
        {
            return this.GetEnumerator();
        }

        public override Type ElementType
        {
            get { return typeof(T); }
        }

        /*
        private class SimpleEnumerator : IEnumerator<T>, IDisposable, IEnumerator
        {

            ObjectResult<T> result;
            T current;
            internal SimpleEnumerator(ObjectResult<T> result)
            {
                this.result = result;
            }

            public T Current
            {
                get { return this.current; }
            }

            public void Dispose()
            {
                this.result.reader.Close();
                this.result.context.ReleaseConnection();
            }

            object IEnumerator.Current
            {
                get { return this.current; }
            }

            public bool MoveNext()
            {
                if (this.result.reader.Read())
                {
                    return true;
                }
                else
                {
                    this.Dispose();
                    return false;
                }
                
               
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
         */

    }
}

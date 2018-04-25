using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Linq.Expressions;
using System.Collections;

namespace Epic.Data.V2
{
    public class ObjectQuery<T> : IObjectQuery<T>
    {


        internal ObjectQuery(ObjectDataProvider<T> provider)
        {
            this.provider = provider;
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


        ObjectDataProvider<T> provider;
        public ObjectDataProvider<T> Provider

        {
            get { return this.provider; }
            set { this.provider = value; }
        }

        string commandText;
        public string CommandText
        {
            get
            {
                if (String.IsNullOrWhiteSpace(this.commandText))
                {
                    this.commandText = this.Builder.ToString();
                    this.ParameterData.Merge(this.Builder.Parameters);


                }
                return this.commandText;
            }
            set { this.commandText = value; }
        }

        CommandType commandType;
        public CommandType CommandType
        {
            get { return this.commandType; }
            set { this.commandType = value; }
        }

        ObjectQueryBuilder<T> builder;
        public ObjectQueryBuilder<T> Builder
        {
            get
            {
                if (this.builder == null)
                    this.builder = new ObjectQueryBuilder<T>();
                return this.builder;
            }
        }

        public Expression<Func<T, bool>> Expression
        {
            get { return this.builder.Expression; }
        }



        public IEnumerator<T> GetEnumerator()
        {
            return this.provider.ExecuteInternal(this).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        DynamicObjectParameterDictionary parameterBag;
        public dynamic ParameterBag
        {
            get
            {
                if (this.parameterBag == null)
                    this.parameterBag = new DynamicObjectParameterDictionary(() => this.ParameterData);
                return this.parameterBag;
            }
        }

        ObjectParameterDictionary parameterData;
        public ObjectParameterDictionary ParameterData
        {
            get
            {
                if (this.parameterData == null)
                    this.parameterData = new ObjectParameterDictionary();
                return this.parameterData;
            }

        }

        #region Param

        PagingParam param;

        public PagingParam Param
        {
            get
            {
                if (this.param == null)
                    this.param = new PagingParam();
                return this.param;
            }
        }

        #endregion

    }
}

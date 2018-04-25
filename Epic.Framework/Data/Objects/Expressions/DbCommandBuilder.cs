using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.Data.Schema;

namespace Epic.Data.Objects.Expressions
{
    internal class DbCommandBuilder
    {
        #region Static 

        static Dictionary<ExpressionType, string> operators;
        static Dictionary<string, string> methods;

        static DbCommandBuilder()
        {
            operators = new Dictionary<ExpressionType, string>();
            operators.Add(ExpressionType.Equal, "=");
            operators.Add(ExpressionType.NotEqual, "<>");
            operators.Add(ExpressionType.GreaterThan, ">");
            operators.Add(ExpressionType.GreaterThanOrEqual, ">=");
            operators.Add(ExpressionType.LessThan, "<");
            operators.Add(ExpressionType.LessThanOrEqual, "<=");
            operators.Add(ExpressionType.AndAlso, "AND");
            operators.Add(ExpressionType.OrElse, "OR");
            operators.Add(ExpressionType.Add, "+");
            operators.Add(ExpressionType.Subtract, "-");
            operators.Add(ExpressionType.Multiply, "*");
            operators.Add(ExpressionType.Divide, "/");

            methods = new Dictionary<string, string>();
            methods.Add("StartsWith", "({0} Like {1}+'%')");
            methods.Add("EndsWith", "({0} Like '%'{1})");
            methods.Add("Contains", "({0} Like '%'+{1}+'%')");
        }

        #endregion

        Type sourceType;
        Stack<string> containers = new Stack<string>();
        List<object> parameters = new List<object>();

        public string Push(string value)
        {
            this.containers.Push(value);
            return value;
        }

        public object PushValue(object value)
        {

            this.containers.Push("@p"+ this.parameters.Count);
            this.parameters.Add(value);
            return value;
        }



        public string Flush(MethodCallExpression node)
        {
            var right = this.containers.Pop();
            var left = this.containers.Pop();

            return this.Push(String.Format(methods[node.Method.Name], left, right));
        }

        public string Flush(BinaryExpression node)
        {
            this.hasWhere = true;
            var right = this.containers.Pop();
            var left = this.containers.Pop();
            return this.Push(String.Format("({1} {0} {2})", operators[node.NodeType], left, right));

        }




        public int FlushTop()
        {
            this.containers.Pop();
            return this.topParts = (int)this.parameters.Last();
        }

        public bool FlushCount()
        {
            return this.hasCount = true;
        }

        public Type PushSource(ConstantExpression node)
        {
            return this.sourceType = node.Type.GetGenericArguments()[0];
        }




        public string OrderPop(string sort = "ASC")
        {
            var result = this.containers.Pop();
            this.OrderByParts[result] = sort;
            return result + " " + sort;
        }


        public string CommandText
        {
            get
            {
                return this.FormatCommandText();
            }
        }

        public List<object> Parameters
        {
            get { return this.parameters; }
        }


        #region Count

        bool hasCount;
        public bool HasCount
        {
            get { return this.hasCount; }
        }

        public string Count
        {
            get
            {
                if (this.hasCount)
                    return " Count(*) ";
                return String.Empty;
            }
        }

        #endregion


        #region Top

        int topParts;

        public bool HasTop
        {
            get { return this.topParts > 0; }
        }

        public string Top
        {
            get
            {
                if (!this.HasCount && this.HasTop)
                    return String.Format(" TOP({0}) ", this.topParts);
                return String.Empty;
            }
        }

        #endregion

        #region Column

        List<string> columnParts;
        public List<string> ColumnParts
        {
            get
            {
                if (this.columnParts == null)
                    this.columnParts = new List<string>();
                return this.columnParts;
            }

        }

        public bool HasColumn
        {
            get { return this.columnParts != null && this.columnParts.Count > 0; }
        }

        public string Column
        {
            get
            {
                if (this.HasCount) return String.Empty;
                if (this.HasColumn) return String.Join(", ", this.columnParts);
                return "*";
            }
        }

        #endregion

        #region Where

        bool hasWhere;

        public bool HasWhere
        {
            get { return this.hasWhere; }
        }

        public string Where
        {
            get
            {
                if (this.hasWhere)
                {
                    if (this.containers.Count == 0)
                        return "Where " + this.containers.Pop();
                    return "Where " + String.Join(" And ", this.containers.Reverse().ToList());
                }
                    
                return String.Empty;
            }
            set
            {
                this.containers.Push(value);
            }
        }

        #endregion

        #region Order

        Dictionary<string, string> orderByParts;
        Dictionary<string, string> OrderByParts
        {
            get
            {
                if (this.orderByParts == null)
                    this.orderByParts = new Dictionary<string, string>();
                return this.orderByParts;
            }
        }

        public bool HasOrder
        {
            get { return this.orderByParts != null && this.orderByParts.Count > 0; }
        }

        public string Order
        {
            get
            {
                if (!this.HasCount && this.HasOrder) return "Order BY " +String.Join(", ", this.orderByParts.Select(e => e.Key + " " + e.Value));
                return String.Empty;
            }
        }

        #endregion

        public string Table
        {
            get { return TableSchema.Get(this.sourceType).FullDBName; }
        }


        string FormatCommandText()
        {
            var sql = "Select {0} {1} From {2} {3} {4}";
            return String.Format(sql, this.Count, this.Column, this.Table, this.Where, this.Order);

        }

        /* 
        string ToCommandText()
        {
            var sb = new StringBuilder();
            sb.Append("Select ");
            if (!String.IsNullOrWhiteSpace(this.topParts))
                sb.Append(this.topParts);

            if (this.isCount)
            {
                sb.Append(" Count(*) ");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(this.columnParts))
                    sb.Append(" * ");
                else
                    sb.Append(" " + this.columnParts + " ");
            }


            sb.Append(" From BetaUN_RssItems");
            if (this.containers.Count > 0)
            {
                sb.Append(" Where ");
                sb.Append(this.containers.Pop());
            }

            if (!this.isCount && this.orderByParts != null && this.OrderByParts.Count > 0)
            {
                sb.Append(" Order BY ");
                sb.Append(String.Join(", ", this.orderByParts));
            }
            sb.Append(";");
            /*
            if (this.parameters.Count > 0)
            {
                for (int i = 0; i < this.parameters.Count; i++)
                {
                    sb.AppendFormat("@p{0} {1}, ", i, this.parameters[i]);
                    
                }
            }
    

            return sb.ToString();

        }
        */

    }
}

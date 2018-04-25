using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Epic.Data.Objects.Expressions
{
    internal class SqlBuilder
    {
        #region Static 

        static Dictionary<ExpressionType, string> operators;
        static Dictionary<string, string> methods;

        static SqlBuilder()
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


        internal Stack<string> containers = new Stack<string>();
        internal List<object> parameters = new List<object>();

        public string Push(string node)
        {
            this.containers.Push(node);
            return node;
        }

        public string Push(MethodCallExpression node)
        {
            var right = this.containers.Pop();
            var left = this.containers.Pop();
            return this.Push(String.Format(methods[node.Method.Name], left, right));
        }

        public string Push(BinaryExpression node)
        {
            var right = this.containers.Pop();
            var left = this.containers.Pop();
            return this.Push("(" + left + " " + operators[node.NodeType] + " " + right +")");
        }


        public string Push(MemberExpression node)
        {
            return this.Push("[" + node.Member.Name + "]");
        }

        public object Push(ConstantExpression node)
        {
            this.parameters.Add(node.Value);
            return this.Push("@p" + (this.parameters.Count - 1));
        }



        string topParts;
        bool isCount;
        string selectParts;


        List<string> orderByParts;
        List<string> OrderByParts
        {
            get
            {
                if (this.orderByParts == null)
                    this.orderByParts = new List<string>();
                return this.orderByParts;
            }
        }

        public string TopPop()
        {
            return this.topParts = "TOP(" + this.containers.Pop() + ")";
        }

        public bool CountPop()
        {
            return this.isCount = true;
        }

        public string OrderPop(string sort = "ASC")
        {
            this.OrderByParts.Add(this.containers.Pop() +" "+ sort);
            return null;
        }



        public string ToQuery()
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
                if (String.IsNullOrWhiteSpace(this.selectParts))
                    sb.Append(" * ");
                else
                    sb.Append(" " + this.selectParts + " ");
            }


            sb.Append(" From [table]");
            if (this.containers.Count > 0)
            {
                sb.Append(" Where ");
                sb.Append(this.containers.Pop());
            }

            if (this.orderByParts != null && this.OrderByParts.Count > 0)
            {
                sb.Append(" Order BY ");
                sb.Append(String.Join(", ", this.orderByParts));
            }
            sb.Append(";");
            if (this.parameters.Count > 0)
            {
                for (int i = 0; i < this.parameters.Count; i++)
                {
                    sb.AppendFormat("@p{0} {1}, ", i, this.parameters[i]);
                    
                }
            }

            return sb.ToString();

        }

    }
}

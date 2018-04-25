using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.Extensions;
using Epic.Data.Schema;

namespace Epic.Data.Expressions
{

    internal class QueryTranslator : ExpressionVisitor
    {


        TranslateResult result;

        /// <summary>
        /// 条件
        /// </summary>
        Stack<string> conditions = new Stack<string>();

        IEnumerable<ColumnDefinition> schemas;

        internal TranslateResult Translate(Expression expression, IEnumerable<ColumnDefinition> schemas = null)
        {
            this.result = new TranslateResult();
            this.schemas = schemas;
            this.Visit(expression);

            return this.result;
        }

        bool HasSchema
        {
            get
            {
                return this.schemas != null && this.schemas.Count() > 0;
            }
        }


        static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = (e as UnaryExpression).Operand;
            }
            return e;
        }

        protected override Expression VisitMethodCall(MethodCallExpression exp)
        {
            



            switch (exp.Method.Name)
            {
                case "Take" :
                    base.VisitMethodCall(exp);
                    this.result.Limit = (int)this.result.Values.Last();
                    this.result.Values.RemoveLast();
                    break;
                case "Skip" :
                    base.VisitMethodCall(exp);
                    this.result.Skip = (int)this.result.Values.Last();
                    this.result.Values.RemoveLast();
                    break;
                case "Like" :
                case "Contains" :
                    base.VisitMethodCall(exp);
                    this.Push("({0} Like '%'+{1}+'%')");
                    break;
                case "StartsWith":
                    base.VisitMethodCall(exp);
                    this.Push("({0} Like {1}+'%')");
                    break;
                case "EndsWith":
                    base.VisitMethodCall(exp);
                    this.Push("({0} Like '%'+{1})");
                    break;
                case "Where" :
                    base.VisitMethodCall(exp);
                    // if (exp.Method.DeclaringType == typeof(Queryable))
                    this.result.Condition = "WHERE "+ this.conditions.Pop();
                    break;
                case "Select":
                    var lambda = (LambdaExpression)StripQuotes(exp.Arguments[1]);
                    new ColumnProjector().Translate(lambda.Body);
                    break;
                case "In":
                    base.VisitMethodCall(exp);
                    object o = this.result.Values.Last();
                    this.result.Values.RemoveLast();
                    var left = this.conditions.Pop();
                    this.conditions.Push(String.Format("({0} In ({1}))", left, ObjectToString(o)));
                    break;
                default:
                    break;
            }
            return exp;
        }

        void Push(string format)
        {
            var right = this.conditions.Pop();
            var left = this.conditions.Pop();
            this.conditions.Push(String.Format(format, left, right));
        }

        protected override Expression VisitBinary(BinaryExpression exp)
        {
            if (exp == null) return exp;

            string opr;
            switch (exp.NodeType)
            {
                case ExpressionType.Equal:
                    opr = "=";
                    break;
                case ExpressionType.NotEqual:
                    opr = "<>";
                    break;
                case ExpressionType.GreaterThan:
                    opr = ">";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    opr = ">=";
                    break;
                case ExpressionType.LessThan:
                    opr = "<";
                    break;
                case ExpressionType.LessThanOrEqual:
                    opr = "<=";
                    break;
                case ExpressionType.AndAlso:
                    opr = "AND";
                    break;
                case ExpressionType.OrElse:
                    opr = "OR";
                    break;
                case ExpressionType.Add:
                    opr = "+";
                    break;
                case ExpressionType.Subtract:
                    opr = "-";
                    break;
                case ExpressionType.Multiply:
                    opr = "*";
                    break;
                case ExpressionType.Divide:
                    opr = "/";
                    break;
                default:
                    throw new NotSupportedException(exp.NodeType + "is not supported.");
            }

            this.Visit(exp.Left);
            this.Visit(exp.Right);

            string right = this.conditions.Pop();
            string left = this.conditions.Pop();

            this.conditions.Push(String.Format("({0} {1} {2})", left, opr, right));

            return exp;
        }


        protected override Expression VisitConstant(ConstantExpression exp)
        {
            var q = exp.Value as IQueryable;
            if (q != null)
            {
                var table = Epic.Data.Schema.TableDefinition.Find(q.ElementType);
                if (table == null)
                    this.result.Tables.Add(TableDefinition.Find(q.ElementType));
                else
                    this.result.Tables.Add(table);
            }
            else
            {
                this.result.Values.Add(exp.Value);
                this.conditions.Push(String.Format("@p{0}", this.result.Values.Count - 1));
            }
            return exp;
        }


        protected override Expression VisitMemberAccess(MemberExpression exp)
        {
            if (exp.Expression != null && exp.Expression.NodeType == ExpressionType.Parameter)
            {
                var name = exp.Member.Name;
                if (this.HasSchema)
                {
                    var column = this.schemas.SingleOrDefault(e => e.Property.Name == name);
                    if (column != null) name = column.ColumnName;
                }

                this.conditions.Push(String.Format("[{0}]", name));
                this.result.Parameters.Add(exp.Member.Name);

                return exp;
            }
            throw new NotSupportedException(string.Format("The member '{0}' is not supported", exp.Member.Name));
        }



        static string ObjectToString(object o)
        {

            if (o is IEnumerable<string>)
                return "'" + String.Join("', '", o as IEnumerable<string>) + "'";
            else if (o is IEnumerable<int>)
                return String.Join(", ", o as IEnumerable<int>);
            else
                return String.Join(", ", o as IEnumerable<object>);
        }

    }
}

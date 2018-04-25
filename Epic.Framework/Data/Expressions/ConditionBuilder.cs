using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Epic.Data.Expressions
{
    /// <summary>
    /// 如何：实现表达式目录树访问器
    /// http://msdn.microsoft.com/zh-cn/library/bb882521.aspx
    /// </summary>
    internal class ConditionBuilder : ExpressionVisitor
    {
        List<object> m_arguments;
        Stack<string> m_conditionParts;
        Stack<string> filter;

        public string Condition { get; private set; }

        public object[] Arguments { get; private set; }

        public string Filter { get; private set; }

        public void Build(Expression expression)
        {
            PartialEvaluator evaluator = new PartialEvaluator();
            Expression evaluatedExpression = evaluator.Eval(expression);

            this.m_arguments = new List<object>();
            this.m_conditionParts = new Stack<string>();
            this.filter = new Stack<string>();

            this.Visit(evaluatedExpression);

            this.Arguments = this.m_arguments.ToArray();
            this.Condition = this.m_conditionParts.Count > 0 ? this.m_conditionParts.Pop() : null;

            this.Filter = this.filter.Count > 0 ? this.filter.Pop() : null;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b == null) return b;

            string opr;
            switch (b.NodeType)
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
                case ExpressionType.And:
                    opr = "&";
                    break;
                default:
                    throw new NotSupportedException(b.NodeType + "is not supported.");
            }

            this.Visit(b.Left);
            this.Visit(b.Right);

            string right = this.m_conditionParts.Pop();
            string left = this.m_conditionParts.Pop();

            string right1 = this.filter.Pop();
            string left1 = this.filter.Pop();

            string condition = String.Format("({0} {1} {2})", left, opr, right);
            this.m_conditionParts.Push(condition);

            this.filter.Push("("+ left1 + " " + opr + " " + right1 +")");

            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c == null) return c;

            this.m_arguments.Add(c.Value);
            this.m_conditionParts.Push(String.Format("@p{0}", this.m_arguments.Count - 1));

            switch (Type.GetTypeCode(c.Value.GetType()))
            {
                case TypeCode.Boolean:
                    this.filter.Push(c.Value.Equals(true) ? "1" : "0");
                    break;
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    this.filter.Push(c.Value.ToString());
                    break;
                case TypeCode.Char:
                case TypeCode.DateTime:
                case TypeCode.String:
                    this.filter.Push("'" + c.Value + "'");
                    break;
                case TypeCode.DBNull:
                case TypeCode.Empty:
                case TypeCode.Object:
                    this.filter.Push("'" + c.Value + "'");
                    break;
                default:
                    Error.ArgumentNull(c.Value +"; "+ c.Value.GetType() );
                    break;
            }

            return c;
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            var mi = m.Member as MemberInfo;
            if (mi == null) return m;

            this.m_conditionParts.Push(String.Format("[{0}]", mi.Name));
            this.filter.Push(mi.Name);

            return m;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            base.VisitMethodCall(m);
            string p = this.m_conditionParts.Pop();
            string n = this.m_conditionParts.Pop();
            this.filter.Pop();
            this.filter.Pop();
            object o = this.m_arguments.Last();
            this.m_arguments.Remove(o);

            switch (m.Method.Name)
            {
                case "Like":
                case "Contains" :
                    this.m_conditionParts.Push(String.Format("({0} Like '%{1}%')", n, o));
                    this.filter.Push(String.Format("({0} Like '%{1}%')", n, o));
                    break;
                case "StartsWith":
                    this.m_conditionParts.Push(String.Format("({0} Like '{1}%')", n, o));
                    this.filter.Push(String.Format("({0} Like '{1}%')", n, o));
                    break;
                case "EndsWith":
                    this.m_conditionParts.Push(String.Format("({0} Like '%{1}')", n, o));
                    this.filter.Push(String.Format("({0} Like '%{1}')", n, o));
                    break;
                case "In":
                    this.m_conditionParts.Push(String.Format("({0} In ({1}))", n, ObjectToString(o)));
                    this.filter.Push(String.Format("({0} In ({1}))", n, ObjectToString(o)));
                    break;
                default:
                    break;
            }
            return m;
        }

        string ObjectToString(object o)
        {

            if (o.GetType() == typeof(string[]))
            {
                return "'" + String.Join("', '", (object[])o) + "'";
            }
            else if (o.GetType() == typeof(int[]))
            {
                return String.Join(", ", (int[])o);
            }
            else
            {
                return String.Join(", ", (object[])o);
            }
        }


        public SelectorArgs Result
        {
            get
            {
                return new SelectorArgs(this);
            }
        }

    }
}

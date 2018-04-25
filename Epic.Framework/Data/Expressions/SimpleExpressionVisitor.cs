using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Reflection;
using Epic.Data.V2;
using Epic.Data.Schema;

namespace Epic.Data.Expressions
{
    internal class SimpleExpressionVisitor : ExpressionVisitor
    {
        List<string> parameterNames;
        List<object> arguments;
        Stack<string> conditionParts;

        IEnumerable<ColumnSchema> customColumnSchemas;

        public string Condition { get; private set; }

        public ObjectParameterDictionary Arguments { get; private set; }

        public void Build(IEnumerable<ColumnSchema> customColumnSchemas, Expression expression)
        {
            PartialEvaluator evaluator = new PartialEvaluator();
            Expression evaluatedExpression = evaluator.Eval(expression);

            this.customColumnSchemas = customColumnSchemas;

            this.parameterNames = new List<string>();
            this.arguments = new List<object>();
            this.conditionParts = new Stack<string>();

            this.Visit(evaluatedExpression);

            var dic = new ObjectParameterDictionary();

            for (int i = 0; i < this.arguments.Count; i++)
            {
                dic.Add("@p"+i, this.arguments[i]);
            }
            this.Arguments = dic;
            this.Condition = this.conditionParts.Count > 0 ? this.conditionParts.Pop() : null;
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
                case ExpressionType.And :
                    opr = "&";
                    break;
                case ExpressionType.Or :
                    opr = "|";
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
                    throw new NotSupportedException(exp.NodeType + " is not supported.");
            }

            this.Visit(exp.Left);
            this.Visit(exp.Right);

            string right = this.conditionParts.Pop();
            string left = this.conditionParts.Pop();

            string condition = String.Format("({0} {1} {2})", left, opr, right);
            this.conditionParts.Push(condition);

            return exp;
        }

        protected override Expression VisitConstant(ConstantExpression exp)
        {
            if (exp == null) return exp;
            this.arguments.Add(exp.Value);
            this.conditionParts.Push(String.Format("@p{0}", this.arguments.Count - 1));

            return exp;
        }

        protected override Expression VisitMemberAccess(MemberExpression exp)
        {
            if (exp == null) return exp;
            var member = exp.Member as MemberInfo;
            if (member == null) return exp;
            var name = member.Name;

            var column = this.customColumnSchemas.SingleOrDefault(e => e.Name == name);
            if (column != null)
                name = column.DbName;

            this.conditionParts.Push(String.Format("[{0}]", name));
            this.parameterNames.Add(member.Name);
            return exp;
        }

        protected override Expression VisitMethodCall(MethodCallExpression exp)
        {
            base.VisitMethodCall(exp);

            string p = this.conditionParts.Pop();
            string n = this.conditionParts.Pop();

            switch (exp.Method.Name)
            {
                case "Like":
                case "Contains":
                    this.conditionParts.Push(String.Format("{0} Like '%'+{1}+'%'", n, p));
                    break;
                case "StartsWith":
                    this.conditionParts.Push(String.Format("{0} Like {1}+'%'", n, p));
                    break;
                case "EndsWith":
                    this.conditionParts.Push(String.Format("{0} Like '%'+{1}", n, p));
                    break;
                case "In":
                    object o = this.arguments.Last();
                    this.arguments.Remove(o);
                    this.conditionParts.Push(String.Format("{0} In ({1})", n, ObjectToString(o)));
                    break;
                default:
                    break;
            }
            return exp;
        }

        string ObjectToString(object o)
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

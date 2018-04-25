using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using Epic.Web;
using System.ComponentModel;


namespace Epic.Paging
{
    internal class SimpleExpressionVisitor : ExpressionVisitor
    {
        static Type ParamType = typeof(HttpParam<>);

        internal Stack<string> condition;
        internal List<string> parameters;
        internal List<object> arguments;
        bool bad;
        bool badSecond;

        object param;
        PropertyDescriptorCollection values;

        public SimpleExpressionVisitor(object param) : this()
        {
            this.param = param;
        }

        public SimpleExpressionVisitor()
        {
            condition = new Stack<string>();
            parameters = new List<string>();
            arguments = new List<object>();
        }


        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            return base.VisitConditional(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            string opr;
            switch (node.NodeType)
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
                case ExpressionType.And:
                    opr = "&";
                    break;
                case ExpressionType.Or:
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
                    throw new NotSupportedException(node.NodeType + " is not supported.");
            }

            this.Visit(node.Left);
            this.Visit(node.Right);
            var right = this.condition.Pop();
            var left = this.condition.Pop();
            if (this.bad)
            {
                this.condition.Push(String.Empty);
                this.bad = false;
                this.badSecond = true;
                return node;
            }


            if (this.badSecond)
            {
                this.badSecond = false;
                this.condition.Push(String.Concat(left, right));
            }
            else
                this.condition.Push(String.Concat(left, " ", opr, " ", right));

            return node;

        }



        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            base.VisitMethodCall(node);

            var p = this.condition.Pop();
            var n = this.condition.Pop();

            if (this.bad)
            {
                this.condition.Push(String.Empty);
                this.bad = false;
                this.badSecond = true;
                return node;
            }

            switch (node.Method.Name)
            {
                case "Like":
                case "Contains":
                    this.condition.Push(String.Format("{0} Like '%'+{1}+'%'", n, p));
                    break;
                case "StartsWith":
                    this.condition.Push(String.Format("{0} Like {1}+'%'", n, p));
                    break;
                case "EndsWith":
                    this.condition.Push(String.Format("{0} Like '%'+{1}", n, p));
                    break;
                case "In":
                    object o = this.arguments.Last();
                    this.arguments.Remove(o);
                    this.condition.Push(String.Format("{0} In ({1})", n, ObjectToString(o)));
                    break;
                default:
                    break;
            }

            return node;

        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var property = node.Member as PropertyInfo;
            if (property != null && property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == ParamType)
            {

                var result = this.ParamProperties.Find(property.Name, false).GetValue(this.param) as HttpParam;
                if (result.State != HttpParamStateType.Vaild)
                {
                    this.condition.Push(String.Concat("[", node.Member.Name, "]"));
                    this.bad = true;
                    return node;
                }

                this.arguments.Add(result.GetValueOrOriginal());
                this.condition.Push(String.Concat("@p", this.arguments.Count - 1));
            }
            else
            {
                this.condition.Push(String.Concat("[", node.Member.Name, "]"));
                this.parameters.Add(node.Member.Name);
            }

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            this.arguments.Add(node.Value);
            this.condition.Push(String.Concat("@p", this.arguments.Count - 1));
            return node;
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            return base.VisitMemberAssignment(node);
        }


        PropertyDescriptorCollection paramProperties;
        internal PropertyDescriptorCollection ParamProperties
        {
            get
            {
                if (this.paramProperties == null)
                    this.paramProperties = TypeDescriptor.GetProperties(param);
                return this.paramProperties;
            }
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

        public override string ToString()
        {
            return this.condition.Pop();
        }

    }
}

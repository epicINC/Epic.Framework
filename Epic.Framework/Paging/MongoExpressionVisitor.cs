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
    /// <summary>
    /// mongo 运算符参考
    /// http://docs.mongodb.org/manual/reference/operators/
    /// </summary>
    internal class MongoExpressionVisitor : ExpressionVisitor
    {
        static Type ParamType = typeof(HttpParam<>);

        internal Stack<string> condition;
        bool bad;
        bool badSecond;

        object param;
        PropertyDescriptorCollection values;

        public MongoExpressionVisitor(object param) : this()
        {
            this.param = param;
        }

        public MongoExpressionVisitor()
        {
            condition = new Stack<string>();
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
                    opr = "\"{0}\":{1}";
                    break;
                case ExpressionType.NotEqual: // <>
                    opr = "\"{0}\":{{\"$ne\":{1}}}";
                    break;
                case ExpressionType.GreaterThan: // >
                    opr = "{{\"{0}\":{{\"$gt\":{1}}}}}";
                    break;
                case ExpressionType.GreaterThanOrEqual: // >=
                    opr = "\"{0}\":{{\"$gte\":{1}}}";
                    break;
                case ExpressionType.LessThan: // <
                    opr = "{{\"{0}\":{{\"$lt\":{1}}}}}";
                    break;
                case ExpressionType.LessThanOrEqual: // <=
                    opr = "\"{0}\":{{\"$lte\":{1}}}";
                    break;
                case ExpressionType.AndAlso: // And "$and [{{{0}}}, {{{1}}}]"
                    opr = "{{{0}, {1}}}";
                    break;
                case ExpressionType.And:
                    opr = "&";
                    break;
                case ExpressionType.Or:
                    opr = "|";
                    break;
                case ExpressionType.OrElse: // Or
                    opr = "{{\"$or\" : [{0}, {1}]}}";
                    break;
                case ExpressionType.Add: // +
                    opr = "\"$inc\": {{{0} : {1}}}";
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
                this.condition.Push(String.Format(opr, left, right));

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

            var format = String.Empty;

            switch (node.Method.Name)
            {
                case "Like":
                case "Contains":
                    format = "{{\"{0}\" : /{1}/}}";
                    break;
                case "StartsWith":
                    format = "{{\"{0}\" : /^{1}/}}";
                    break;
                case "EndsWith":
                    format = "{{\"{0}\" : /{1}^/}}";
                    break;
                case "In":

                    format = "{{\"{0}\" : {{ $in : [{1}]}}}}";
                    //this.condition.Push(String.Format("{0} In ({1})", n, ObjectToString()));
                    break;
                default:
                    break;
            }
            this.condition.Push(String.Format(format, n, p));

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
                    this.condition.Push(node.Member.Name);
                    this.bad = true;
                    return node;
                }

                this.condition.Push(result.GetValueOrOriginal().ToString());
            }
            else
            {
                this.condition.Push(node.Member.Name);
            }

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            this.condition.Push(node.Value.ToString());
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

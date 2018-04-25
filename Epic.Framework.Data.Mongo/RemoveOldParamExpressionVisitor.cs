using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Epic.Data.Mongo
{
    public class RemoveOldParamExpressionVisitor : ExpressionVisitor
    {
        static Type ParamType = typeof(Nullable<>);

        object param;

        public RemoveOldParamExpressionVisitor(object param)
        {
            this.param = param;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var body = this.Visit(node.Body);
            if (body != node.Body && body == null)
                return null;

            var parameters = this.VisitAndConvert<ParameterExpression>(node.Parameters, "VisitLambda");
            return node.Update(body, parameters);

        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = this.Visit(node.Left);
            var conversion = this.VisitAndConvert<LambdaExpression>(node.Conversion, "VisitBinary");
            var right = this.Visit(node.Right);

            if (left == null) return right.NodeType == ExpressionType.MemberAccess ? null : right;
            if (right == null) return left.NodeType == ExpressionType.MemberAccess ? null : left;

            return node.Update(left, conversion, right);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var property = node.Member as PropertyInfo;
            if (property != null && property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == ParamType)
            {
                var result = property.GetValue(this.param, null);
                if (result == null) return null;

                return Expression.Constant(result);
            }

            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "In")
            {
                var instance = this.Visit(node.Object);
                var args = this.Visit(node.Arguments);
                if (args != node.Arguments) return args.Count == 2 && args[1] == null ? null : node.Update(instance, args);
               return node;
;
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            var result = this.Visit(node.Expressions);
            if (node.Expressions != result)
            {
                var expressions = result.Where(e => e != null);
                if (expressions.Count() == 0) return null;
                return node.Update(expressions);
            }

            return node;
        }



        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Convert)
                return this.Visit(node.Operand);
            return base.VisitUnary(node);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Epic.Data.Objects.Expressions
{
    internal static class SqlExpressionVisitorEX
    {
        internal static string ToSqlValue(object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.String:
                case TypeCode.DateTime:
                case TypeCode.Object:
                    return "'" + o + "'";
                    break;
                default:
                    return o.ToString();
                    break;
            }

        }
    }


    internal class SqlExpressionVisitor : System.Linq.Expressions.ExpressionVisitor
    {
        internal static void Build(Expression node)
        {
            var v = new SqlExpressionVisitor();
            v.Visit(node);
        }




        DbCommandBuilder builder = new DbCommandBuilder();

        public Expression Modify(Expression expression)
        {
            return Visit(expression);
        }


        public override Expression Visit(Expression node)
        {
            if (node == null) return node;

            #region case

            switch (node.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.ArrayIndex:
                case ExpressionType.Coalesce:
                case ExpressionType.Divide:
                case ExpressionType.Equal:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LeftShift:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.Modulo:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.NotEqual:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.RightShift:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    this.VisitBinary((BinaryExpression)node);
                    break;
                case ExpressionType.ArrayLength:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    this.VisitUnary((UnaryExpression)node);
                    break;
                case ExpressionType.AddAssign:
                    break;
                case ExpressionType.AddAssignChecked:
                    break;
                case ExpressionType.AndAssign:
                    break;
                case ExpressionType.Assign:
                    break;
                case ExpressionType.Block:
                    break;
                case ExpressionType.Call:
                    this.VisitCall((MethodCallExpression)node);
                    break;
                case ExpressionType.Conditional:
                    break;
                case ExpressionType.Constant:
                    this.VisitConstant(node as ConstantExpression);
                    break;
                case ExpressionType.DebugInfo:
                    break;
                case ExpressionType.Decrement:
                    break;
                case ExpressionType.Default:
                    break;
                case ExpressionType.DivideAssign:
                    break;
                case ExpressionType.Dynamic:
                    break;
                case ExpressionType.ExclusiveOrAssign:
                    break;
                case ExpressionType.Extension:
                    break;
                case ExpressionType.Goto:
                    break;
                case ExpressionType.Increment:
                    break;
                case ExpressionType.Index:
                    break;
                case ExpressionType.Invoke:
                    break;
                case ExpressionType.IsFalse:
                    break;
                case ExpressionType.IsTrue:
                    break;
                case ExpressionType.Label:
                    break;
                case ExpressionType.Lambda:
                    this.VisitLambda(node as LambdaExpression);
                    break;
                case ExpressionType.LeftShiftAssign:
                    break;
                case ExpressionType.ListInit:
                    break;
                case ExpressionType.Loop:
                    break;
                case ExpressionType.MemberAccess:
                    this.VisitMemberAccess(node as MemberExpression);
                    break;
                case ExpressionType.MemberInit:
                    break;
                case ExpressionType.ModuloAssign:
                    break;
                case ExpressionType.MultiplyAssign:
                    break;
                case ExpressionType.MultiplyAssignChecked:
                    break;
                case ExpressionType.New:
                    break;
                case ExpressionType.NewArrayBounds:
                    break;
                case ExpressionType.NewArrayInit:
                    break;
                case ExpressionType.OnesComplement:
                    break;
                case ExpressionType.OrAssign:
                    break;
                case ExpressionType.Parameter:
                    break;
                case ExpressionType.PostDecrementAssign:
                    break;
                case ExpressionType.PostIncrementAssign:
                    break;
                case ExpressionType.Power:
                    break;
                case ExpressionType.PowerAssign:
                    break;
                case ExpressionType.PreDecrementAssign:
                    break;
                case ExpressionType.PreIncrementAssign:
                    break;
                case ExpressionType.RightShiftAssign:
                    break;
                case ExpressionType.RuntimeVariables:
                    break;
                case ExpressionType.SubtractAssign:
                    break;
                case ExpressionType.SubtractAssignChecked:
                    break;
                case ExpressionType.Switch:
                    break;
                case ExpressionType.Throw:
                    break;
                case ExpressionType.Try:
                    break;
                case ExpressionType.TypeEqual:
                    break;
                case ExpressionType.TypeIs:
                    break;
                case ExpressionType.UnaryPlus:
                    break;
                case ExpressionType.Unbox:
                    break;
                default:
                    break;
            }

            #endregion



            return null;

        }

        bool IsValid(Type type)
        {
            return type == typeof(IQueryable<>) || type == typeof(ObjectSet<>);
        }

        protected void VisitCall(MethodCallExpression node)
        {
            this.Visit(node.Object);
            foreach (var item in node.Arguments)
            {
                    this.Visit(item);
            }

            switch (node.Method.Name)
            {
                case "Where" :
                    break;
                case "Take" :
                    this.builder.FlushTop();
                    break;
                case "StartsWith" :
                case "EndsWith":
                case "Contains":
                    this.builder.Flush(node);
                    break;
                case "Select":

                    break;
                case "OrderBy":
                case "ThenBy" :
                    this.builder.OrderPop();
                    break;
                case "OrderByDescending" :
                    this.builder.OrderPop("DESC");
                    break;
                default:
                    break;
            }
        }


        protected Expression VisitLambda(LambdaExpression node)
        {
            this.Visit(node.Body);
            this.Visit(node.Parameters[0]);
            return null;
        }



        protected Expression VisitUnary(UnaryExpression node)
        {
            this.Visit(node.Operand);
            return node;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            this.Visit(node.Left);
            this.Visit(node.Right);
            this.builder.Flush(node);
            return node;
        }


        protected Expression VisitMemberAccess(MemberExpression node)
        {
            if (node.Member == null) return node;
            this.builder.Push(node.Member.Name);
            return node;
        }


        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Type.IsGenericType && node.Type.GetGenericTypeDefinition() == typeof(ObjectSet<>)) return node;
            this.builder.PushValue(node);
            return node;
        }



    }
}

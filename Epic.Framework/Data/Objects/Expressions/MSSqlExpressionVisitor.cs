using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.Data.Expressions;

namespace Epic.Data.Objects.Expressions
{
    internal class MSSqlExpressionVisitor : Epic.Data.Expressions.ExpressionVisitor
    {
        public static DbCommandBuilder Build(DbCommandBuilder builder, Expression node)
        {
            //PartialEvaluator evaluator = new PartialEvaluator();
            //Expression evaluatedExpression = evaluator.Eval(node);

            var v = new MSSqlExpressionVisitor(builder);
            v.Visit(node);
            return v.builder;

        }

        MSSqlExpressionVisitor(DbCommandBuilder builder)
        {
            this.builder = builder;
        }

        DbCommandBuilder builder;

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node == null) return node;
            this.Visit(node.Left);
            this.Visit(node.Right);
            this.builder.Flush(node);
            return node;
        }

        protected override Expression VisitMemberAccess(MemberExpression node)
        {
            if (node == null || node.Member == null) return node;
            this.builder.Push(node.Member.Name);
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node == null) return node;
            if (node.Type.IsGenericType && node.Type.GetInterface("IQueryable") != null)
            {
                this.builder.PushSource(node);
                return node;
            }
            this.builder.PushValue(node.Value);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            base.VisitMethodCall(node);
            switch (node.Method.Name)
            {
                case "Where":
                    break;
                case "Take":
                    this.builder.FlushTop();
                    break;
                case "StartsWith":
                case "EndsWith":
                case "Contains":
                    this.builder.Flush(node);
                    break;
                case "Select":

                    break;
                case "OrderBy":
                case "ThenBy":
                    this.builder.OrderPop("ASC");
                    break;
                case "OrderByDescending":
                    this.builder.OrderPop("DESC");
                    break;
                case "Count" :
                    this.builder.FlushCount();
                    break;
                default:
                    break;
            }
            return node;
        }




    }
}

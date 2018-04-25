using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Epic.Components;
using MongoDB.Bson;


namespace Epic.Data.Mongo.Gen10
{
    /// <summary>
    /// mongo 运算符参考
    /// http://docs.mongodb.org/manual/reference/operators/
    /// </summary>
    public class MongoBsonExpressionVisitor : ExpressionVisitor
    {
        static Type ParamType = typeof(Nullable<>);

        internal Stack<IMongoQuery> query = new Stack<IMongoQuery>();
        internal Stack<string> parameter = new Stack<string>();
        internal Stack<object> constant = new Stack<object>();



        internal static IMongoQuery ParseQuery(Expression node)
        {
            var result = new MongoBsonExpressionVisitor();
            result.Visit(node);
            return result.GenerateQuery();
        }


        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {

            this.Visit(node.Left);
            this.Visit(node.Right);


            switch (node.NodeType)
            {
                case ExpressionType.Equal:              // =
                    this.PushBinary(e => Query.EQ(e.Left, e.Right));
                    break;
                case ExpressionType.NotEqual:           // <>
                    this.PushBinary(e => Query.NE(e.Left, e.Right));
                    break;
                case ExpressionType.GreaterThan:        // >
                    this.PushBinary(e => Query.GT(e.Left, e.Right));
                    break;
                case ExpressionType.GreaterThanOrEqual: // >=
                    this.PushBinary(e => Query.GTE(e.Left, e.Right));
                    break;
                case ExpressionType.LessThan:           // <
                    this.PushBinary(e => Query.LT(e.Left, e.Right));
                    break;
                case ExpressionType.LessThanOrEqual:    // <=
                    this.PushBinary(e => Query.LTE(e.Left, e.Right));
                    break;
                case ExpressionType.AndAlso:            // And
                    this.PushQuery(e => Query.And(e.Left, e.Right));
                    break;
                case ExpressionType.And:                // &
                    break;
                case ExpressionType.Or:                 // |
                    break;
                case ExpressionType.OrElse:             // Or
                    this.PushQuery(e => Query.Or(e.Left, e.Right));
                    break;
                case ExpressionType.Add:                // +
                    break;
                case ExpressionType.Subtract:           // -
                    break;
                case ExpressionType.Multiply:           // *
                    break;
                case ExpressionType.Divide:             // /
                    break;
                default:
                    throw new NotSupportedException(node.NodeType + " is not supported.");
            }



            return node;

        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            base.VisitMethodCall(node);

            switch (node.Method.Name)
            {
                case "Like":
                case "Contains":
                    this.PushBinary(e => Query.Matches(e.Left, "/" + e.Right + "/"));
                    break;
                case "StartsWith":
                    this.PushBinary(e => Query.Matches(e.Left, "/^" + e.Right + "/"));
                    break;
                case "EndsWith":
                    this.PushBinary(e => Query.Matches(e.Left, "/" + e.Right + "^/"));
                    break;
                case "In":
                    this.PushBinaryArray(e => Query.In(e.Left, e.Right));
                    //this.condition.Push(String.Format("{0} In ({1})", n, ObjectToString()));
                    break;
                default:
                    break;
            }
            
            return node;

        }

        protected override Expression VisitMember(MemberExpression node)
        {
            this.parameter.Push(node.Member.Name);
            return base.VisitMember(node);
        }


        protected override Expression VisitConstant(ConstantExpression node)
        {

            this.constant.Push(node.Value);
            return node;
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            this.Visit(node.Expressions);

            var result = new List<object>();
            for (int i = 0; i < node.Expressions.Count; i++)
            {
                result.Add(this.constant.Pop());
            }

            this.constant.Push(result);

            return node;
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

        #region Pop


        TupleHorizontal<IMongoQuery, IMongoQuery> PopQuery()
        {
            return this.PopQuery<IMongoQuery>(this.query);
        }

        TupleHorizontal<T, T> PopQuery<T>(Stack<T> value)
        {
            return new TupleHorizontal<T, T>() { Right = value.Pop(), Left = value.Pop() };
        }

        void PushQuery(Func<TupleHorizontal<IMongoQuery, IMongoQuery>, IMongoQuery> func)
        {
            this.query.Push(func(this.PopQuery()));
        }


        TupleHorizontal<string, BsonValue> PopBinary()
        {
            return new TupleHorizontal<string, BsonValue>() { Left = this.parameter.Pop(), Right = BsonValue.Create(this.constant.Pop()) };
        }

        TupleHorizontal<string, BsonArray> PopBinaryArray()
        {
            return new TupleHorizontal<string, BsonArray>() { Left = this.parameter.Pop(), Right = BsonArray.Create(this.constant.Pop()) };
        }


        void PushBinary(Func<TupleHorizontal<string, BsonValue>, IMongoQuery> func)
        {
            this.query.Push(func(this.PopBinary()));
        }

        void PushBinaryArray(Func<TupleHorizontal<string, BsonArray>, IMongoQuery> func)
        {
            this.query.Push(func(this.PopBinaryArray()));
        }

        #endregion

        public override string ToString()
        {
            return this.query.Pop().ToString();
        }

        public IMongoQuery GenerateQuery()
        {
            return this.query.Pop();
        }

    }


}

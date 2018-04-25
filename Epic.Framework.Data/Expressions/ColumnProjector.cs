using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Epic.Data.Schema;
using Epic.FluentAPI;

namespace Epic.Data.Expressions
{
    public abstract class ProjectionRow
    {
        public abstract object GetValue(int index);
    }

    internal class ColumnProjection
    {
        internal string Columns;
        internal Expression Selector;
    }

    internal class ColumnProjector : ExpressionVisitor
    {
        TableDefinition Table;



        internal TableDefinition Translate(Expression expression)
        {
            this.Visit(expression);
            return Table;
        }

        protected override NewExpression VisitNew(NewExpression nex)
        {
            var isExists = TableDefinition.Exists(nex.Type);
            this.Table = TableDefinition.Find(nex.Type);
            this.Table.Columns.Clear();

            if (isExists) return nex;


            return base.VisitNew(nex);
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
            {
                this.Table.Property(new PropertyPath(m.Member as PropertyInfo));

                return m;
            }
            else
                return base.VisitMemberAccess(m);
        }
    }
}

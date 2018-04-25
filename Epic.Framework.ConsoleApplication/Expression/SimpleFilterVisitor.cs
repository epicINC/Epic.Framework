using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Epic.Framework.ConsoleApplication.Expressions
{
    public class SimpleFilterVisitor : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }
    }
}

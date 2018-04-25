using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Epic.Utility
{
    internal static class SimpleExpression
    {
        public static System.Reflection.MemberInfo Find(LambdaExpression value)
        {
            return Find(value.Body as MemberExpression);
        }

        public static System.Reflection.MemberInfo Find(MemberExpression value)
        {
            return value.Member;
        }
    }
}

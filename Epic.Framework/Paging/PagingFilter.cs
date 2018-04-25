using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Epic.Components;

namespace Epic.Paging
{
    public interface IPagingFilter<T>
    {


        void Where(Expression<Func<T, bool>> func);
    }



    public class PagingFilter<T, K> 
    {
        K param;
        public PagingFilter(K param)
        {
            this.param = param;
        }

        public string Where(Expression<Func<T, K, bool>> func)
        {
            var result = new MongoExpressionVisitor(param);
            result.Visit(func);
            var temp = result.ToString();
            Console.WriteLine(temp);
            return temp;
        }
    }
}

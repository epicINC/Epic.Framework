using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data;

namespace Epic.Data.V2
{
    public interface IObjectQuery<T> : IEnumerable<T>
    {
        bool IsSimple { get; }
        ObjectDataProvider<T> Provider { get; set; }
        string CommandText { get; set; }
        CommandType CommandType { get; set; }
        ObjectQueryBuilder<T> Builder { get; }
        ObjectParameterDictionary ParameterData { get; }
        Expression<Func<T, bool>> Expression {get;}
        PagingParam Param {get;}
    }
}

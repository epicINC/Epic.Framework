using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Epic.Business
{
    public interface IBusiness<T>
    {
        T Find(Expression<Func<T, bool>> selector);
        bool Insert(T value);
        bool Update(T value);
        bool Save(T value);
        bool Delete(T value);
        Dictionary<string, string> IsValid(T value);
    }

}

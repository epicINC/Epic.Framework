using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Components;

namespace Epic.Data.Objects
{
    public class ObjectSet<T> : ObjectQuery<T>, IOrderedQueryable<T>
    {
        ObjectContext context;

        internal ObjectSet(ObjectContext context) : base(typeof(T).Name, context)
        {
            this.context = context;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }




    }
}

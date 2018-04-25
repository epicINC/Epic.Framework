using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Data.V2;
using System.Linq.Expressions;
using Epic.Utility;

namespace Epic.Business
{

    public abstract class BaseBusiness<T, D> : IBusiness<T> where D : ObjectDataProvider<T>, new()
    {
        public BaseBusiness()
        {
            
        }


        D dataProvider;
        protected D DataProvider
        {
            get
            {
                if (this.dataProvider == null)
                    this.dataProvider = new D();
                return this.dataProvider;
            }
        }


        public T Find(Expression<Func<T, bool>> selector)
        {
            return this.DataProvider.CreateQuery().And(selector).SingleOrDefault();
        }


        public virtual bool Insert(T value)
        {
            return this.DataProvider.Insert(value);
        }

        public virtual bool Update(T value)
        {
            return this.DataProvider.Update(value);
        }

        public abstract bool Save(T value);

        public virtual bool Delete(T value)
        {
            return this.DataProvider.Delete(value);
        }

        public abstract Dictionary<string, string> IsValid(T value);

        protected Validator<T> Validator(T value)
        {
            return new Validator<T>(value);
        }
    }
}

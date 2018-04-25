using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Epic.NewParam
{
    public class TestParam
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public DateTime CreateDate
        {
            get;
            set;
        }


        public static void Test()
        {
            var p = new WebParam<TestParam>();
            p.RuleFor(e => e.ID).ID().Required().Valid(e => e > 10);
            p.RuleFor(e => e.Name).Required().Regex("");
            //p.Validator(e => e.ID).i;
            //p.Param(e => e.Name).Value;
        }

    }


    public class WebParam<T> where T : new()
    {
        T value;
        public WebParam(bool force = false)
        {
            this.IsForce = force;
            this.value = new T();
            this.Dictionary = new Dictionary<string, IWebParamItem>();
            this.Context = System.Web.HttpContext.Current;
            
        }

        public bool IsForce
        {
            get;
            set;
        }

        public WebParamState State
        {
            get;
            set;
        }

        public T Value
        {
            get { return this.value; }
        }

        internal System.Web.HttpContext Context
        {
            get;
            private set;
        }

        Dictionary<string, IWebParamItem> Dictionary
        {
            get;
            set;
        }

        #region RuleFor

        public RuleForExpression<T, K> RuleFor<K>(Expression<Func<T, K>> selector)
        {
            var result = this.Property(selector);
            return new RuleForExpression<T, K>(result);
        }

        public RuleForExpression<T, K> RuleFor<K>(Expression<Func<T, K[]>> selector)
        {
            var result = this.Property(selector);
            return new RuleForExpression<T, K>(result);
        }

        public RuleForExpression<T, K> RuleFor<K>(string name)
        {
            var result = this.Property<K>(name);
            return new RuleForExpression<T, K>(result);
        }

        public RuleForExpression<T, K> RuleForArray<K>(string name)
        {
            var result = this.PropertyArray<K>(name);
            return new RuleForExpression<T, K>(result);
        }

        #endregion


        #region Property

        public WebParamItem<T, K> P<K>(Expression<Func<T, K>> selector)
        {
            return this.Property<K>(selector);
        }

        public WebParamArrayItem<T, K> P<K>(Expression<Func<T, K[]>> selector)
        {
            return this.Property<K>(selector);
        }

        public WebParamItem<T, K> Property<K>(Expression<Func<T, K>> selector)
        {
            var result = this.FindByName(selector);
            if (result == null)
            {
                result = new WebParamItem<T, K>(this, selector);
                this.Dictionary.Add(result.Name, result);
            }
            return result as WebParamItem<T, K>;
        }

        public WebParamArrayItem<T, K> Property<K>(Expression<Func<T, K[]>> selector)
        {
            var result = this.FindByName(selector);
            if (result == null)
            {
                result = new WebParamArrayItem<T, K>(this, selector);
                this.Dictionary.Add(result.Name, result);
            }
            return result as WebParamArrayItem<T, K>;
        }

        public WebParamItem<T, K> Property<K>(string name)
        {
            var result = this.Find(name);
            if (result == null)
            {
                result = WebParamItem<T, K>.Create(this, name);
                this.Dictionary.Add(result.Name, result);
            }
            return result as WebParamItem<T, K>;
        }

        public WebParamArrayItem<T, K> PropertyArray<K>(string name)
        {
            var result = this.Find(name);
            if (result == null)
            {
                result = WebParamArrayItem<T, K>.Create(this, name);
                this.Dictionary.Add(result.Name, result);
            }
            return result as WebParamArrayItem<T, K>;

        }

        #endregion

        #region Find

        public IWebParamItem Find(string name)
        {
            IWebParamItem result;
            this.Dictionary.TryGetValue(name, out result);
            return result;
        }

        IWebParamItem FindByName(LambdaExpression value)
        {
            return this.Find(this.FindName(value));
        }

        string FindName(LambdaExpression value)
        {
            return Epic.FluentAPI.FluentAPIExtensions.GetComplexPropertyAccess(value).Single().Name;
        }

        #endregion




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.Data.Expressions;
using Epic.Web;


namespace Epic.Data
{
    public class PagingFilter<T>
    {
        #region 构造函数
        Paging<T> paging;

        internal PagingFilter(Paging<T> paging)
        {
            this.paging = paging;
        }

        #endregion



        Expression<Func<T, bool>> whereSelector;

        List<string> filter;
        List<string> InitFilter()
        {
            if (this.filter == null)
                this.filter = new List<string>();
            return this.filter;
        }
    

        #region String

        public PagingFilter<T> And(string condition)
        {
            if (!String.IsNullOrWhiteSpace(condition))
            {
                this.InitFilter().Add("AND");
                this.filter.Add(condition);
            }
            return this;
        }


        public PagingFilter<T> Or(string condition)
        {
            if (!String.IsNullOrWhiteSpace(condition))
            {
                this.InitFilter().Add("OR");
                this.filter.Add(condition);
            }
            return this;
        }

        public PagingFilter<T> In(string key, string[] value)
        {
            if (value != null && value.Length > 0)
                this.InitFilter().Add("key in ('" + String.Join(("', '"), value) + "')");
            return this;
        }

        public PagingFilter<T> In(string key, int[] value)
        {
            if (value != null && value.Length > 0)
                this.InitFilter().Add("key in (" + String.Join((", "), value) + ")");
            return this;
        }

        public PagingFilter<T> In<K>(HttpParam<K> p, string key, string[] value)
        {
            if (!p.IsValid<K>()) return this;
            if (value == null || value.Length == 0) return this;
            this.paging.Param.Add(p);
            this.InitFilter().Add("key in ('" + String.Join(("', '"), value) + "')");

            return this;
        }

        public PagingFilter<T> In<K>(HttpParam<K> p, string key, int[] value)
        {
            if (!p.IsValid<K>()) return this;
            if (value == null || value.Length == 0) return this;
            this.paging.Param.Add(p);
            this.InitFilter().Add("key in (" + String.Join((", "), value) + ")");
            return this;
        }

        #endregion

        public PagingFilter<T> And(Expression<Func<T, bool>> query)
        {
            if (query != null)
            {
                whereSelector = InnerExpressionHelper.And(whereSelector, query);
                hasExpression = true;
            }

            return this;
        }

        public PagingFilter<T> Or(Expression<Func<T, bool>> query)
        {
            if (query != null)
            {
                whereSelector = InnerExpressionHelper.Or(whereSelector, query);
                hasExpression = true;
            }
            return this;
        }


        #region HttpParam

        public PagingFilter<T> And<K>(HttpParam<K> p, Expression<Func<T, bool>> query)
        {
            if (p.IsValid() && query != null)
            {
                whereSelector = InnerExpressionHelper.And(whereSelector, query);
                hasExpression = true;

                this.paging.Param.Add(p);
            }

            return this;
        }

        public PagingFilter<T> Or<K>(HttpParam<K> p, Expression<Func<T, bool>> query)
        {
            if (p.IsValid() && query != null)
            {
                whereSelector = InnerExpressionHelper.Or(whereSelector, query);
                hasExpression = true;

                this.paging.Param.Add(p);
            }
            return this;
        }

        #endregion



        bool hasExpression;
        public bool HasExpression
        {
            get { return this.hasExpression; }
        }

        public string Filter
        {
            get
            {

                string result = null;
                if (this.whereSelector != null)
                    result = this.Build(whereSelector).Filter;
                if (this.filter != null && this.filter.Count > 0)
                {
                    if (String.IsNullOrWhiteSpace(result))
                        this.filter.RemoveAt(0);
                    result = " " + String.Join(" ", this.filter.ToArray());
                }
                return result;

            }
        }


        public SelectorArgs Build(LambdaExpression predicate)
        {
            ConditionBuilder conditionBuilder = new ConditionBuilder();
            conditionBuilder.Build(predicate.Body);
            return conditionBuilder.Result;
            
        }


    }


}

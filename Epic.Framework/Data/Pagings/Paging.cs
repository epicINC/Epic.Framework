using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.Web;
using Epic.Components;

namespace Epic.Data
{


    public class Paging<T> : BasePaging
    {



        PagingFilter<T> whereSelector;
        public PagingFilter<T> Where
        {
            get
            {
                if (this.whereSelector == null)
                    this.whereSelector = new PagingFilter<T>(this);
                return this.whereSelector;
            }
        }


        #region Order

        Dictionary<string, SortDirection> orderSelector;


        bool AddOrder(string order, SortDirection sort)
        {
            this.isOrderCalcResult = false;
            if (sort == SortDirection.Default)
                sort = SortDirection.Desc;
            if (this.orderSelector == null)
            {
                this.orderSelector = new Dictionary<string, SortDirection>();
                this.orderSelector.Add(order, sort);
                return true;
            }

            if (this.orderSelector.ContainsKey(order))
            {
                this.orderSelector[order] = sort;
                return false;
            }
            else
            {
                this.orderSelector.Add(order, sort);
                return true;
            }

        }


        public Paging<T> OrderBy(string order, SortDirection sort)
        {
            AddOrder(order, sort);
            return this;
        }

        public Paging<T> OrderByAscending(string order)
        {
            return this.OrderBy(order, SortDirection.Asc);
        }
        public Paging<T> OrderByDescending(string order)
        {
            return this.OrderBy(order, SortDirection.Desc);
        }



        public Paging<T> OrderByAscending<K>(HttpParam<K> order)
        {
            return OrderByDescending<K>(order, SortDirection.Asc);
        }

        public Paging<T> OrderByDescending<K>(HttpParam<K> order)
        {
            return OrderByDescending<K>(order, SortDirection.Desc);
        }

        public Paging<T> OrderByDescending<K>(HttpParam<K> order, SortDirection sort)
        {
            if (order.IsValid())
            {
                if (AddOrder(order.value.ToString(), sort))
                    this.Param.Add(order);
            }
            return this;
        }

        public Paging<T> OrderBy<K>(HttpParam<K> order, HttpParam<SortDirection> sort)
        {
            if (order.IsValid() && sort.IsValid())
            {
                if (AddOrder(order.value.ToString(), sort.value))
                {
                    this.Param.Add(order);
                    this.Param.Add(sort);
                }
            }
            return this;
        }


        public Paging<T> OrderBy<K>(string order, HttpParam<SortDirection> sort)
        {
            if (sort.IsValid())
            {
                if (AddOrder(order, sort.value))
                    this.Param.Add(sort);
            }
            return this;
        }

        #endregion


        protected override string BuildOrder()
        {
            string result;
            if (this.orderSelector != null && this.orderSelector.Count > 0)
            {
                var t = from e in this.orderSelector
                        select (e.Key + " " + e.Value);
                result = String.Join<string>(", ", t);
            }
            else
                result = this.defaultOrder;

            this.isOrderCalcResult = true;
            return result;
            
        }






    }

}

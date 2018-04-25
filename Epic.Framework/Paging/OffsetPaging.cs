using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.FluentAPI;
using System.Linq.Expressions;
using Epic.Web;
using Epic.Components;

namespace Epic.Paging
{
    /// <summary>
    /// 偏移量分页对象
    /// </summary>
    public abstract class OffsetPaging
    {
        public OffsetPaging()
        {
            this.PageSize = 20;
        }

        /// <summary>
        /// 当前偏移
        /// </summary>
        public int OffSet
        {
            get;
            set;
        }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling((double)this.RecordCount / this.PageSize);
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public long RecordCount
        {
            get;
            set;
        }

        public string Field
        {
            get;
            set;
        }

        public string Table
        {
            get;
            set;
        }

        public string Filter
        {
            get;
            protected set;
        }


        public abstract void Where<T>(Expression<Func<T, bool>> func);


        public void And<T>(T value, Expression<Func<T, bool>> func)
        {

        }



        public void OrderBy<T>(Expression<Func<T>> func, SortDirection sort = SortDirection.Default)
        {
            if (func == null)
                throw new ArgumentNullException("func");

            var name = func.GetComplexPropertyAccess().SingleOrDefault();
            if (name == null)
                throw new ArgumentNullException("name");

            this.OrderBy(name.Name, sort);

        }

        public void OrderBy(string value, SortDirection sort = SortDirection.Default)
        {
            if (value == null)
                throw new ArgumentNullException("name");

            this.Order.Add(value, sort);
        }


        Dictionary<string, SortDirection> order;
        public Dictionary<string, SortDirection> Order
        {
            get
            {
                if (this.order == null)
                    this.order = new Dictionary<string, SortDirection>();
                return this.order;
            }
            set { this.order = value;}
        }

        /// <summary>
        /// 是否有条件
        /// </summary>
        public bool HasFilter
        {
            get { return !String.IsNullOrEmpty(this.Filter); }
        }

        /// <summary>
        /// 是否排序
        /// </summary>
        public bool HasOrder
        {
            get { return this.order != null && this.order.Count > 0; }
        }


        public void AddOrder(string field, SortDirection sort = SortDirection.Default)
        {
            this.Order.Add(field, sort);
        }

        void Rest()
        {

        }
    }
}

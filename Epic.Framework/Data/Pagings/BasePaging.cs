using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Web;

namespace Epic.Data
{
    public abstract class BasePaging
    {
        #region Var

        protected int absolutePage;
        protected int pageSize;
        protected int pageCount;
        protected int recordCount;

        protected string fields;
        protected string tables;
        protected string order;
        protected string group;
        protected string filter;

        #endregion




        #region Pro

        bool isSetAbsolutePage = false;

        public int PreviousPage
        {
            get
            {
                if (this.AbsolutePage < 2)
                    return 1;
                return this.AbsolutePage - 1;
            }
        }

        public int NextPage
        {
            get
            {
                if (this.AbsolutePage < this.PageCount)
                    return this.AbsolutePage + 1;
                return this.PageCount;
            }
        }

        public int AbsolutePage
        {
            get
            {
                if (!isSetAbsolutePage)
                {
                    var id = System.Web.HttpContext.Current.Request.QueryString.ToParam<int>("Page").Parse().ID();
                    this.absolutePage = id.State == HttpParamStateType.Vaild ? id.value : 1;
                    this.isSetAbsolutePage = true;
                }
                return this.absolutePage;
            }
            set
            {
                this.isSetAbsolutePage = true;
                this.absolutePage = value;
            }
        }

        public int PageSize
        {
            get { return this.pageSize; }
            set { this.pageSize = value; }
        }

        public int PageCount
        {
            get { return this.pageCount; }
            set { this.pageCount = value; }
        }

        bool isSetRecordCount;
        public int RecordCount
        {
            get { return this.recordCount; }
            set
            {
                this.recordCount = value;
                isSetRecordCount = true;
                this.pageCount = (int)Math.Ceiling((double)this.recordCount / this.pageSize);
            }
        }

        /// <summary>
        /// 是否设置过 RecordCount
        /// </summary>
        internal bool IsSetRecordCount
        {
            get { return this.isSetRecordCount; }
        }


        public string Fields
        {
            get { return this.fields; }
            set { this.fields = value; }
        }

        public string Tables
        {
            get { return this.tables; }
            set { this.tables = value; }
        }

        protected bool isOrderCalcResult =  false;
        public string Order
        {
            get
            {
                if (!this.isOrderCalcResult)
                    this.order = BuildOrder();
                return this.order;
            }
        }

        protected string defaultOrder;
        public string DefaultOrder
        {
            get { return this.defaultOrder; }
            set { this.defaultOrder = value; }
        }

        public string Group
        {
            get { return this.group; }
            set { this.group = value; }
        }

        #endregion

        #region Param

        PagingParam param;

        public PagingParam Param
        {
            get
            {
                if (this.param == null)
                    this.param = new PagingParam();
                return this.param;
            }
        }

        #endregion


        #region 虚方法

        protected abstract string BuildOrder();

        #endregion


    }
}

using System;
using System.Data.SqlClient;
using Epic.Components;
using System.ComponentModel;
using Epic.Data.Schema;

namespace Epic.Framework.ConsoleApplication
{
    #region Product
    /// <summary>
    ///  商品标签 : 新品速递（一定时间后自动去掉），特价折扣品、限量商品，奢侈品，明星产品，热卖商品，推荐商品
    /// </summary>
    public enum ProductLableType {
        /// <summary>
        /// 没有标签
        /// </summary>
        [Description("没有标签")]
        None,
        /// <summary>
        /// 新品速递
        /// </summary>
       [Description("新品速递")]
       New,

        /// <summary>
       /// 特价折扣品
        /// </summary>
       [Description("特价折扣品")]
       Special,

        /// <summary>
       /// 限量商品
        /// </summary>
        [Description("限量商品")]
        Limited,

        /// <summary>
        /// 奢侈品
        /// </summary>
        [Description("奢侈品")]
        Luxury,

        /// <summary>
        /// 明星产品
        /// </summary>
        [Description("明星产品")]
        Star,

        /// <summary>
        /// 热卖商品
        /// </summary>
        [Description("热卖商品")]
        Hot,

        /// <summary>
        /// 推荐商品
        /// </summary>
        [Description("推荐商品")]
        Featured 

    }
    /// <summary>
    /// 商品状态
    /// </summary>
    public enum ProductCommodType
    {
        /// <summary>
        ///  下架
        /// </summary>
        [Description("下架")]
        Default,
        /// <summary>
        /// 上架 
        /// </summary>
        [Description("上架")]
        OnSales,

        /// <summary>
        /// 下架状态
        /// </summary>
        [Description("下架状态")]
        DelState,

        /// <summary>
        /// 标记删除
        /// </summary>
        [Description("标记删除")]
        Marked,
    }

    /// <summary>
    /// 审核状态
    /// </summary>
    [Flags]
    public enum ProductAuditSateType
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Description("默认")]
        Default     = 1 << 1,

        /// <summary>
        /// 不符合标准
        /// </summary>
        [Description("不符合标准")]
        Fail        = 1 << 2,

        /// <summary>
        /// 已审核 
        /// </summary>
        [Description("已审核")]
        Approval    = 1 << 3,

        /// <summary>
        /// 未通过
        /// </summary>
        [Description("未通过")]
        UnApproval        = 1 << 4,
        /// <summary>
        /// 修改通过
        /// </summary>
        [Description("修改通过")]
        ModifiedPass = 1 << 5,

        /// <summary>
        /// 全部未审核
        /// </summary>
        [Description("全部未审核")]
        Unaudited = Default | UnApproval,

        /// <summary>
        /// 全部已审核
        /// </summary>
        [Description("全部已审核")]
        Audited = Approval | ModifiedPass
    }
    /// <summary>
    /// 商品类型
    /// </summary>
    public enum ProductType
    {
        /// <summary>
        /// 代购商品
        /// </summary>
        [Description("代购商品")]
        Agent,
        /// <summary>
        /// 一口价商品
        /// </summary>
        [Description("一口价商品")]
        Fixedprice,
        /// <summary>
        /// 特殊商品
        /// </summary>
        [Description("特殊商品")]
        Stock,
    }
    #endregion




    /// <summary>
    /// 标记删除状态
    /// </summary>
    public enum DelSateType : byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("标记删除")]
        Marked
    }

    /// <summary>
    /// Author:         WANGYALONG
    /// Description:    商品 实体
    /// Copyright:      (c) 2000 - 2010 EpicLab Corporation, All rights reserved.
    /// </summary>
    /// <history>
    ///	Create: WANGYALONG, 2010-09-16;
    /// </history>
    public partial class ProductionProduct
	{

        #region Properties


        /// <summary>
        /// 
        /// </summary>
        [ColumnSchema]
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// 商品名称
        /// </summary>
         [ColumnSchema]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        /// 外文名称
        /// </summary>
        [ColumnSchema]
        public string ForeignName
        {
            get { return this.foreignName; }
            set { this.foreignName = value; }
        }
        /// <summary>
        /// 名称格式如 <span style='color:red'>{0}</span>
        /// </summary>
        [ColumnSchema]
        public string NameFormat
        {
            get { return this.nameFormat; }
            set { this.nameFormat = value; }
        }
        /// <summary>
        /// 商品名解释, 如 商品名 长焦新品，突破想象！
        /// </summary>
        [ColumnSchema]
        public string NameDescription
        {
            get { return this.nameDescription; }
            set { this.nameDescription = value; }
        }
        /// <summary>
        /// 类型ID
        /// </summary>
        [DisplayName("分类")]
        [ColumnSchema]
        public int CategoryID
        {
            get { return this.categoryID; }
            set { this.categoryID = value; }
        }


        /// <summary>
        /// 品牌ID
        /// </summary>
        [DisplayName("品牌")]
        [ColumnSchema]
        public int BrandID
        {
            get { return this.brandID; }
            set { this.brandID = value; }
        }
        /// <summary>
        /// 商家编号
        /// </summary>
        [DisplayName("商家")]
        [ColumnSchema]
        public int StoreID
        {
            get { return this.storeID; }
            set { this.storeID = value; }
        }

        /// <summary>
        /// 商品规格 可多个 ID
        /// </summary>
        [DisplayName("规格")]
        [ColumnSchema]
        public string SpecIDS
        {
            get { return this.specIDS; }
            set { this.specIDS = value; }
        }
        

        /// <summary>
        /// 活动类型
        /// </summary>
        [DisplayName("活动类型")]
        public int ActivitieID
        {
            get { return this.activitieID; }
            set { this.activitieID = value; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        [ColumnSchema]
        public int Amount
        {
            get { return this.amount; }
            set { this.amount = value; }
        }
        /// <summary>
        /// 浏览次数
        /// </summary>
        [DisplayName("浏览次数")]
        [ColumnSchema]
        public int Views
        {
            get { return this.views; }
            set { this.views = value; }
        }
        /// <summary>
        /// 销量
        /// </summary>
        [DisplayName("销量")]
        [ColumnSchema]
        public int Sales
        {
            get { return this.sales; }
            set { this.sales = value; }
        }
        /// <summary>
        /// 摘要
        /// </summary>
        [DisplayName("摘要")]
        [ColumnSchema]
        public string Summary
        {
            get { return this.summary; }
            set { this.summary = value; }
        }
        /// <summary>
        /// 介绍
        /// </summary>
        [DisplayName("介绍")]
        [ColumnSchema]
        public string Intro
        {
            get { return this.intro; }
            set { this.intro = value; }
        }
        /// <summary>
        /// 外文介绍
        /// </summary>
        [DisplayName("外文介绍")]
        [ColumnSchema]
        public string ForeignIntro
        {
            get { return this.foreignIntro; }
            set { this.foreignIntro = value; }
        }
        /// <summary>
        /// 商品货号
        /// </summary>
        [DisplayName("国外网站货号")]
        [ColumnSchema]
        public string ItemNO
        {
            get { return this.itemNO; }
            set { this.itemNO = value; }
        }
        /// <summary>
        /// 来源编号
        /// </summary>
        [DisplayName("来源编号")]
        [ColumnSchema]
        public int SourceID
        {
            get { return this.sourceID; }
            set { this.sourceID = value; }
        }
        /// <summary>
        /// 商品来源
        /// </summary>
        [DisplayName("商品来源")]
        [ColumnSchema]
        public string SourceUrl
        {
            get { return this.sourceUrl; }
            set { this.sourceUrl = value; }
        }
        /// <summary>
        /// 国际运费
        /// </summary>
        [DisplayName("国际运费")]
        [ColumnSchema]
        public decimal InternationalShipping
        {
            get { return this.internationalShipping; }
            set { this.internationalShipping = value; }
        }
        /// <summary>
        /// 国外原价
        /// </summary>
        [DisplayName("国外原价")]
        [ColumnSchema]
        public decimal ForeignOriginalPrice
        {
            get { return this.foreignOriginalPrice; }
            set { this.foreignOriginalPrice = value; }
        }
        /// <summary>
        /// 国外折扣价
        /// </summary>
        [DisplayName("国外折扣价")]
        [ColumnSchema]
        public decimal ForeignDiscountPrice
        {
            get { return this.foreignDiscountPrice; }
            set { this.foreignDiscountPrice = value; }
        }
        /// <summary>
        /// 国外运费
        /// </summary>
        [DisplayName("国外运费")]
        [ColumnSchema]
        public decimal ForeignShipping
        {
            get { return this.foreignShipping; }
            set { this.foreignShipping = value; }
        }
        /// <summary>
        /// 国外税
        /// </summary>
        [DisplayName("国外税")]
        [ColumnSchema]
        public decimal ForeignTax
        {
            get { return this.foreignTax; }
            set { this.foreignTax = value; }
        }
        /// <summary>
        /// 国外总价
        /// </summary>
        [DisplayName("国外总价")]
        [ColumnSchema]
        public decimal ForeignTotalPrice
        {
            get { return this.foreignTotalPrice; }
            set { this.foreignTotalPrice = value; }
        }

        /// <summary>
        /// 市场价
        /// </summary>
        [DisplayName("国内市场价")]
        [ColumnSchema]
        public decimal OriginalPrice
        {
            get { return this.originalPrice; }
            set { this.originalPrice = value; }
        }
        /// <summary>
        /// 人民币最终价 折扣价
        /// </summary>
        [DisplayName("国内折扣价")]
        [ColumnSchema]
        public decimal DiscountPrice
        {
            get { return this.discountPrice; }
            set { this.discountPrice = value; }
        }
        /// <summary>
        /// 运费
        /// </summary>
        [DisplayName("运费")]
        [ColumnSchema]
        public decimal Shipping
        {
            get { return this.shipping; }
            set { this.shipping = value; }
        }
        /// <summary>
        /// 税
        /// </summary>
        [DisplayName("税")]
        [ColumnSchema]
        public decimal Tax
        {
            get { return this.tax; }
            set { this.tax = value; }
        }
        /// <summary>
        /// 服务费
        /// </summary>
        [DisplayName("服务费")]
        [ColumnSchema]
        public decimal ServiceCharge
        {
            get { return this.serviceCharge; }
            set { this.serviceCharge = value; }
        }
        /// <summary>
        /// 是否显示价格组成
        /// </summary>
        
        public bool ShowPriceDetails
        {
            get
            {
                if (this.type == ProductType.Fixedprice || this.type == ProductType.Stock)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 重量
        /// </summary>
        [DisplayName("重量")]
        [ColumnSchema]
        public decimal Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }
        /// <summary>
        /// 商品图片: 格式 图片1;图片2
        /// </summary>
        [DisplayName("商品图片")]
        [ColumnSchema]
        public string Image
        {
            get { return this.image; }
            set { this.image = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [ColumnSchema]
        public string Memo
        {
            get { return this.memo; }
            set { this.memo = value; }
        }

        /// <summary>
        /// 上架时间
        /// </summary>
        [DisplayName("上架时间")]
        [ColumnSchema]
        public DateTime PlacedDate
        {
            get { return this.placedDate; }
            set { this.placedDate = value; }
        }


        /// <summary>
        /// SEO关键字
        /// </summary>
        [DisplayName("SEO 标题")]
        [ColumnSchema]
        public string SEOTitle
        {
            get { return this.sEOTitle; }
            set { this.sEOTitle = value; }

        }
        /// <summary>
        /// SEO关键字
        /// </summary>
        [DisplayName("SEO 关键字")]
        [ColumnSchema]
        public string SEOKeyword
        {
            get { return this.sEOKeyword; }
            set { this.sEOKeyword = value; }
        }
        /// <summary>
        /// SEO描述
        /// </summary>
        [DisplayName("SEO 描述")]
        [ColumnSchema]
        public string SEODescription
        {
            get { return this.sEODescription; }
            set { this.sEODescription = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("创建时间")]
        [ColumnSchema]
        public DateTime CreateDate
        {
            get { return this.createDate; }
            set { this.createDate = value; }
        }
        #endregion

        ProductAuditSateType auditStatus;
        ProductCommodType status;
        ProductType type;
        DelSateType delState;
        ProductLableType labelID;

        /// <summary>
        /// 商品类型: 代购商品 一口价商品 特殊商品
        /// </summary>
        public ProductType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// 商品标签 : 新品速递（一定时间后自动去掉），特价折扣品、限量商品，奢侈品，明星产品，热卖商品，推荐商品
        /// </summary>
        public ProductLableType LabelID
        {
            get { return this.labelID; }
            set { this.labelID = value; }
        }
        /// <summary>
        /// 审核状态: 未审核 已审核 为通过 修改通过
        /// </summary>
        [DisplayName("审核状态")]
        public ProductAuditSateType AuditStatus
        {

            get { return this.auditStatus; }
            set { this.auditStatus = value; }
        }
        /// <summary>
        /// 商品状态: 下架 下架 删除
        /// </summary>
        [DisplayName("商品状态")]
        public ProductCommodType Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        /// <summary>
        /// 是否允许上架
        /// </summary>
        public bool AllowOnSales
        {
            get
            {
                if ((this.auditStatus & ProductAuditSateType.Audited) == auditStatus)
                    return true;
                else
                    return false;
            }
            
           
        }

        /// <summary>
        /// 删除: 默认，删除，修改过
        /// </summary>
        [DisplayName("删除")]
        public DelSateType DelState
        {
            get { return this.delState; }
            set { this.delState = value; }
        }




        #region Method

        #region Parse

        public void Parse(SqlDataReader reader)
        {
            this.ID = reader.GetInt32(0);
            this.name = reader.GetString(1);
            this.foreignName = reader.GetString(2);
            this.nameFormat = reader.GetString(3);
            this.nameDescription = reader.GetString(4);
            this.categoryID = reader.GetInt32(5);
            this.brandID = reader.GetInt32(6);
            this.storeID = reader.GetInt32(7);
            this.type = (ProductType)reader.GetByte(8);
            this.specIDS = reader.GetString(9);
            this.labelID = (ProductLableType)reader.GetInt32(10);
            this.auditStatus = (ProductAuditSateType)reader.GetByte(11);
            this.status = (ProductCommodType)reader.GetByte(12);
            this.activitieID = reader.GetInt32(13);
            this.amount = reader.GetInt32(14);
            this.views = reader.GetInt32(15);
            this.sales = reader.GetInt32(16);
            this.summary = reader.GetString(17);
            this.intro = reader.GetString(18);
            this.foreignIntro = reader.GetString(19);
            this.itemNO = reader.GetString(20);
            this.sourceID = reader.GetInt32(21);
            this.sourceUrl = reader.GetString(22);
            this.internationalShippingRateID = reader.GetInt32(23);
            this.internationalShipping = reader.GetDecimal(24);
            this.foreignOriginalPrice = reader.GetDecimal(25);
            this.foreignDiscountPrice = reader.GetDecimal(26);
            this.foreignShipping = reader.GetDecimal(27);
            this.foreignTax = reader.GetDecimal(28);
            this.foreignTotalPrice = reader.GetDecimal(29);
            this.foreignFinalPrice = reader.GetDecimal(30);
            this.exchangeRateID = reader.GetInt32(31);
            this.originalPrice = reader.GetDecimal(32);
            this.discountPrice = reader.GetDecimal(33);
            this.totalPrice = reader.GetDecimal(34);
            this.finalPrice = reader.GetDecimal(35);
            this.shipping = reader.GetDecimal(36);
            this.tax = reader.GetDecimal(37);
            this.serviceChargeRateID = reader.GetInt32(38);
            this.serviceCharge = reader.GetDecimal(39);
            this.weight = reader.GetDecimal(40);
            this.image = reader.GetString(41);
            this.memo = reader.GetString(42);
            this.delState = (DelSateType)reader.GetByte(43);
            this.placedDate = reader.GetDateTime(44);
            this.sEOTitle = reader.GetString(45);
            this.sEOKeyword = reader.GetString(46);
            this.sEODescription = reader.GetString(47);
            this.createDate = reader.GetDateTime(48);
        }
        #endregion

        #endregion


    }

}

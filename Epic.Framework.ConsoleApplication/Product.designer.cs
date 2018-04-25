using System;
using System.Data.SqlClient;
using System.ComponentModel;
using Epic.Components;


namespace Epic.Framework.ConsoleApplication
{
    /// <summary>
    /// Author:         SLIGHTBOY
    /// Description:    商品 实体(生成)
    /// Copyright:      (c) 2000 - 2010 EpicLab Corporation, All rights reserved.
    /// </summary>
    /// <history>
    ///	Create: SLIGHTBOY, 2010-10-21;
    /// </history>
    public partial class ProductionProduct : Epic.Components.IComponent
    {
        #region Constructor


        #endregion

        #region Fields

        int id;
        string name;
        string foreignName;
        string nameFormat;
        string nameDescription;
        int categoryID;
        int brandID;
        int storeID;

        string specIDS;

        int activitieID;
        int amount;
        int views;
        int sales;
        string summary;
        string intro;
        string foreignIntro;
        string itemNO;
        int sourceID;
        string sourceUrl;
        int internationalShippingRateID;
        decimal internationalShipping;
        decimal foreignOriginalPrice;
        decimal foreignDiscountPrice;
        decimal foreignShipping;
        decimal foreignTax;
        decimal foreignTotalPrice;
        decimal foreignFinalPrice;
        int exchangeRateID;
        decimal originalPrice;
        decimal discountPrice;
        decimal totalPrice;
        decimal finalPrice;
        decimal shipping;
        decimal tax;
        int serviceChargeRateID;
        decimal serviceCharge;
        decimal weight;
        string image;
        string memo;
        DateTime placedDate;
        string sEOTitle;
        string sEOKeyword;
        string sEODescription;
        DateTime createDate;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        public int InternationalShippingRateID
        {
            get { return this.internationalShippingRateID; }
            set { this.internationalShippingRateID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        public decimal ForeignFinalPrice
        {
            get { return this.foreignFinalPrice; }
            set { this.foreignFinalPrice = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        public int ExchangeRateID
        {
            get { return this.exchangeRateID; }
            set { this.exchangeRateID = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        public decimal TotalPrice
        {
            get { return this.totalPrice; }
            set { this.totalPrice = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        public decimal FinalPrice
        {
            get { return this.finalPrice; }
            set { this.finalPrice = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        public int ServiceChargeRateID
        {
            get { return this.serviceChargeRateID; }
            set { this.serviceChargeRateID = value; }
        }
        


        #endregion


    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Data.Schema;
using System.ComponentModel;
using MongoDB.Bson;

namespace Epic.Framework.ConsoleApplication
{

    public enum RssItemState : int
    {
        Normal,
        Delete
    }

    [TableSchema("test")]
	public partial class RssItem
	{



        public ObjectId _id
        {
            get;
            set;
        }

		#region Properties

        [ColumnSchema(DbType = "int", IsDbGenerated = true, IsPrimaryKey = true)]
        [DisplayName("")]
        public int ID
        {
            get;
            set;
        }


        #region Constructor

        #endregion
        
        #region Fields
        
		#endregion
        
         int feedID;
        
        /// <summary>
        /// 
        /// </summary>
        [ColumnSchema(DbType = "int")]
        [DisplayName("")]
		public int FeedID
		{
			get { return this.feedID; }
			set { this.feedID = value; }
		}		
        
         int categoryID;
        
        /// <summary>
        /// 
        /// </summary>
        [ColumnSchema]
        [DisplayName("")]
		public int CategoryID
		{
			get { return this.categoryID; }
			set { this.categoryID = value; }
		}		
        
         string title;
        
        /// <summary>
        /// 
        /// </summary>
         [ColumnSchema(DbType = "nvarchar(50)")]
        [DisplayName("")]
		public string Title
		{
			get { return this.title; }
			set { this.title = value; }
		}		
        
         string content;
        
        /// <summary>
        /// 
        /// </summary>
        [ColumnSchema]
        [DisplayName("")]
		public string Content
		{
			get { return this.content; }
			set { this.content = value; }
		}		
        
         string url;
        
        /// <summary>
        /// 
        /// </summary>
        [ColumnSchema]
        [DisplayName("")]
		public string Url
		{
			get { return this.url; }
			set { this.url = value; }
		}		
        
         DateTime pubDate;
        
        /// <summary>
        /// 
        /// </summary>
        [ColumnSchema]
        [DisplayName("")]
		public DateTime PubDate
		{
			get { return this.pubDate; }
			set { this.pubDate = value; }
		}		
        
         RssItemState state;
        
        /// <summary>
        /// 
        /// </summary>
        [ColumnSchema]
        [DisplayName("")]
		public RssItemState State
		{
			get { return this.state; }
			set { this.state = value; }
		}		
        
         DateTime createDate;
        
        /// <summary>
        /// 
        /// </summary>
        [ColumnSchema]
        [DisplayName("")]
		public DateTime CreateDate
		{
			get { return this.createDate; }
			set { this.createDate = value; }
		}


        
        
		#endregion


        //public Epic.Framework.ConsoleApplication.新参数对象.Token[] IDS
        //{
        //    get;
        //    set;
        //}


        #region Method
        
        
        #endregion


    }

        
}

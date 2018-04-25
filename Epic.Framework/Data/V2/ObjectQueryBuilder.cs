using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.Data.Schema;
using System.Data;
using Epic.Components;

namespace Epic.Data.V2
{
	[Flags]
	public enum QueryFuncType
	{
		Select = 1 << 1,
		Insert = 1 << 2,
		Update = 1 << 3,
		Delect = 1 << 4,

		Count = 1 << 5,
		Min = 1 << 6,
		Max = 1 << 7,
		Sum = 1 << 8,
		Avg = 1 << 9,
		Paging = 1 << 10,



	}



	public sealed class ObjectQueryBuilder<T>
	{
		public QueryFuncType Func
		{
			get;
			internal set;
		}



		#region Expression

		Expression<Func<T, bool>> expression;

		public Expression<Func<T, bool>> Expression
		{
			get { return this.expression; }
		}

		internal void And(Expression<Func<T, bool>> expression)
		{
			this.expression = Expressions.InnerExpressionHelper.And(this.expression, expression);
		}
		internal void Or(Expression<Func<T, bool>> expression)
		{
			this.expression = Expressions.InnerExpressionHelper.Or(this.expression, expression);
		}



		void SqlAppend(string opertion, string query)
		{
			if (this.Where.Count == 0)
				this.where.Push(query);
			else
				this.where.Push(String.Format("({0} {1} {2})", this.where.Pop(), opertion, query));
		}

		internal void And(string query)
		{
			SqlAppend("And", query);
		}

		internal void Or(string query)
		{
			SqlAppend("Or", query);
		}

		#endregion


		bool count;

		public bool Count
		{
			get { return this.count; }
			internal set { this.count = value; }
		}

		string CountFragment
		{
			get
			{
				if (this.count)
					return "Count(*)";
				return String.Empty;
			}
		}


		int limit;
		public int Limit
		{
			get { return this.limit; }
			internal set { this.limit = value; }
		}

		string TopFragment
		{
			get
			{

				if (!this.count && this.limit > 0)
					return "TOP(" + this.limit + ")";
				return String.Empty;
			}
		}

		int skip;
		public int Skip
		{
			get { return this.skip; }
			internal set { this.skip = value; }
		}


		bool HasColumn
		{
			get
			{
				return this.column != null && this.column.Count > 0;
			}
		}

		List<string> column;
		public List<string> Column
		{
			get
			{
				if (this.column == null)
					this.column = new List<string>();
				return this.column;

			}
		}

		string ColumnFragment
		{
			get
			{
				if (this.QueryType == QueryType.Select && this.count) return String.Empty;
				if (this.HasColumn) return String.Join(", ", this.column);

				if (this.QueryType == QueryType.Update)
					return String.Join(", ", TableSchema<T>.Columns.Select(e => String.Format("[{0}] = @{0}", e.DbName)));
				if (this.QueryType == QueryType.Insert)
				{
                    var columns = String.Join(", ", TableSchema<T>.Columns.Select(e => String.Format("[{0}]", e.DbName)));
                    var values = String.Join(", ", TableSchema<T>.Columns.Select(e => String.Format("@{0}", e.DbName)));

					return String.Format("({0}) Values ({1})", columns, values);
				}


				return "*";
			}
		}



		string TableFragment
		{
			get
			{
				return TableSchema<T>.FullDBName; ;
			}
		}





		bool HasWhere
		{
			get
			{
				return this.where != null && this.where.Count > 0;
			}
		}


		Stack<string> where;
		Stack<string> Where
		{
			get
			{
				if (this.where == null)
					this.where = new Stack<string>();
				return this.where;
			}
		}


		string whereFragment
		{
			get
			{
				if (this.HasWhere)
					return "Where " + String.Join(" And ", this.where);
				return String.Empty;
			}
		}


		bool HasOrderBy
		{
			get
			{
				return this.orderBy != null && this.orderBy.Count > 0;
			}
		}


		Dictionary<string, SortDirection> orderBy;
		internal Dictionary<string, SortDirection> OrderBy
		{
			get
			{
				if (this.orderBy == null)
					this.orderBy = new Dictionary<string, SortDirection>();
				return this.orderBy;
			}
		}

		string OrderByFragment
		{
			get
			{
				if (this.HasOrderBy)
					return "Order By " + String.Join(", ", this.orderBy.Select(e => e.Key +" "+ e.Value));
				return String.Empty;
			}
		}

		ObjectParameterDictionary parameters;
		public ObjectParameterDictionary Parameters
		{
			get
			{
				if (this.parameters == null)
					this.parameters = new ObjectParameterDictionary();
				return this.parameters;
			}
		}

		internal QueryType QueryType { get; set; }


		


		public override string ToString()
		{
			var visitor = new Epic.Data.Expressions.SimpleExpressionVisitor();
			if (this.expression != null)
			{
				visitor.Build(TableSchema<T>.CustomColumns, this.expression);
				this.Where.Push(visitor.Condition);
				this.parameters = visitor.Arguments;
			}

			switch (this.QueryType)
			{
				case QueryType.Insert:
					return InsertBuild();
					break;
				case QueryType.Update:
					return UpdateBuild();
					break;
				case QueryType.Delete:
					return DeleteBuild();
				default:
					return SelectBuild();
					break;
			}
		}


		/* 分页语句
					string result = String.Format(@"
		Select @RecordCount = Count(*) From {1} {2} {3} {4}
		IF (@Skip >0 And @Limit > 0)

			WITH CTE as
			(
			SELECT {0}, ROW_NUMBER() OVER ({3}) as _ROWNUMBER From {1} {2} {3} {4}
			)
			SELECT {0} FROM CTE WHERE _ROWNUMBER BETWEEN @Skip+1 And @Skip + @Limit


		Else IF (@Limit > 0)

			Select TOP(@Limit) {0} From {1} {2} {3} {4}

		Else IF (@Skip > 0)

			WITH CTE as
			(
			SELECT {0}, ROW_NUMBER() OVER ({3}) as _ROWNUMBER From {1} {2} {3} {4}
			)
			SELECT {0} FROM CTE WHERE _ROWNUMBER BETWEEN @Skip+1

		Else

			Select {0} From {1} {2} {3} {4}
		");
		 */

		string PagingSelectBuild()
		{
			string result = "Select @RecordCount = Count(*) From {1} {2} {4};";

			if (this.skip > 0 && this.limit > 0)
				result += @"
	WITH CTE as
	(
	SELECT {0}, ROW_NUMBER() OVER ({3}) as _ROWNUMBER From {1} {2} {4}
	)
	SELECT {0} FROM CTE WHERE _ROWNUMBER BETWEEN @Skip+1 And @Skip + @Limit
";
			else
				result += "Select TOP(@Limit) {0} From {1} {2} {3} {4}";

			this.Parameters.Add("@Skip", this.skip);
			this.Parameters.Add("@Limit", this.limit);
			this.parameters.Add("@RecordCount", DbType.Int32, ParameterDirection.Output);

			return String.Format(result,
												this.ColumnFragment,
												 this.TableFragment,
												 this.whereFragment,
												 this.OrderByFragment,
												 String.Empty);

		}

		string SelectBuild()
		{
			if (this.Func == QueryFuncType.Paging)
				return PagingSelectBuild();
			string result;
			if (this.skip > 0)
			{
				result = String.Format(@"
		WITH _CTE AS
		(
		  SELECT TOP(@Skip) {0}, ROW_NUMBER() OVER ({3}) as _ROWNUMBER
		  FROM {1} {2} {4}
		)
		SELECT * FROM _CTE WHERE _ROWNUMBER > @Limit",
												 this.ColumnFragment,
												 this.TableFragment,
												 this.whereFragment,
												 this.OrderByFragment,
												 String.Empty
												 );
				this.Parameters.Add("@Skip", this.skip + this.limit);
				this.Parameters.Add("@Limit", this.skip);
			}
			else
				result = String.Format("Select {0} {1} {2} From {3} {4} {5} {6} {7}", this.CountFragment, this.TopFragment, this.ColumnFragment, this.TableFragment, this.whereFragment, this.OrderByFragment, String.Empty, String.Empty);
			// 0 AVG | COUNT | MAX | MIN | SUM
			// 1 TOP
			// 2 *
			// 3 TABLE
			// 4 WHERE
			// 5 ORDER BY
			// 6 GROUP BY
			// 7 FOR

			return result;
		}


		string InsertBuild()
		{
			var column = TableSchema<T>.NoneDbGeneratedColumns;
			var result = "INSERT INTO {0} ({1}) VALUES ({2});{3}";
			var names = String.Join(", ", column.Select(e => "[" + e.DbName + "]"));
			var values = String.Join(", ", column.Select(e => "@"+ e.DbName));
			string id = String.Empty;


			foreach (var item in TableSchema<T>.NoneDbGeneratedColumns)
			{
				this.Parameters.Add(item);
			}
			if (TableSchema<T>.DbGeneratedColumns.Count > 0)
			{
				id = "Set @" + TableSchema<T>.DbGeneratedColumns[0].DbName + " = SCOPE_IDENTITY();";
				this.Parameters.Add(TableSchema<T>.DbGeneratedColumns[0]).Direction = System.Data.ParameterDirection.Output;
			}

			return String.Format(result, this.TableFragment, names, values, id);
		}

		string UpdateBuild()
		{
			var column = String.Join(", ", TableSchema<T>.NoneDbGeneratedColumns.Select(e => String.Format("[{0}] = @{0}", e.DbName)));
			var result = "UPDATE {0} {1} SET {2} {3}";
			if (this.HasWhere)
			{
				return String.Format(result, this.TopFragment, this.TableFragment, column, this.whereFragment);
			}
			foreach (var item in TableSchema<T>.NoneDbGeneratedColumns)
			{
				this.Parameters.Add(item);
			}

			if (TableSchema<T>.DbGeneratedColumns.Count > 0)
			{
				result = String.Format(result, this.TopFragment, this.TableFragment, column, String.Format("WHERE [{0}] = @{0}", TableSchema<T>.DbGeneratedColumns[0].DbName));
				this.Parameters.Add(TableSchema<T>.DbGeneratedColumns[0]);
			}
			else
				throw new NotSupportedException("无增长类型更新不被支持");

			return result;
		}

		string DeleteBuild()
		{
			var result = "DELETE FROM {0} {1}";
			if (TableSchema<T>.DbGeneratedColumns.Count > 0)
			{
				result = String.Format(result, this.TableFragment, String.Format("WHERE [{0}] = @{0}", TableSchema<T>.DbGeneratedColumns[0].DbName));
				this.Parameters.Add(TableSchema<T>.DbGeneratedColumns[0]);
			}
			else
				result = String.Format(this.TableFragment, this.whereFragment);

			return result;
		}
	}
}

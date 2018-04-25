using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.NewParam.Rules.Expressions;
using Epic.Extensions;
using System.ComponentModel;
using Epic.Extensions.Expressions;
using System.Reflection;

namespace Epic.NewParam
{
    public class WebParamArrayItem<T, K> : BaseWebParamItem<T, K>, IWebParamItem where T : new()
    {

        internal static WebParamArrayItem<T, K> Create(WebParam<T> parent, string name)
        {
            var type = typeof(T);
            var property = type.GetProperty(name, BindingFlags.CreateInstance | BindingFlags.Public);
            if (property == null)
                return new WebParamManualArrayItem<T, K>(parent, name);
            return new WebParamArrayItem<T, K>(parent, property);
        }

        protected WebParamArrayItem()
        {
        }


        internal WebParamArrayItem(WebParam<T> parent, PropertyInfo property)
        {
            this.Parent = parent;
            this.Name = property.Name;
            this.Selector = CreateSelector(property);
        }

        internal WebParamArrayItem(WebParam<T> parent, Expression<Func<T, K[]>> expression)
        {
            this.Parent = parent;
            this.Name = Epic.FluentAPI.FluentAPIExtensions.GetComplexPropertyAccess(expression).Single().Name;
            this.Selector = expression.Compile();
        }


        #region 显示实现 IWebParamItem 接口

        object IWebParamItem.Value
        {
            get { return this.Value; }
        }

        #endregion


        protected Func<T, K[]> Selector
        {
            get;
            set;
        }


        public virtual K[] Value
        {
            get
            {
                this.Raise();
                return this.InnerValue;
            }
            internal set
            {
                var result = TypeDescriptor.GetProperties(this.Parent.Value).Find(this.Name, true);
                if (result == null) return;
                result.SetValue(this.Parent.Value, value);
            }
        }


        K[] InnerValue
        {
            get { return this.Selector(this.Parent.Value); }
        }

        Func<T, K[]> CreateSelector(string name)
        {
            var e = ExpressionExtensions.Parameter<T>("e");
            var p = System.Linq.Expressions.Expression.Property(e, this.Name);
            return System.Linq.Expressions.Expression.Lambda<Func<T, K[]>>(p, e).Compile();
        }

        Func<T, K[]> CreateSelector(PropertyInfo value)
        {
            var e = ExpressionExtensions.Parameter<T>("e");
            var p = System.Linq.Expressions.Expression.Property(e, value);
            return System.Linq.Expressions.Expression.Lambda<Func<T, K[]>>(p, e).Compile();
        }



        /// <summary>
        /// 转换器
        /// </summary>
        /// <param name="action"></param>
        internal override void RaiseEvent(ParseExpression<T, K> value)
        {
            if (this.RequiredExpression != null && this.State != WebParamState.Vaild) return;

            K[] result;

            if (value.ArrayAction(this.Original, out result))
            {
                if (result != null) this.Value = result;
                this.State = WebParamState.Vaild;
            }
            else
            {
                this.State = value.State;
                this.PushError(value);
            }

            this.Value = result;
        }

        /// <summary>
        /// 必须 验证
        /// </summary>
        /// <param name="value"></param>
        internal override void RaiseEvent(RequiredExpression<T, K> value)
        {
            if (value.Action(this.Original))
            {
                this.State = value.State;
                this.PushError(value);
            }
            else
            {
                this.State = WebParamState.Vaild;
            }
        }

        /// <summary>
        /// 验证器
        /// </summary>
        /// <param name="action"></param>
        internal override void RaiseEvent(ValidExpression<T, K> value)
        {
            if (!this.Parent.IsForce && this.State != WebParamState.Vaild) return;


            if (!this.InnerValue.TrueForAll(value.Action))
            {
                this.State = value.State;
                this.PushError(value);
            }
        }

        /// <summary>
        /// Change 改变 Value 值
        /// </summary>
        /// <param name="action"></param>
        internal override void RaiseEvent(FuncExpression<T, K> value)
        {
            if (!this.Parent.IsForce && this.State != WebParamState.Vaild) return;

            bool isChanged = false;
            var array = this.InnerValue;
            for (int i = 0; i < array.Length; i++)
            {
                var item = value.Action(array[i]);
                if (Object.Equals(item, array[i]))
                {
                    isChanged = true;
                    array[i] = item;
                }
            }

            if (isChanged)
                this.Value = array;
        }

        /// <summary>
        /// Each, 处理事务
        /// </summary>
        /// <param name="action"></param>
        internal override void RaiseEvent(ActionExpression<T, K> value)
        {
            if (!this.Parent.IsForce && this.State != WebParamState.Vaild) return;

            this.InnerValue.ForEach(value.Action);
        }

        internal override void Raise()
        {
            if (this.IsCalc) return;
            this.RaiseEvent(this.RequiredExpression);
            this.AutoAttachParser();
            this.RaiseEvent(this.ParseExpression);
            this.RaiseEvent(this.Expression);

            this.IsCalc = true;
        }


        protected override void AutoAttachParser()
        {
            if (this.ParseExpression == null)
                this.ParseExpression = new ParseExpression<T, K>(DefaultParseFunc<T>.QueryArray<K>(), "ParseError");
        }

    }
}

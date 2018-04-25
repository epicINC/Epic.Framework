using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Web.Expressions;

namespace Epic.Web
{
    #region 状态枚举

    /// <summary>
    /// Author:     SLIGHTBOY
    /// Description:	Http 参数类型状态
    /// Copyright:		(c) 2000 - 2008 EpicLab Corporation, All rights reserved.
    /// </summary>
    /// <history>
    ///	Create:	SLIGHTBOY, 2008-4-8;
    /// </history>
    [Flags]
    public enum HttpParamStateType
    {
        /// <summary>
        /// 默认 null
        /// </summary>
        Default = 1,

        /// <summary>
        /// 转换出错, 类型错误
        /// </summary>
        ParseError = 2,

        /// <summary>
        /// 超过范围
        /// </summary>
        OutOfRange = 4,

        /// <summary>
        /// 验证失败
        /// </summary>
        ValidateFail = 8,

        /// <summary>
        /// 空 ""
        /// </summary>
        Empty = 16,

        /// <summary>
        /// 有值
        /// </summary>
        HasValue = 32,

        /// <summary>
        /// 有效
        /// </summary>
        Vaild = Empty | HasValue 
    }

    #endregion


    public abstract class HttpParam
    {
        protected bool hasValue;
        internal bool isParam;
        protected HttpParamStateType state;
        protected string name;
        protected string original;


        public bool HasValue
        {
            get { return this.hasValue; }
        }

        public HttpParamStateType State
        {
            get { RaiseRun(); return this.state; }
            internal set { this.state = value; }
        }

        public bool HasName
        {
            get { return String.IsNullOrEmpty(this.name); }
        }

        public string Name
        {
            get { return this.name; }
        }

        public string Original
        {
            get { return this.original; }
        }

        public bool IsParam
        {
            get { return this.isParam; }
        }

        protected abstract void RaiseRun();
        public abstract object GetValueOrOriginal();
    }

   

    /// <summary>
    /// Author:     SLIGHTBOY
    /// Description:	基础参数类型
    /// Copyright:		(c) 2000 - 2008 EpicLab Corporation, All rights reserved.
    /// </summary>
    /// <history>
    ///	Create:	SLIGHTBOY, 2008-4-8;
    /// </history>
    /// <typeparam name="T">参数类型</typeparam>
    public class HttpParam<T> : HttpParam
    {
        internal T value;

        bool isChanged;

        ParamExpression<T> exp;


        public static HttpParam<T> Error(T value)
        {
            return Error<T>(null, value);
        }

        public static HttpParam<T> Error<T>(string name, T value)
        {
            var result = new HttpParam<T>();
            result.name = name;
            result.value = value;
            result.state = HttpParamStateType.ParseError;
            result.hasValue = true;
            return result;
        }


        public static HttpParam<T> Valid(T value)
        {
            return Valid<T>(null, value);
        }


        public static HttpParam<T> Valid<T>(string name, T value)
        {
            var result = new HttpParam<T>();
            result.name = name;
            result.value = value;
            result.state = HttpParamStateType.Vaild;
            result.hasValue = true;
            return result;
        }

        HttpParam(){}

        public HttpParam(string name, string original)
        {
            this.name = name;
            this.original = original;
            this.hasValue = original != null;
        }




        public T Value
        {
            get
            {
                if (!this.HasValue) throw new InvalidOperationException("value");
                RaiseRun();
                return this.value;
            }
        }


        public T GetValueOrDefault()
        {
            return this.value;
        }

        public T GetValueOrDefault(T defaultValue)
        {
            if (!this.HasValue)
                return defaultValue;
            return this.value;
        }

        public override object GetValueOrOriginal()
        {
            if (this.hasValue || this.value != null)
                return this.value;
            return this.original;
        }

        public override bool Equals(object other)
        {
            if (!this.hasValue)
                return (other == null);
            if (other == null)
                return false;
            return this.value.Equals(other);
        }

        public override int GetHashCode()
        {
            if (!this.HasValue)
                return 0;
            return this.value.GetHashCode();
        }

        public override string ToString()
        {
            if (!this.HasValue)
                return String.Empty;
            return this.value.ToString();
        }


        #region Parse 转换方法

        public HttpParam<T> Parse(ParseAction<string, T> action)
        {
            this.isChanged = true;
            this.exp += new ParseExpression<T>(action);
            return this;
        }

        #endregion


        public bool Test(Predicate<T> action)
        {
            return action(this.value);
        }


        public T Test(Func<T, T> action)
        {
            return action(this.value);
        }


        public void Test(Action<T> action)
        {
            action(this.value);
        }

        #region Do 操作

        public HttpParam<T> Do(Func<T, T> action)
        {
            this.isChanged = true;
            this.exp += new FuncExpression<T>(action);
            return this;
        }


        public HttpParam<T> Do(Action<T> action)
        {
            this.isChanged = true;
            this.exp += new ActionExpression<T>(action);
            return this;
        }


        #endregion

        #region Validator 校验

        public HttpParam<T> Validator(Predicate<T> action)
        {
            this.isChanged = true;
            this.exp += new ValidatorExpression<T>(action);
            return this;
        }

        public HttpParam<T> Filter(Predicate<T> action)
        {
            return this.Validator(action);
        }

        #endregion

        #region Range 范围

        public HttpParam<T> Range(Predicate<T> action)
        {
            this.isChanged = true;
            this.exp += new RangeExpression<T>(action);
            return this;
        }

        #endregion


        public void Throw(object o)
        {
            throw new ApplicationException(o + "");
        }


        #region RaiseEvent

        internal void RaiseEvent(ParseAction<string, T> action, HttpParamStateType state)
        {
            if (action != null)
                this.state = action(this.original, out this.value) ? HttpParamStateType.Vaild : state;
        }

        internal void RaiseEvent(Predicate<T> action, HttpParamStateType state)
        {
            if (action == null) return;
            if (this.state != HttpParamStateType.Vaild) return;
            this.state = action(this.value) ? HttpParamStateType.Vaild : state;
        }

        internal void RaiseEvent(Func<T, T> action)
        {
            if (action == null) return;
            if (this.state != HttpParamStateType.Vaild) return;
            this.value = action(this.value);
        }

        internal void RaiseEvent(Action<T> action)
        {
            if (action == null) return;
            if (this.state != HttpParamStateType.Vaild) return;
            action(this.value);
        }

        #endregion

        protected override void RaiseRun()
        {
            if (this.isChanged && this.exp != null)
            {
                this.exp.Build(this);
                this.isChanged = false;
            }
        }

        public static implicit operator T(HttpParam<T> value)
        {
            return value.GetValueOrDefault();
        }
    }
}

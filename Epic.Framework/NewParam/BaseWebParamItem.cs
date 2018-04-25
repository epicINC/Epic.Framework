using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Epic.Extensions.Expressions;
using Epic.NewParam.Rules.Expressions;

namespace Epic.NewParam
{
    public abstract class BaseWebParamItem<T, K> where T : new()
    {

        /// <summary>
        /// 状态
        /// </summary>
        public WebParamState State
        {
            get;
            protected set;
        }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 别名(取值)
        /// </summary>
        public string Alias
        {
            get;
            set;
        }

        /// <summary>
        /// 原始值
        /// </summary>
        public string Original
        {
            get;
            internal set;
        }

        public bool HasError
        {
            get { return this.Errors != null && this.Errors.Count > 0; }
        }




        /// <summary>
        /// 是否已计算表达式
        /// </summary>
        internal bool IsCalc
        {
            get;
            set;
        }

        internal WebParam<T> Parent
        {
            get;
            set;
        }

        internal ParamExpression<T, K> ParseExpression
        {
            get;
            set;
        }

        internal ParamExpression<T, K> RequiredExpression
        {
            get;
            set;
        }

        internal ParamExpression<T, K> Expression
        {
            get;
            set;
        }

        List<WebParamValidResult> errors;
        public List<WebParamValidResult> Errors
        {
            get
            {
                if (this.errors == null)
                    this.errors = new List<WebParamValidResult>();
                return this.errors;
            }
        }

        #region Raise



        protected void PushError(ParamExpression<T, K> value)
        {
            var result = new WebParamValidResult() { State = value.State, Message = value.Message };
            this.Errors.Add(result);
        }


        internal abstract void RaiseEvent(ParseExpression<T, K> value);
        internal abstract void RaiseEvent(RequiredExpression<T, K> value);
        internal abstract void RaiseEvent(ValidExpression<T, K> value);
        internal abstract void RaiseEvent(FuncExpression<T, K> value);
        internal abstract void RaiseEvent(ActionExpression<T, K> value);



        internal void RaiseEvent(ParamExpression<T, K> value)
        {
            if (value == null) return;
            value.Execute(this);
        }


        protected abstract void AutoAttachParser();

        /// <summary>
        /// 触发事件
        /// </summary>
        internal abstract void Raise();

        #endregion
    }
}

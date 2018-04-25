using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam
{

    public class WebParamValidator<T, K> : BaseWebParamValidator<T, K>
    {
        #region Static 静态方法

        internal static WebParamValidator<T, K> Parse(Func<K, bool> action, string message = null)
        {

            return new WebParamValidator<T, K>(WebParamValidatorNodeType.Parse, action, message);
        }

        internal static WebParamValidator<T, K> Required(Func<K, bool> action, string message = null)
        {
            return new WebParamValidator<T, K>(WebParamValidatorNodeType.Required, action, message);
        }

        internal static WebParamValidator<T, K> Valid(Func<K, bool> action, string message = null)
        {
            return new WebParamValidator<T, K>(WebParamValidatorNodeType.PreParse, action, message);
        }      



        #endregion

        #region ctor 构造函数

        internal WebParamValidator(WebParamValidatorNodeType nodeType, Func<K, bool> action, string message = null) : base(nodeType, message)
        {
            this.Validator = action;
        }

        #endregion

        /*
        internal override bool Valid(WebParamItem<T, K> item)
        {
            K result;
            if (this.Validator(item.Original, out result))
            {
                this.State = WebParamState.Default;
                item.Value = result;
                return true;
            }
            this.State = WebParamState.ParseError;
            return false;
        }
        */

        public Func<K, bool> Validator
        {
            get;
            internal set;
        }


    }
}

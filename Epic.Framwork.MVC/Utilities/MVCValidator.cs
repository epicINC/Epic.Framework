using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Epic.MVC
{
    public class MVCValidator<T> : Epic.Utility.Validator<T>
    {
        public MVCValidator(ModelStateDictionary modelState, T value, bool isForce = false, bool isThrow = true)
            :base(value, isForce, isThrow)
        {
            this.modelState = modelState;
        }

        ModelStateDictionary modelState;

        protected override Utility.Validator<T> Valid<K>(string key, System.Linq.Expressions.Expression<Func<T, K>> expression, Func<K, bool> func, string error)
        {
            base.Valid<K>(key, expression, func, error);
            if (this.LastError != null && this.modelState != null)
                this.modelState.AddModelError(key, this.LastError);

            return this;
        }

    }
}

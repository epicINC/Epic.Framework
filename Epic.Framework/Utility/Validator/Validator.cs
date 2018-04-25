using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Epic.Utility
{
    public enum ValidResultType
    {
        Default,
        Empty,
        OutOfRange,
        ValidateFail
    }

    public class Validator<T> : IDisposable
    {
        T value;
        bool isForce;
        bool isThrow;
        Dictionary<string, string> result = new Dictionary<string, string>();

        public Validator(T value, bool isForce = false, bool isThrow = true)
        {
            this.isForce = isForce;
            this.isThrow = isThrow;

            this.value = value;
        }

        public bool IsForce
        {
            get { return this.isForce; }
            set { this.isForce = value; }
        }

        public bool IsThrow
        {
            get { return this.isThrow; }
            set { this.isThrow = value; }
        }

        public Dictionary<string, string> Result
        {
            get { return this.result; }
        }

        string lastError;
        public string LastError
        {
            get { return this.lastError; }
        }


        bool IsContinue(string key)
        {
            if (this.result.Count == 0 || this.isForce) return true;
            return !this.result.ContainsKey(key);
        }


        #region Method

        public Validator<T> IsEmpty<K>(Expression<Func<T, K>> expression, Func<K, bool> func)
        {
            return this.Valid<K>(expression, func, ValidResultType.Empty.ToString());
        }

        public Validator<T> Range<K>(Expression<Func<T, K>> expression, Func<K, bool> func)
        {
            return this.Valid<K>(expression, func, ValidResultType.OutOfRange.ToString());
        }

        public Validator<T> Valid<K>(Expression<Func<T, K>> expression, Func<K, bool> func)
        {
            return this.Valid<K>(expression, func, ValidResultType.ValidateFail.ToString());
        }

        Validator<T> Valid<K>(Expression<Func<T, K>> expression, Func<K, bool> func, string error)
        {
            return this.Valid<K>(SimpleExpression.Find(expression).Name, expression, func, error);
        }

        protected virtual Validator<T> Valid<K>(string key, Expression<Func<T, K>> expression, Func<K, bool> func, string error)
        {
            this.lastError = null;

            if (!this.IsContinue(key)) return this;

            if (!func(expression.Compile()(this.value)))
            {
                this.lastError = this.result[key] = error;
            }
            return this;
        }

        #endregion

        void Throw()
        {

        }



        public void Dispose()
        {
            if (this.isThrow)
                this.Throw();
        }
    }
}

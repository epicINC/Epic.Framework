using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Web.Mvc;
using Epic.Web;
using System.Collections.Specialized;
namespace Epic.MVC
{
    public abstract class EpicController : Controller
    {
        protected internal RedirectToRouteResult RedirectToAction<T>(Expression<Action<T>> expression) where T : EpicController
        {
            if (expression == null) throw new ArgumentNullException("expression"); 
            var call = expression.Body as MethodCallExpression;
            if (call == null) throw new ArgumentException("Expected method call"); 

            return this.RedirectToAction(call.Method.Name);
        }

        protected HttpParam<T> Form<T>(string key) where T : struct
        {
            return this.HttpContext.Request.Form.ToParam<T>(key);
        }

        protected HttpParam<T> QueryString<T>(string key) where T : struct
        {
            return this.HttpContext.Request.QueryString.ToParam<T>(key);
        }
    }


    public abstract class EpicController<T, B> : EpicController where B : Epic.Business.IBusiness<T>, new()
    {
        protected B Business
        {
            get { return Epic.Business.BusinessContainer<B>.Current; }
        }

        public bool IsValid(T value)
        {
            var collection = this.Business.IsValid(value);

            foreach (var item in collection)
            {
                this.ModelState.AddModelError(item.Key, item.Value);
            }
            return this.ModelState.IsValid;
        }


    }


    public static class ControllerExtensions
    {
    
    }

}

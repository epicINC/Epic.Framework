using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;


namespace Epic.MVC
{
    public class HtmlHelperWrapper
    {
        HtmlHelper helper;

        public HtmlHelperWrapper(ViewContext viewContext, IViewDataContainer viewDataContainer)
        {
            this.helper = new HtmlHelper(viewContext, viewDataContainer);
        }


        public string Label(string expression)
        {
            return this.helper.Label(expression).ToString();
        }

        public string Label(string expression, string labelText)
        {
            return this.helper.Label(expression, labelText).ToString();
        }


        /*
    public static MvcHtmlString Label(this HtmlHelper html, string expression);
    public static MvcHtmlString Label(this HtmlHelper html, string expression, string labelText);
    public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression);
    public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText);
    public static MvcHtmlString LabelForModel(this HtmlHelper html);
    public static MvcHtmlString LabelForModel(this HtmlHelper html, string labelText);

         */
    }
}

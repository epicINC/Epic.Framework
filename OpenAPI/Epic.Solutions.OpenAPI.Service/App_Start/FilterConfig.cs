using System.Web;
using System.Web.Mvc;

namespace Epic.Solutions.OpenAPI.Service
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

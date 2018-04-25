using Epic.Net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epic.Solutions.OpenAPI.Service.Controllers
{
    public class PTController : Controller
    {
        //
        // GET: /PT/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            var queryString = this.DecodeTicket(this.HttpContext.Request.Params["ticket"]);

            if (queryString == null)
                queryString = this.HttpContext.Request.Params;
            else
            {
                queryString["redirect_uri"] = this.HttpContext.Request.Params["redirect_uri"];
                queryString["toappid"] = this.HttpContext.Request.Params["toappid"];
            }

            var result = this.SSOLogin(queryString);
            if (result != null)
                return result;

            return this.View();

        }



        NameValueCollection DecodeTicket(string ticket)
        {
            if (String.IsNullOrWhiteSpace(ticket)) return null;
            try
            {
                return HttpUtility.ParseQueryString(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(ticket)));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        ActionResult SSOLogin(NameValueCollection collection)
        {
            var result = new { client_id = String.IsNullOrWhiteSpace(collection["toappid"]) ? collection["appid"] : collection["toappid"], response_type = "code", redirect_uri = collection["redirect_uri"] };

            return this.Redirect("api/authorize", result);
        }

        [HttpPost]
        public ActionResult Login(string account, string password)
        {
            return this.HttpNotFound();
        }


        ActionResult Redirect<T>(string redirect_uri, T result) where T : class
        {

            if (String.IsNullOrWhiteSpace(redirect_uri))
                return this.Json(result);

            var url = new UrlBuilder(redirect_uri);
            url.AddQueryString(result);

            return this.Redirect(url.ToString());
        }

	}
}
using Epic.Net;
using Epic.OpenAPI.Components;
using Epic.OpenAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Epic.Solutions.OpenAPI.Service.Controllers
{



    [RoutePrefix("api/oauth2.0")]
    public class OAuthController : ApiController
    {
        [Route("authorize")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Authorize(string client_id, OAuthResponseType response_type, string state, string redirect_uri)
        {
            if (!this.User.Identity.IsAuthenticated)
                return this.RedirectToRoute("pt/Login", null);

            switch (response_type)
            {
                case OAuthResponseType.code:
                    return ServiceSide(client_id, state, redirect_uri);
                case OAuthResponseType.token:
                    return ClientSide(client_id, state, redirect_uri);
                default:
                    return this.BadRequest();
            }
        }

        IHttpActionResult ServiceSide(string client_id, string state, string redirect_uri)
        {
            var result = new { code = OAuthUtility.RNG(), openid = "", opnekey = "", state = state };

            return this.Redirect(redirect_uri, result);
        }

        IHttpActionResult ClientSide(string client_id, string state, string redirect_uri)
        {
            var result = new { token = OAuthUtility.RNG(), expires_in = 3600, refresh_token = OAuthUtility.RNG(), openid = "", opnekey = "", state = state };

            return this.Redirect(redirect_uri, result);
        }


        [Route("token")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Token(OAuthGrantType grant_type, string client_id, string client_secret, string code, string state, string redirect_uri)
        {
            if (grant_type == OAuthGrantType.authorization_code)
                return this.Redirect(redirect_uri, new { access_token = OAuthUtility.RNG(), expires_in = 7776000, refresh_token = OAuthUtility.RNG(), state = state });

            return this.BadRequest();
        }

        [Route("token")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Token(OAuthGrantType grant_type, string client_id, string client_secret, string refresh_token, string state)
        {
            if (grant_type == OAuthGrantType.refresh_token)
                return this.Redirect(null, new { access_token = OAuthUtility.RNG(), expires_in = 7776000, refresh_token = OAuthUtility.RNG(), state = state });

            return this.BadRequest();
        }



        IHttpActionResult Redirect<T>(string redirect_uri, T result) where T : class
        {

            if (String.IsNullOrWhiteSpace(redirect_uri))
                return this.Json(result);

            var url = new UrlBuilder(redirect_uri);
            url.AddQueryString(result);

            return this.Redirect(url.ToString()); 
        }
    }
}
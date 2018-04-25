using Epic.OpenAPI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Epic.Solutions.OpenAPI.Service.Controllers
{
    [RoutePrefix("api/client")]
    public class OAuthClientController : ApiController
    {

        public static Dictionary<string, string> GetQueryStrings(HttpRequestMessage request)
        {
            return request.GetQueryNameValuePairs().ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
        }


        [Route("receive")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult OnResponse()
        {

            var client = new OAuthClient("", "cc70e2a6ea454ae78b09af8cd9cc4581", "24edca451b0e4e5ab4a666fb8f53f0b3");
            client.ReceiveToken += client_ReceiveToken;
            client.Start(GetQueryStrings(this.Request));

            return this.Ok();

        }

        void client_ReceiveToken(string access_token, int expires_in, string refresh_token, string openid, string openkey, string state, System.Collections.Specialized.NameValueCollection collection)
        {
            throw new NotImplementedException();
        }

 
  

    }
}
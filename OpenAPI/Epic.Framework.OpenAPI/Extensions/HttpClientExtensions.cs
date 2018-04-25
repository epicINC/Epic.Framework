using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Epic.OpenAPI.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> GetAsJsonAsync(this HttpClient client)
        {
            return null;

        }

        public static Task<HttpResponseMessage> PutAsync(this HttpClient client, Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return client.SendAsync
                (
                    new HttpRequestMessage(HttpMethod.Get, requestUri) { Content = content },
                    cancellationToken
                );
        }

    }

}

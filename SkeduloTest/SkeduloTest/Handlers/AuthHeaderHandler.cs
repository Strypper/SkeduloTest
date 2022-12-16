using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkeduloTest.Handlers
{
    public class AuthHeaderHandler : DelegatingHandler
    {

        public AuthHeaderHandler()
        {
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiKey = "live_HWJY5fHxnRBCLIYCXNLnz9YLoOENoH9NdYDkDAMWVahrF4UoniRpHzbqedvCw0bm";

            //potentially refresh token here if it has expired etc.

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", string.Empty);
            request.Headers.Add("x-api-key", apiKey);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}

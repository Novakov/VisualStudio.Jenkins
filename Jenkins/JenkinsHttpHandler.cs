using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Jenkins
{
    public class JenkinsHttpHandler : HttpClientHandler
    {
        public ICredentialsProvider CredentialsProvider { get; set; }

        public JenkinsHttpHandler()
        {
            this.AllowAutoRedirect = false;
#if DEBUG
            this.Proxy = new WebProxy("localhost", 8888);
#endif
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var resp = await base.SendAsync(request, cancellationToken);

            if (resp.StatusCode != System.Net.HttpStatusCode.Forbidden)
            {
                return resp;
            }

            var cred = GetCredentials(request.RequestUri);

            if (cred == null)
            {
                return resp;
            }

            var netCred = cred.GetCredential(request.RequestUri, "Basic");

            var authText = string.Format("{0}:{1}", netCred.UserName, netCred.Password);

            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(authText));

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encoded);

            return await base.SendAsync(request, cancellationToken);
        }

        private ICredentials GetCredentials(Uri requestUri)
        {
            if (this.CredentialsProvider != null)
            {
                return this.CredentialsProvider.GetCredentials(requestUri);
            }
            else
            {
                return null;
            }
        }
    }
}

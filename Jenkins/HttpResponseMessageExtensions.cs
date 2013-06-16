using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jenkins
{
    static class HttpResponseMessageExtensions
    {
        public static HttpResponseMessage EnsureSuccessStatusCode(this HttpResponseMessage @this, bool allowRedirect)
        {
            if (allowRedirect && 300 <= (int)@this.StatusCode && (int)@this.StatusCode <= 399)
            {
                return @this;
            }

            return @this.EnsureSuccessStatusCode();
        }

        public static async Task<HttpResponseMessage> EnsureSuccessStatusCode(this Task<HttpResponseMessage> @this, bool allowRedirect)
        {
            var resp = await @this;
            return resp.EnsureSuccessStatusCode(allowRedirect);
        }
    }
}

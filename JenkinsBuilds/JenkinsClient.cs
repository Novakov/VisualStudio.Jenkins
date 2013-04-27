using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CredentialManagement;
using Niles.Client;

namespace JenkinsBuilds
{    
    public class JenkinsClient : JsonJenkinsClient
    {
        public Uri ServerUrl { get; private set; }

        public JenkinsClient(Uri serverUrl)
        {
            this.ServerUrl = serverUrl;
        }

        public void StartBuild(Uri jobUrl)
        {
            var buildUrl = jobUrl.AppendPath("build");

            try
            {
                var request = CreateRequest(buildUrl);
                
                request.Method = "POST";
                request.AllowAutoRedirect = false;

                request.GetRequestStream().Close();

                var resp = request.GetResponse();
                using (var sr = new System.IO.StreamReader(resp.GetResponseStream()))
                {
                    var s = sr.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                throw new ClientException("Could not access resource at: " + buildUrl, e);
            }
        }

        private HttpWebRequest CreateRequest(Uri url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            var cred = new Credential
            {
                Target = this.ServerUrl.ToString(),
                PersistanceType = PersistanceType.LocalComputer,
                Type = CredentialType.Generic
            };

            if (cred.Load())
            {
                request.UseDefaultCredentials = false;
                request.Credentials = new NetworkCredential(cred.Username, cred.Password);
                var authValue = cred.Username + ":" + cred.Password;
                var header = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(authValue));

                request.Headers.Add(HttpRequestHeader.Authorization, header);
            }

            return request;
        }
    }
}

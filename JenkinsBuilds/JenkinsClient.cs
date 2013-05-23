using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CredentialManagement;
using JenkinsBuilds.Jenkins;
using Niles.Client;
using Niles.Json;
using Niles.Model;
using Commons;

namespace JenkinsBuilds
{
    public class JenkinsClient : JsonJenkinsClient
    {
        private JenkinsJsonSerializer serializer;

        public Uri ServerUrl { get; private set; }

        public JenkinsClient(Uri serverUrl)
        {
            this.ServerUrl = serverUrl;

            this.serializer = new JenkinsJsonSerializer();
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

        public T GetResourceIfAvailable<T>(Uri url, string fetchTree)
            where T : class
        {
            try
            {
                var request = CreateRequest(GetAbsoluteUri(url, fetchTree));

                request.Method = "GET";
                request.AllowAutoRedirect = false;                

                var resp = request.GetResponse();

                return this.serializer.ReadObject<T>(resp.GetResponseStream());
            }
            catch (WebException e)
            {
                if (((HttpWebResponse)e.Response).StatusCode == HttpStatusCode.NotFound)
                {
                    return default(T);
                }

                throw new ClientException("Could not access resource at: " + url, e);
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

        private Uri GetAbsoluteUri(Uri resourceUri, string tree)
        {
            var relativeUri = ApiSuffix;
            if (!string.IsNullOrWhiteSpace(tree))
                relativeUri += "?tree=" + tree;

            var absoluteUri = new Uri(resourceUri, relativeUri);
            return absoluteUri;
        }


        public async Task<string> GetRawDataAsync(Uri uri)
        {
            var request = this.CreateRequest(uri);

            var response = await request.GetResponseAsync();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async Task<Version> GetVersion()
        {
            var request = this.CreateRequest(this.ServerUrl);

            var response = await request.GetResponseAsync();

            var version = response.Headers["X-Jenkins"];

            if (string.IsNullOrWhiteSpace(version))
            {
                return null;
            }

            return Version.Parse(version);
        }

        public async Task DownloadFileAsync(Stream destination, Uri url, IProgress<ProgressReport> progress)
        {
            var request = this.CreateRequest(url);

            var response = await request.GetResponseAsync();

            using (var stream = response.GetResponseStream())
            {
                await stream.CopyToAsync(destination, progress, totalSize: response.ContentLength);
            }
        }
    }
}

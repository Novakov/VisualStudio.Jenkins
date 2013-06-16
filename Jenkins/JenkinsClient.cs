using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CredentialManagement;
using Niles.Client;
using Niles.Json;
using Niles.Model;
using Commons;
using System.Net.Http;

namespace Jenkins
{
    public class JenkinsClient : JsonJenkinsClient
    {
        private JenkinsJsonSerializer serializer;

        private JenkinsHttpHandler handler;
        private HttpClient client;

        public Uri ServerUrl { get; private set; }

        public ICredentials Credentials { get; set; }

        public ICredentialsProvider CredentialsProvider
        {
            get { return this.handler.CredentialsProvider; }
            set { this.handler.CredentialsProvider = value; }
        }

        public JenkinsClient(Uri serverUrl)
        {
            this.ServerUrl = serverUrl;

            this.serializer = new JenkinsJsonSerializer();

            this.handler = new JenkinsHttpHandler();
            this.client = new HttpClient(this.handler);
        }

        public async Task StartBuild(Uri jobUrl)
        {
            var buildUrl = jobUrl.AppendPath("build");

            try
            {
                var resp = await this.client.PostAsync(buildUrl, null).EnsureSuccessStatusCode(true);
            }
            catch (HttpRequestException e)
            {
                throw new ClientException("Could not access resource at: " + buildUrl, e);
            }
        }

        public async Task<T> GetResourceIfAvailableAsync<T>(Uri url, string fetchTree)
            where T : class
        {
            try
            {
                var absoluteUri = GetAbsoluteUri(url, fetchTree);

                var resp = await this.client.GetAsync(absoluteUri);

                if (resp.StatusCode == HttpStatusCode.NotFound)
                {
                    return default(T);
                }

                return this.serializer.ReadObject<T>(await resp.Content.ReadAsStreamAsync());
            }
            catch (WebException e)
            {
                throw new ClientException("Could not access resource at: " + url, e);
            }
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
            return await this.client.GetStringAsync(uri);
        }

        public async Task<Version> GetVersion()
        {            
            var response = await client.GetAsync(this.ServerUrl);

            var version = response.Headers.Where(x => x.Key == "X-Jenkins").SelectMany(x => x.Value).SingleOrDefault();

            if (string.IsNullOrWhiteSpace(version))
            {
                return null;
            }

            return Version.Parse(version);
        }

        public async Task DownloadFileAsync(Stream destination, Uri url, IProgress<ProgressReport> progress)
        {            
            var response = await this.client.GetAsync(url);

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                await stream.CopyToAsync(destination, progress, totalSize: response.Content.Headers.ContentLength.Value);
            }
        }
    }
}

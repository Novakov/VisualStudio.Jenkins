using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
                var request = WebRequest.Create(buildUrl);

                request.GetResponse();
            }
            catch (WebException e)
            {
                throw new ClientException("Could not access resource at: " + buildUrl, e);
            }
        }
    }
}

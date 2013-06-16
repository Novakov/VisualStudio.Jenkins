using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CredentialManagement;
using Jenkins;
using Microsoft.VisualStudio.Shell.Interop;

namespace JenkinsBuilds
{
    [Export(typeof(IClientFactory))]
    public class ClientFactory : JenkinsBuilds.IClientFactory
    {
        public JenkinsClient GetClient(Uri serverUri)
        {
            var client = new JenkinsClient(serverUri);

            client.CredentialsProvider = new CredStoreProvider(serverUri);            

            return client;
        }

        public JenkinsClient GetClient(string serverUri)
        {
            return this.GetClient(new Uri(serverUri));
        }
    }
}

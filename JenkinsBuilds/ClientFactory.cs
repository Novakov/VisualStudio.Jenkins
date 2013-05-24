using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jenkins;

namespace JenkinsBuilds
{
    [Export(typeof(IClientFactory))]
    public class ClientFactory : JenkinsBuilds.IClientFactory
    {
        public JenkinsClient GetClient(Uri serverUri)
        {
            return new JenkinsClient(serverUri);
        }

        public JenkinsClient GetClient(string serverUri)
        {
            return this.GetClient(new Uri(serverUri));
        }
    }
}

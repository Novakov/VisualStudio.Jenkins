using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CredentialManagement;
using Jenkins;

namespace JenkinsBuilds
{
    public class CredStoreProvider : ICredentialsProvider
    {
        private Uri serverUri;

        public CredStoreProvider(Uri serverUri)
        {            
            this.serverUri = serverUri;
        }
        public System.Net.ICredentials GetCredentials(Uri requestUri)
        {
            var cred = new Credential
            {
                Target = serverUri.ToString(),
                PersistanceType = PersistanceType.LocalComputer,
                Type = CredentialType.Generic
            };

            if (cred.Load())
            {
                return new NetworkCredential(cred.Username, cred.Password);
            }

            cred = GetCredFromUser();

            if (cred != null)
            {
                return new NetworkCredential(cred.Username, cred.Password);
            }

            return null;
        }

        private Credential GetCredFromUser()
        {
            var prompt = CredentialsPromptProvider.Prompt;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                var cred = new Credential
                {
                    Type = CredentialType.Generic,
                    PersistanceType = PersistanceType.LocalComputer,
                    Target = serverUri.ToString(),
                    Username = prompt.Username,
                    Password = prompt.Password
                };

                cred.Save();

                return cred;
            }
            else
            {
                return null;
            }
        }
    }
}

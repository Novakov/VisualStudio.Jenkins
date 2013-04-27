using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CredentialManagement;

namespace JenkinsBuilds
{
    public class CredentialsPromptProvider
    {
        [Export]
        public static BaseCredentialsPrompt Prompt
        {
            get
            {
                return new VistaPrompt
                    {
                        GenericCredentials = true,
                        ShowSaveCheckBox = false
                    };
            }
        }
    }
}

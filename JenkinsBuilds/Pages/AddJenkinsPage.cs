using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CredentialManagement;
using JenkinsBuilds.Commands;
using Microsoft.TeamFoundation.Controls;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerPage(AddJenkinsPage.PageId, ParentPageId = BuildsPage.PageId)]
    public class AddJenkinsPage : Base.TeamExplorerPageBase<AddJenkinsPageView>
    {
        public const string PageId = "{1256AA90-290A-4656-8BEB-4AF1B171B6BB}";

        private BaseCredentialsPrompt prompt;

        public new AddJenkinsPageViewModel ViewModel { get { return (AddJenkinsPageViewModel)base.ViewModel; } }

        [ImportingConstructor]
        public AddJenkinsPage(BaseCredentialsPrompt prompt)
        {
            this.prompt = prompt;

            this.Title = "Add Jenkins";
            this.IsBusy = false;
        }

        private async void Save(object obj)
        {            
            if (this.ViewModel.RequiresAuthentication)
            {                
                if (this.prompt.ShowDialog() != DialogResult.OK)
                {
                    this.ShowError("If Jenkins requires authentication, valid credentials need to be provided");
                    return;
                }

                var cred = new Credential
                {
                    Type = CredentialType.Generic,
                    PersistanceType = PersistanceType.LocalComputer,
                    Target = this.ViewModel.Url,
                    Username = this.prompt.Username,
                    Password = this.prompt.Password
                };

                var b = cred.Save();
            }

            var client = new JenkinsClient(new Uri(this.ViewModel.Url));

            var version = await client.GetVersion();

            if (version == null)
            {
                this.ShowError("Url does not point to Jenkins instance");

                return;
            }

            Properties.Settings.Default.AddInstance(this.ViewModel.DisplayName, new Uri(this.ViewModel.Url), this.ViewModel.RequiresAuthentication);

            Properties.Settings.Default.Save();

            this.Close();
        }

        private void Back(object obj)
        {
            this.Close();
        }

        protected override AddJenkinsPageView CreateView()
        {
            return new AddJenkinsPageView();
        }

        protected override Base.ViewModelBase CreateViewModel()
        {
            return new AddJenkinsPageViewModel
            {
                CancelCommand = new DelegateCommand(Back),
                SaveCommand = new DelegateCommand(Save)
            };
        }
    }
}

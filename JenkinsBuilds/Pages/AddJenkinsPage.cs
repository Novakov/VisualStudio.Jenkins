using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CredentialManagement;
using Microsoft.TeamFoundation.Controls;
using VisualStudio.TeamExplorer;
using Commons.Wpf;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerPage(AddJenkinsPage.PageId, ParentPageId = BuildsPage.PageId)]
    public class AddJenkinsPage : TeamExplorerPageBase<AddJenkinsPageView>
    {
        public const string PageId = "{1256AA90-290A-4656-8BEB-4AF1B171B6BB}";

        private BaseCredentialsPrompt prompt;
        private IClientFactory clientFactory;

        public new AddJenkinsPageViewModel ViewModel { get { return (AddJenkinsPageViewModel)base.ViewModel; } }

        [ImportingConstructor]
        public AddJenkinsPage(BaseCredentialsPrompt prompt, IClientFactory clientFactory)
        {
            this.prompt = prompt;
            this.clientFactory = clientFactory;

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

            var client = this.clientFactory.GetClient(this.ViewModel.Url);

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

        protected override ViewModelBase CreateViewModel()
        {
            return new AddJenkinsPageViewModel
            {
                CancelCommand = new DelegateCommand(Back),
                SaveCommand = new DelegateCommand(Save)
            }.AllowWhenNoErrors(x => x.SaveCommand);
        }
    }
}

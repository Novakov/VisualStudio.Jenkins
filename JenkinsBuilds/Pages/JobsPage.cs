using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Commands;
using JenkinsBuilds.Configuration;
using JenkinsBuilds.Properties;
using Microsoft.TeamFoundation.Controls;
using Niles.Client;
using Niles.Model;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerPage(JobsPage.PageId, ParentPageId = BuildsPage.PageId)]
    public class JobsPage : Base.TeamExplorerPageBase<JobsPageView>
    {
        public const string PageId = "{074B6E0F-2690-4BAF-9CD3-F98C917DA15C}";

        private JenkinsInstance instance;

        public new JobsPageViewModel ViewModel { get { return (JobsPageViewModel)base.ViewModel; } }

        private void AddToFavourites(object obj)
        {         
            Properties.Settings.Default.AddJob(new Uri(this.instance.Url), ((JobViewModel)obj).JobUrl);
            Properties.Settings.Default.Save();

            this.Refresh();
        }

        public override void Initialize(object sender, PageInitializeEventArgs e)
        {
            base.Initialize(sender, e);

            this.instance = (JenkinsInstance)e.Context;

            this.Title = "Jobs on " + instance.DisplayName;
        }

        public override void Loaded(object sender, PageLoadedEventArgs e)
        {
            this.DuringBusy(async () => await this.LoadAsync());
        }

        public override void SaveContext(object sender, PageSaveContextEventArgs e)
        {
            e.Context = this.instance;
        }

        public override void Refresh()
        {
            this.DuringBusy(async () => await this.LoadAsync());
        }

        private async Task LoadAsync()
        {
            var client = new JsonJenkinsClient();

            Node node;

            try
            {
                node = await client.GetResourceAsync<Node>(this.instance.Url, "jobs[name,color,displayName,url,lastBuild[url,number,building,result,timestamp]]");
            }
            catch (Exception e)
            {
                this.ShowError("Unable to contact Jenkins instance at {0}: {1}", this.instance.Url, e.Message);

                return;
            }

            this.ViewModel.Jobs = from j in node.Jobs
                             let isFavourite = Settings.Default.IsFavourite(j.Url)
                             select new JobViewModel().LoadFrom(j).MarkFavourite(isFavourite);
        }

        protected override JobsPageView CreateView()
        {
            return new JobsPageView();
        }

        protected override Base.ViewModelBase CreateViewModel()
        {
            return new JobsPageViewModel
            {
                AddToFavouritesCommand = new DelegateCommand(this.AddToFavourites)
            };
        }
    }
}

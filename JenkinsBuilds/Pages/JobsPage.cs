using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Configuration;
using Microsoft.TeamFoundation.Controls;
using Niles.Client;
using Niles.Model;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerPage(JobsPage.PageId, ParentPageId = BuildsPage.PageId)]
    public class JobsPage : Base.TeamExplorerPageBase
    {
        public const string PageId = "{074B6E0F-2690-4BAF-9CD3-F98C917DA15C}";

        private JenkinsInstance instance;
        private JobsView view;
        
        public JobsPage()
        {
            this.PageContent = this.view = new JobsView();    
        }

        public override void Initialize(object sender, PageInitializeEventArgs e)
        {
            this.instance = (JenkinsInstance)e.Context;

            this.Title = "Jobs on " + instance.DisplayName;
        }

        public async override void Loaded(object sender, PageLoadedEventArgs e)
        {
            this.IsBusy = true;

            await this.LoadAsync();

            this.IsBusy = false;
        }

        public async override void Refresh()
        {
            this.IsBusy = true;

            await this.LoadAsync();

            this.IsBusy = false;
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

            this.view.Jobs = node.Jobs;
        }
    }
}

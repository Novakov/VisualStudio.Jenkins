using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Controls;
using Niles.Client;
using Niles.Model;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerSection(BuildsSection.SectionId, BuildsPage.PageId, 10)]
    public class BuildsSection : Base.TeamExplorerSectionBase
    {
        public const string SectionId = "{5D23BE7D-C7AA-4938-ACED-C4A26587CF7F}";

        private BuildsSectionView view;

        public BuildsSection()
        {
            this.Title = "Favourite jobs";

            this.SectionContent = this.view = new BuildsSectionView();

            this.IsExpanded = true;
            this.IsVisible = true;
        }

        public async override void Loaded(object sender, SectionLoadedEventArgs e)
        {
            this.IsBusy = true;

            await RefeshAsync();

            this.IsBusy = false;
        }

        private async Task RefeshAsync()
        {
            var client = new JsonJenkinsClient();

            var jobs = await Task.WhenAll(Properties.Settings.Default.FavouriteJobs.Select(x => client.GetResourceAsync<Job>(x.JobUrl, "name,color,displayName,url,lastBuild[url,number,building,result,timestamp]")));

            this.view.Jobs = from j in jobs
                             select new JobViewModel().LoadFrom(j);
        }
    }
}

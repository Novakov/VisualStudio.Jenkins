using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Properties;
using Microsoft.TeamFoundation.Controls;
using Niles.Client;
using Niles.Model;
using Niles.Monitor;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerSection(BuildsSection.SectionId, BuildsPage.PageId, 10)]
    public class BuildsSection : Base.TeamExplorerSectionBase
    {
        public const string SectionId = "{5D23BE7D-C7AA-4938-ACED-C4A26587CF7F}";

        private BuildsSectionView view;

        private IDictionary<Uri, BackgroundJenkinsMonitor> monitors;

        private Settings settings;

        public BuildsSection()
        {
            this.settings = Properties.Settings.Default;

            this.Title = "Favourite jobs";

            this.SectionContent = this.view = new BuildsSectionView();

            this.IsExpanded = true;
            this.IsVisible = true;

            this.monitors = new Dictionary<Uri, BackgroundJenkinsMonitor>();
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

            var jobFetchTasks = from j in this.settings.FavouriteJobs
                                select client.GetResourceAsync<Job>(j.JobUrl, JobViewModel.FetchTree);

            var jobs = await Task.WhenAll(jobFetchTasks);

            var vms = jobs.Select(x => new JobViewModel().LoadFrom(x)).ToArray();

            foreach (var item in vms)
            {
                BackgroundJenkinsMonitor monitor;
                
                if (!this.monitors.TryGetValue(item.ServerUrl, out monitor))
                {
                    monitor = new BackgroundJenkinsMonitor(item.ServerUrl);
                    monitor.JobFetchTree = JobViewModel.FetchTree;

                    this.monitors.Add(item.ServerUrl, monitor);
                }

                SubscribeMonitorEvents(item, monitor);
            }

            foreach (var item in this.monitors)
            {
                item.Value.Start();
            }

            this.view.Jobs = new ObservableCollection<JobViewModel>(vms);
        }

        private void SubscribeMonitorEvents(JobViewModel item, BackgroundJenkinsMonitor monitor)
        {
            monitor.BuildChanged += (s, e) =>
            {
                if (e.Job.Url == item.JobUrl) 
                {
                    item.LoadFrom(e.Job);               
                }
            };
        }

        public override void Dispose()
        {
            foreach (var item in this.monitors)
            {
                item.Value.Stop();
            }

            base.Dispose();
        }
    }
}

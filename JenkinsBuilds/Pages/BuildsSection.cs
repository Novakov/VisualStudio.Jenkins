using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Commands;
using JenkinsBuilds.Model;
using JenkinsBuilds.Properties;
using Microsoft.TeamFoundation.Controls;
using Niles.Client;
using Niles.Model;
using Niles.Monitor;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerSection(BuildsSection.SectionId, BuildsPage.PageId, 10)]
    public class BuildsSection : Base.TeamExplorerSectionBase<BuildsSectionView>
    {
        public const string SectionId = "{5D23BE7D-C7AA-4938-ACED-C4A26587CF7F}";        

        private IDictionary<Uri, BackgroundJenkinsMonitor> monitors;

        private Settings settings;

        public new BuildsSectionViewModel ViewModel { get { return (BuildsSectionViewModel)base.ViewModel; } }
        
        public BuildsSection()
        {
            this.settings = Properties.Settings.Default;

            this.Title = "Favourite jobs";
            
            this.IsExpanded = true;
            this.IsVisible = true;

            this.monitors = new Dictionary<Uri, BackgroundJenkinsMonitor>();           
        }

        private void RemoveFromFavourites(object obj)
        {
            var job = (JobModel)obj;

            this.settings.RemoveJob(job.Url);

            this.settings.Save();

            this.Refresh();
        }

        private void BuildNow(object obj)
        {
            var job = (JobModel)obj;

            new JenkinsClient(job.ServerUrl).StartBuild(job.Url);
        }

        public override void Loaded(object sender, SectionLoadedEventArgs e)
        {
            this.DuringBusy(async () => await this.RefeshAsync());
        }

        public override void Refresh()
        {
            this.DuringBusy(async () => await this.RefeshAsync());
        }

        private async Task RefeshAsync()
        {
            var jobFetchTasks = from i in this.settings.Instances
                                let client = new JenkinsClient(new Uri(i.Url))
                                from j in i.FavouriteJobs
                                select client.GetResourceAsync<Job>(j, JobModel.FetchTree)
                                    .ContinueWith(z => new JobModel().LoadFrom(z.Result));

            var vms = await Task.WhenAll(jobFetchTasks);

            this.StopMonitors();

            foreach (var item in vms)
            {
                BackgroundJenkinsMonitor monitor;
                
                if (!this.monitors.TryGetValue(item.ServerUrl, out monitor))
                {
                    monitor = new BackgroundJenkinsMonitor(item.ServerUrl);
                    monitor.JobFetchTree = JobModel.FetchTree;

                    this.monitors.Add(item.ServerUrl, monitor);
                }

                SubscribeMonitorEvents(item, monitor);
            }

            foreach (var item in this.monitors)
            {
                item.Value.Start();
            }

            this.ViewModel.Jobs = new ObservableCollection<JobModel>(vms);
        }

        private void SubscribeMonitorEvents(JobModel item, BackgroundJenkinsMonitor monitor)
        {
            monitor.BuildChanged += (s, e) =>
            {
                if (e.Job.Url == item.Url) 
                {
                    item.LoadFrom(e.Job);               
                }
            };
        }

        public override void Dispose()
        {
            StopMonitors();

            base.Dispose();
        }

        private void StopMonitors()
        {
            foreach (var item in this.monitors)
            {
                item.Value.Stop();
            }

            this.monitors.Clear();
        }

        protected override BuildsSectionView CreateView()
        {
            return new BuildsSectionView();
        }

        protected override Base.ViewModelBase CreateViewModel()
        {
            return new BuildsSectionViewModel
            {
                BuildNowCommand = new DelegateCommand(BuildNow),
                RemoveFromFavourites = new DelegateCommand(RemoveFromFavourites)
            };
        }
    }
}

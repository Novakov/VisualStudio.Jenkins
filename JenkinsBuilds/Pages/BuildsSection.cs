using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Model;
using JenkinsBuilds.Properties;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell.Interop;
using Niles.Client;
using Niles.Model;
using Niles.Monitor;
using Jenkins;
using VisualStudio.TeamExplorer;
using Commons.Wpf;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerSection(BuildsSection.SectionId, BuildsPage.PageId, 10)]
    public class BuildsSection : TeamExplorerSectionBase<BuildsSectionView>
    {
        public const string SectionId = "{5D23BE7D-C7AA-4938-ACED-C4A26587CF7F}";

        private IWindowManager windowManager;
        private Settings settings;
        private IClientFactory clientFactory;  

        private IDictionary<Uri, BackgroundJenkinsMonitor> monitors;              

        public new BuildsSectionViewModel ViewModel { get { return (BuildsSectionViewModel)base.ViewModel; } }

        [ImportingConstructor]
        public BuildsSection(IWindowManager windowManager, IClientFactory clientFactory)
        {
            this.settings = Properties.Settings.Default;            

            this.windowManager = windowManager;
            this.clientFactory = clientFactory;

            this.Title = "Favorite jobs";

            this.IsExpanded = true;
            this.IsVisible = true;

            this.monitors = new Dictionary<Uri, BackgroundJenkinsMonitor>();            
        }

        private void RemoveFromFavorites(JobModel job)
        {
            this.settings.RemoveJob(job.Url);

            this.settings.Save();

            this.Refresh();
        }

        private async void BuildNow(JobModel job)
        {
            try
            {
                await this.clientFactory.GetClient(job.ServerUrl).StartBuild(job.Url);
            }
            catch (Exception e)
            {
                this.ShowError("Starting build failed!\n{0}", e.Message);
            }
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
                                let client = this.clientFactory.GetClient(i.Url)
                                from j in i.FavoriteJobs
                                select client.GetResourceAsync<Job>(j, JobModel.FetchTree);                                              

            this.StopMonitors();

            var jobs = new List<JobModel>();

            foreach (var task in jobFetchTasks)
            {
                JobModel item;

                try
                {
                    item = new JobModel().LoadFrom(await task);
                }
                catch (Exception e)
                {
                    this.ShowError("Failed to get job: {0}", e.Message);
                    continue;
                }

                jobs.Add(item);

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

            this.ViewModel.Jobs = new ObservableCollection<JobModel>(jobs);
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

        protected override ViewModelBase CreateViewModel()
        {
            return new BuildsSectionViewModel
            {
                BuildNowCommand = new DelegateCommand<JobModel>(BuildNow),
                RemoveFromFavorites = new DelegateCommand<JobModel>(RemoveFromFavorites),
                OpenBuildDetailsCommand = new DelegateCommand<JobModel>(OpenBuildDetails),
                ViewBuildsCommand = new DelegateCommand<JobModel>(ViewBuilds)
            };
        }

        private void ViewBuilds(JobModel job)
        {            
            var window = this.windowManager.FindWindow<BuildsExplorer.BuildsExplorerWindow>(true);
         
            var frame = (IVsWindowFrame)window.Frame;

            window.SelectJob(job);

            frame.Show();
        }

        private void OpenBuildDetails(JobModel job)
        {
            windowManager.OpenBuildDetails(job.ServerUrl, job.LastBuild);
        }
    }
}

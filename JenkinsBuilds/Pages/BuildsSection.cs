using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Commands;
using JenkinsBuilds.Jenkins;
using JenkinsBuilds.Model;
using JenkinsBuilds.Properties;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell.Interop;
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

            this.Title = "Favorite jobs";

            this.IsExpanded = true;
            this.IsVisible = true;

            this.monitors = new Dictionary<Uri, BackgroundJenkinsMonitor>();
        }

        private void RemoveFromFavorites(object obj)
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

        protected override Base.ViewModelBase CreateViewModel()
        {
            return new BuildsSectionViewModel
            {
                BuildNowCommand = new DelegateCommand(BuildNow),
                RemoveFromFavorites = new DelegateCommand(RemoveFromFavorites),
                OpenBuildDetailsCommand = new DelegateCommand(OpenBuildDetails),
                ViewBuildsCommand = new DelegateCommand(ViewBuilds)
            };
        }

        private void ViewBuilds(object obj)
        {
            var pkg = JenkinsBuildsPackage.Instance;

            var window = (BuildsExplorer.BuildsExplorerWindow)pkg.FindWindowPane(typeof(BuildsExplorer.BuildsExplorerWindow), 0, true);
            var frame = (IVsWindowFrame)window.Frame;

            window.SelectJob((JobModel)obj);

            frame.Show();
        }

        private void OpenBuildDetails(object obj)
        {
            var job = (JobModel)obj;

            JenkinsBuildsPackage.Instance.OpenBuildDetails(job.ServerUrl, job.LastBuild);
        }
    }
}

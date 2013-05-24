using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Commands;
using JenkinsBuilds.Jenkins;
using JenkinsBuilds.Model;
using JenkinsBuilds.Properties;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Niles.Model;

namespace JenkinsBuilds.BuildsExplorer
{
    public class BuildsExplorerWindow : ToolWindowPane
    {
        private BuildsExplorerViewModel viewModel;

        private JenkinsClient client;

        [Import]
        private IClientFactory clientFactory;

        [Import]
        private IWindowManager windowManager;

        private JobModel preselectedJob;

        public BuildsExplorerWindow()
        {
            this.viewModel = new BuildsExplorerViewModel
            {
                Instances = Settings.Default.Instances.ToList(),
                SearchBuildsCommand = new DelegateCommand(SearchBuilds),
                OpenBuildDetailsCommand = new DelegateCommand(OpenBuildDetails)
            };

            ((INotifyPropertyChanged)this.viewModel).PropertyChanged += SelectedInstanceChanged;

            this.Content = new BuildsExplorerView
            {
                ViewModel = this.viewModel
            };

            this.Caption = "Builds explorer";
        }

        private async void SelectedInstanceChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedInstance")
            {
                this.viewModel.SelectedJob = null;
                var jobs = await this.GetJobsFromInstance(this.viewModel.SelectedInstance.Url);

                jobs.Insert(0, new AllJobsModel());

                this.viewModel.Jobs = jobs;

                if (this.preselectedJob != null)
                {
                    this.viewModel.SelectedJob = jobs.SingleOrDefault(x => x.Url == this.preselectedJob.Url);

                    this.preselectedJob = null;
                }
            }
        }

        private async Task<List<JobModel>> GetJobsFromInstance(string instanceUrl)
        {
            var client = this.clientFactory.GetClient(this.viewModel.SelectedInstance.Url);

            var node = await client.GetResourceAsync<Node>(this.viewModel.SelectedInstance.Url, "jobs[displayName,url]");

            return node.Jobs.Select(x => new JobModel().LoadFrom(x)).ToList();
        }

        private void OpenBuildDetails(object obj)
        {            
            var selectedBuild = ((BuildModel)obj);            

            this.windowManager.OpenBuildDetails(selectedBuild.ServerUrl, selectedBuild);
        }

        private async void SearchBuilds(object obj)
        {
            this.client = this.clientFactory.GetClient(this.viewModel.SelectedInstance.Url);

            List<BuildModel> builds;

            if (this.viewModel.SelectedJob is AllJobsModel)
            {
                builds = await SearchBuildsInAllJobs();
            }
            else
            {
                builds = await SearchBuildsInJob(this.viewModel.SelectedJob);
            }

            if (this.viewModel.SelectedStatus.Status != null)
            {
                builds = builds.Where(x => x.Status == this.viewModel.SelectedStatus.Status).ToList();
            }

            this.viewModel.Builds = builds;
        }

        private async Task<List<BuildModel>> SearchBuildsInJob(JobModel jobModel)
        {
            var job = await this.client.GetResourceAsync<Job>(this.viewModel.SelectedJob.Url, JobModel.WithBuildsTree);

            return job.Builds.Select(x => new BuildModel().LoadFrom(x)).OrderByDescending(x => x.Timestamp).ToList();
        }

        private async Task<List<BuildModel>> SearchBuildsInAllJobs()
        {
            var tree = "jobs[" + JobModel.WithBuildsTree + "]";

            var node = await this.client.GetResourceAsync<Node>(new Uri(this.viewModel.SelectedInstance.Url), tree);

            var q = from j in node.Jobs
                    from b in j.Builds
                    orderby b.Timestamp descending
                    select new BuildModel().LoadFrom(b);

            return q.ToList();
        }

        public void SelectJob(JobModel jobModel)
        {
            this.viewModel.SelectedInstance = this.viewModel.Instances.SingleOrDefault(x => new Uri(x.Url) == jobModel.ServerUrl);
            this.preselectedJob = jobModel;
        }
    }
}

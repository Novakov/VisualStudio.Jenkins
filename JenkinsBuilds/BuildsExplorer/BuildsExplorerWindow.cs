using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public BuildsExplorerWindow()
        {
            this.viewModel = new BuildsExplorerViewModel
            {
                Instances = Settings.Default.Instances.ToList(),
                SearchBuildsCommand = new DelegateCommand(SearchBuilds),
                OpenBuildDetailsCommand = new DelegateCommand(OpenBuildDetails)
            };

            //((INotifyPropertyChanged)this.viewModel).PropertyChanged += SelectedInstanceChanged;

            this.Content = new BuildsExplorerView
            {
                ViewModel = this.viewModel
            };

            this.Caption = "Builds explorer";
        }

        private async void OpenBuildDetails(object obj)
        {
            var client = new JenkinsClient(new Uri(this.viewModel.SelectedInstance.Url));

            var selectedBuild = ((BuildModel)obj);

            var job = client.GetResourceAsync<Job>(selectedBuild.JobUrl, JobModel.FetchTree);
            var build = client.GetResourceAsync<Build>(selectedBuild.Url, ExtendedBuildModel.FetchTree);

            var jobModel = new JobModel().LoadFrom(await job);
            var buildModel = new ExtendedBuildModel().LoadFrom(await build);

            var detailsWindow = JenkinsBuildsPackage.Instance.FindWindow<BuildsDetails.BuildDetailsWindow>(create: true);

            var warnings = client.GetResourceIfAvailable<BuildWarnings>(buildModel.WarningsReportUrl, WarningsModel.FetchTree);

            detailsWindow.LoadFrom(jobModel, buildModel);

            if (warnings != null)
            {
                detailsWindow.LoadWarnings(new WarningsModel().LoadFrom(warnings));
            }

            ((IVsWindowFrame)detailsWindow.Frame).Show();
        }

        private async void SearchBuilds(object obj)
        {
            this.client = new JenkinsClient(new Uri(this.viewModel.SelectedInstance.Url));

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
    }
}

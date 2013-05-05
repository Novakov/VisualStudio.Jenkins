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

            var job = client.GetResourceAsync<Job>(this.viewModel.SelectedJob.Url, JobModel.FetchTree);
            var build = client.GetResourceAsync<Build>(((BuildModel)obj).Url, ExtendedBuildModel.FetchTree);

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

            var job = await this.client.GetResourceAsync<Job>(this.viewModel.SelectedJob.Url, JobModel.WithBuildsTree);

            this.viewModel.Builds = job.Builds.Select(x => new BuildModel().LoadFrom(x)).ToList();
        }

        //private void SelectedInstanceChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "SelectedInstance")
        //    {
        //        this.client = new JenkinsClient(new Uri(this.viewModel.SelectedInstance.Url));

        //        this.viewModel.Jobs = this.client.GetResource<Node>(new Uri(this.viewModel.SelectedInstance.Url), "jobs[" + JobModel.FetchTree + "]")
        //            .Jobs.Select(x => new JobModel().LoadFrom(x))
        //            .ToList();
        //    }
        //}        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace JenkinsBuilds.BuildsDetails
{    
    public class BuildDetailsWindow : ToolWindowPane
    {
        private BuildDetailsViewModel viewModel;

        public BuildDetailsWindow()
        {
            //this.viewModel = new BuildDetailsViewModel();
            this.viewModel = new DesignBuildDetailsViewModel();

            this.Content = new BuildDetailsView
            {
                DataContext = this.viewModel
            };
        }

        public void LoadFrom(Model.JobModel job, Model.ExtendedBuildModel build)
        {
            this.viewModel.Job = job;
            this.viewModel.Build = build;

            this.Caption = string.Format("{0} #{1}", job.DisplayName, build.Number);
        }
    }
}

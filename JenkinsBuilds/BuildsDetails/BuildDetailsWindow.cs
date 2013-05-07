﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using JenkinsBuilds.Model;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace JenkinsBuilds.BuildsDetails
{
    public class BuildDetailsWindow : ToolWindowPane
    {
        private BuildDetailsViewModel viewModel;

        public BuildDetailsWindow()
        {
            this.viewModel = new BuildDetailsViewModel
            {
                HasWarningsReport = false,
                HasTestResults = false
            };
            //this.viewModel = new DesignBuildDetailsViewModel();

            this.Content = new ScrollViewer
            {
                Content = new BuildDetailsView
                {
                    DataContext = this.viewModel
                }
            };             
        }

        protected override void OnClose()
        {
            base.OnClose();

            this.Dispose();
        }

        public BuildDetailsWindow LoadFrom(Model.JobModel job, Model.ExtendedBuildModel build)
        {
            this.viewModel.Job = job;
            this.viewModel.Build = build;

            this.Caption = string.Format("{0} #{1}", job.DisplayName, build.Number);

            return this;
        }

        public BuildDetailsWindow LoadWarnings(WarningsModel warnings)
        {
            this.viewModel.Warnings = warnings;

            this.viewModel.HasWarningsReport = true;

            return this;
        }

        public BuildDetailsWindow LoadTests(TestResultModel testModel)
        {
            this.viewModel.TestResults = testModel;

            this.viewModel.HasTestResults = true;

            return this;
        }
    }
}

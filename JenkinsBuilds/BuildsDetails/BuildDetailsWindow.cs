using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using JenkinsBuilds.Commands;
using JenkinsBuilds.Model;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using VisualStudio;

namespace JenkinsBuilds.BuildsDetails
{
    public class BuildDetailsWindow : ToolWindowPane
    {
        private BuildDetailsViewModel viewModel;

        [Import]
        private IArtifactDownloader downloader = null;

        [Import]
        private IWindowManager windowManager = null;
        
        [Import]
        private IStatusBar statusBar = null;

        public BuildDetailsWindow()
        {
            this.viewModel = new BuildDetailsViewModel
            {
                HasWarningsReport = false,
                HasTestResults = false,   
             
                ViewFileCommand = new DelegateCommand(ViewFile),
                SaveFileAsCommand = new DelegateCommand(SaveFileAs)
            };            

            this.Content = new ScrollViewer
            {
                Content = new BuildDetailsView
                {
                    DataContext = this.viewModel
                }
            };             
        }

        private async void SaveFileAs(object obj)
        {
            var path = (string)obj;

            var caption = string.Format("Downloading artifact {0}...", path);

            var dialog = new SaveFileDialog
            {
                FileName = Path.GetFileName(path),
                Filter = "All files|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                await this.downloader.Fetch(dialog.FileName, this.viewModel.Build, path, this.statusBar.ProgressReporter(caption));
            }
        }

        private async void ViewFile(object obj)
        {
            var path = (string)obj;                             

            var caption = string.Format("Downloading artifact {0}...", path);

            var destPath = this.downloader.GetTargetPath(this.viewModel.Job, this.viewModel.Build, path);

            await this.downloader.Fetch(destPath, this.viewModel.Build, path, this.statusBar.ProgressReporter(caption));

            this.windowManager.OpenFile(destPath);
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

        public BuildDetailsWindow LoadLazyBuildLog(Task<string> log)
        {
            this.viewModel.BuildLog = log;

            return this;
        }
    }
}

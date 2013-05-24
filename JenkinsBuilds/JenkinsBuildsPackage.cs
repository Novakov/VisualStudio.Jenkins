﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using JenkinsBuilds.Model;
using JenkinsBuilds.BuildsDetails;
using Niles.Model;
using JenkinsBuilds.Jenkins;
using Microsoft.VisualStudio.ComponentModelHost;
using System.ComponentModel.Composition;

namespace JenkinsBuilds
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidJenkinsBuildsPkgString)]
    [ProvideToolWindow(typeof(BuildsDetails.BuildDetailsWindow), Style = VsDockStyle.Tabbed, Orientation = ToolWindowOrientation.Right, Window = EnvDTE.Constants.vsWindowKindMainWindow, MultiInstances = true, DocumentLikeTool = true)]
    [ProvideToolWindow(typeof(BuildsExplorer.BuildsExplorerWindow), Style = VsDockStyle.Tabbed, Orientation = ToolWindowOrientation.Right, Window = EnvDTE.Constants.vsWindowKindMainWindow, DocumentLikeTool = true)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string)]
    public sealed class JenkinsBuildsPackage : Package
    {
        [Export]
        public static JenkinsBuildsPackage Instance { get; private set; }

        public EnvDTE.DTE DTE { get; private set; }
        public IVsStatusbar StatusBar { get; private set; }
        public IComponentModel ComponentModel { get; private set; }

        public JenkinsBuildsPackage()
        {
            JenkinsBuildsPackage.Instance = this;
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.DTE = this.GetService<EnvDTE.DTE, EnvDTE.DTE>();
            this.StatusBar = this.GetService<IVsStatusbar, SVsStatusbar>();
            this.ComponentModel = this.GetService<IComponentModel, SComponentModel>();
        }

        public TWindow FindWindow<TWindow>(bool create, int id = 0)
            where TWindow : ToolWindowPane
        {
            return (TWindow)this.FindToolWindow(typeof(TWindow), id, create);
        }

        public TWindow CreateWindow<TWindow>()
            where TWindow : ToolWindowPane
        {
            for (int i = 0; ; i++)
            {
                var window = this.FindWindowPane(typeof(TWindow), i, false);

                if (window == null)
                {
                    window = this.CreateToolWindow(typeof(TWindow), i);

                    this.ComponentModel.DefaultCompositionService.SatisfyImportsOnce(window);

                    return (TWindow)window;
                }
            }
        }

        public void OpenBuildDetails(Uri serverUrl, BuildModel selectedBuild)
        {
            var window = this.CreateWindow<BuildDetailsWindow>();
            var frame = (IVsWindowFrame)window.Frame;

            var client = new JenkinsClient(serverUrl);

            var build = client.GetResource<ExtendedBuild>(selectedBuild.Url, ExtendedBuildModel.FetchTree);

            var job = client.GetResource<Job>(selectedBuild.JobUrl, JobModel.FetchTree);

            var jobModel = new JobModel().LoadFrom(job);

            var buildModel = new ExtendedBuildModel().LoadFrom(build);

            window.LoadFrom(jobModel, buildModel);

            var warnings = client.GetResourceIfAvailable<BuildWarnings>(buildModel.WarningsReportUrl, WarningsModel.FetchTree);

            if (warnings != null)
            {
                var warningsModel = new WarningsModel().LoadFrom(warnings);

                window.LoadWarnings(warningsModel);
            }

            var testResult = client.GetResourceIfAvailable<TestResults>(buildModel.TestReportUrl, TestResultModel.FetchTree);

            if (testResult != null)
            {
                var testModel = new TestResultModel().LoadFrom(testResult);

                window.LoadTests(testModel);
            }

            window.LoadLazyBuildLog(client.GetRawDataAsync(buildModel.LogUrl));

            frame.Show();
        }

        public TService GetService<TService, TType>()
        {
            return (TService)this.GetService(typeof(TType));
        }

        public void OpenFile(string path)
        {
            this.DTE.ItemOperations.OpenFile(path);
        }        
    }
}

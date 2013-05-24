using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Jenkins.Model;
using JenkinsBuilds.BuildsDetails;
using JenkinsBuilds.Model;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Niles.Model;

namespace JenkinsBuilds
{
    [Export(typeof(IWindowManager))]
    public class WindowManager : IWindowManager
    {
        private readonly IClientFactory clientFactory;
        private ICompositionService composition;
        private DTE dte;

        [ImportingConstructor]
        public WindowManager(IClientFactory clientFactory, ICompositionService composition)
        {            
            this.clientFactory = clientFactory;
            this.composition = composition;
            this.dte = (DTE)Package.GetGlobalService(typeof(DTE));
        }

        public void OpenBuildDetails(Uri serverUrl, BuildModel selectedBuild)
        {
            var window = this.CreateWindow<BuildDetailsWindow>();
            var frame = (IVsWindowFrame)window.Frame;

            var client = this.clientFactory.GetClient(serverUrl);

            var build = client.GetResource<ExtendedBuild>(selectedBuild.Url, ExtendedBuildModel.FetchTree);

            var job = client.GetResource<Job>(selectedBuild.JobUrl, JobModel.FetchTree);

            var jobModel = new JobModel().LoadFrom(job);

            var buildModel = new ExtendedBuildModel().LoadFrom(build);

            window.LoadFrom(jobModel, buildModel);

            var warnings = client.GetResourceIfAvailable<BuildWarnings>(buildModel.WarningsReportUrl, WarningsModel.FetchTree);

            if (warnings != null)
            {                
                window.LoadWarnings(new WarningsModel().LoadFrom(warnings));
            }

            var testResult = client.GetResourceIfAvailable<TestResults>(buildModel.TestReportUrl, TestResultModel.FetchTree);

            if (testResult != null)
            {               
                window.LoadTests(new TestResultModel().LoadFrom(testResult));
            }

            window.LoadLazyBuildLog(client.GetRawDataAsync(buildModel.LogUrl));

            frame.Show();
        }

        public TWindow FindWindow<TWindow>(bool create, int id = 0)
            where TWindow : ToolWindowPane
        {
            var window = (TWindow)JenkinsBuildsPackage.Instance.FindToolWindow(typeof(TWindow), id, false);

            if (window == null && create)
            {
                window = (TWindow)JenkinsBuildsPackage.Instance.FindToolWindow(typeof(TWindow), id, true);

                this.composition.SatisfyImportsOnce(window);
            }

            return window;
        }

        public TWindow CreateWindow<TWindow>()
           where TWindow : ToolWindowPane
        {
            for (int i = 0; ; i++)
            {
                var window = JenkinsBuildsPackage.Instance.FindWindowPane(typeof(TWindow), i, false);

                if (window == null)
                {
                    window = JenkinsBuildsPackage.Instance.CreateToolWindow<TWindow>(i);

                    this.composition.SatisfyImportsOnce(window);

                    return (TWindow)window;
                }
            }
        }        

        public void OpenFile(string path)
        {
            this.dte.ItemOperations.OpenFile(path);
        } 
    }
}

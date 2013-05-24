using System;
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
        public EnvDTE.DTE DTE { get; private set; }
        public IVsStatusbar StatusBar { get; private set; }
        public IComponentModel ComponentModel { get; private set; }

        public static JenkinsBuildsPackage Instance { get; private set; }

        public JenkinsBuildsPackage()
        {
            Instance = this;
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.DTE = this.GetService<EnvDTE.DTE, EnvDTE.DTE>();
            this.StatusBar = this.GetService<IVsStatusbar, SVsStatusbar>();
            this.ComponentModel = this.GetService<IComponentModel, SComponentModel>();            
        }             

        public TService GetService<TService, TType>()
        {
            return (TService)this.GetService(typeof(TType));
        }

        public TWindow CreateToolWindow<TWindow>(int id)
            where TWindow : ToolWindowPane
        {
            return (TWindow)this.CreateToolWindow(typeof(TWindow), id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Commands;
using JenkinsBuilds.Properties;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerSection(InstancesSection.SectionId, BuildsPage.PageId, 20)]
    public class InstancesSection : Base.TeamExplorerSectionBase
    {
        public const string SectionId = "62A1A1D3-AAC9-401D-8627-621D4C013C8B";

        private InstancesView view;

        [ImportingConstructor]
        public InstancesSection([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.Title = "Instances";
            this.SectionContent = this.view = new InstancesView();
            this.IsExpanded = true;
            this.IsVisible = true;

            this.view.AddInstanceCommand = new DelegateCommand(OpenAddJenkinsPage);
            this.view.ViewJobsCommand = new DelegateCommand(OpenJobsPage);
        }

        private void OpenJobsPage(object obj)
        {
            this.GetService<ITeamExplorer>().NavigateToPage(new Guid(JobsPage.PageId), obj);
        }

        private void OpenAddJenkinsPage(object obj)
        {
            this.GetService<ITeamExplorer>().NavigateToPage(new Guid(AddJenkinsPage.PageId), null);
        }

        public override void Loaded(object sender, SectionLoadedEventArgs e)
        {
            this.Refresh();
        }

        public override void Refresh()
        {
            ((InstancesView)this.SectionContent).LoadInstances(Settings.Default.Instances);
        }
    }
}

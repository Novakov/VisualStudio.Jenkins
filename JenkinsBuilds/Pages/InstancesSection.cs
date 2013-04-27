using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Commands;
using JenkinsBuilds.Configuration;
using JenkinsBuilds.Properties;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerSection(InstancesSection.SectionId, BuildsPage.PageId, 20)]
    public class InstancesSection : Base.TeamExplorerSectionBase<InstancesSectionView>
    {
        public const string SectionId = "62A1A1D3-AAC9-401D-8627-621D4C013C8B";

        public new InstancesSectionViewModel ViewModel { get { return (InstancesSectionViewModel)base.ViewModel; } }

        public InstancesSection()           
        {
            this.Title = "Instances";
            
            this.IsExpanded = true;
            this.IsVisible = true;            
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
            this.ViewModel.Instances = new ObservableCollection<JenkinsInstance>(Settings.Default.Instances);
        }

        protected override InstancesSectionView CreateView()
        {
            return new InstancesSectionView();
        }

        protected override Base.ViewModelBase CreateViewModel()
        {
            return new InstancesSectionViewModel
            {
                AddInstanceCommand = new DelegateCommand(OpenAddJenkinsPage),
                ViewJobsCommand = new DelegateCommand(OpenJobsPage),
                RemoveInstanceCommand = new DelegateCommand(RemoveInstance)
            };
        }

        private void RemoveInstance(object obj)
        {
            var instance = (JenkinsInstance)obj;

            Settings.Default.Instances.Remove(instance);
            Settings.Default.Save();

            this.Refresh();
        }
    }
}

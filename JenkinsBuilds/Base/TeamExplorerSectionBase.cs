using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Controls;

namespace JenkinsBuilds.Base
{
    public abstract class TeamExplorerSectionBase : TeamExplorerBase, ITeamExplorerSection
    {
        private bool isBusy;
        private bool isExpanded;
        private bool isVisible;
        private object sectionContent;
        private string title;

        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.isBusy = value; this.RaisePropertyChanged(); }
        }

        public bool IsExpanded
        {
            get { return this.isExpanded; }
            set { this.isExpanded = value; this.RaisePropertyChanged(); }
        }

        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; this.RaisePropertyChanged(); }
        }

        public object SectionContent
        {
            get { return this.sectionContent; }
            set { this.sectionContent = value; this.RaisePropertyChanged(); }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; this.RaisePropertyChanged(); }
        }

        public TeamExplorerSectionBase()
        {
        }

        public TeamExplorerSectionBase(IServiceProvider serviceProvider)            
        {
            this.ServiceProvider = serviceProvider;
        }

        public virtual void Cancel()
        {            
        }

        public object GetExtensibilityService(Type serviceType)
        {
            return null;
        }

        public virtual void Initialize(object sender, SectionInitializeEventArgs e)
        {            
        }

       

        public virtual void Loaded(object sender, SectionLoadedEventArgs e)
        {         
        }

        public virtual void Refresh()
        {         
        }

        public virtual void SaveContext(object sender, SectionSaveContextEventArgs e)
        {         
        }

        
    }
}

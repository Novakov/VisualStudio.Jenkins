using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.TeamFoundation.Controls;

namespace JenkinsBuilds.Base
{
    public abstract class TeamExplorerSectionBase<TView> : TeamExplorerViewableBase<TView>, ITeamExplorerSection
        where TView : UserControl
    {        
        private bool isExpanded;
        private bool isVisible;        
  
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
            get { return this.View; }
        }
     
        public TeamExplorerSectionBase()
        {
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
            if (this.ServiceProvider == null)
            {
                this.ServiceProvider = e.ServiceProvider;
            }

            base.InitializeBase();
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

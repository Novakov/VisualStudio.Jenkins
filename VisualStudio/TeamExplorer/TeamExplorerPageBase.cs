using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.TeamFoundation.Controls;

namespace VisualStudio.TeamExplorer
{
    public abstract class TeamExplorerPageBase<TView> : TeamExplorerViewableBase<TView>, ITeamExplorerPage
        where TView : UserControl
    {                       
        public object PageContent
        {
            get { return this.View; }            
        }

        public TeamExplorerPageBase()
        {
        }        

        public virtual void Cancel()
        {
            
        }

        public virtual object GetExtensibilityService(Type serviceType)
        {
            return null;
        }

        public virtual void Initialize(object sender, PageInitializeEventArgs e)
        {
            if (this.ServiceProvider == null)
            {
                this.ServiceProvider = e.ServiceProvider;
            }

            base.InitializeBase();
        }
       
        public virtual void Loaded(object sender, PageLoadedEventArgs e)
        {            
        }       

        public virtual void Refresh()
        {            
        }

        public virtual void SaveContext(object sender, PageSaveContextEventArgs e)
        {            
        }        
    }
}

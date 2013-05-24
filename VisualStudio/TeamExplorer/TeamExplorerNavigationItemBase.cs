using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Controls;

namespace VisualStudio.TeamExplorer
{
    public abstract class TeamExplorerNavigationItemBase : TeamExplorerBase, ITeamExplorerNavigationItem
    {
        private Image image;
        private string text;

        public System.Drawing.Image Image
        {
            get { return this.image; }
            set { this.image = value; this.RaisePropertyChanged(); }
        }

        public string Text
        {
            get { return this.text; }
            set { this.text = value; this.RaisePropertyChanged(); }
        }

        public bool IsVisible
        {
            get { return true; }
        }    

        public TeamExplorerNavigationItemBase(IServiceProvider serviceProvider)
        {            
            this.ServiceProvider = serviceProvider;
        }

        public virtual void Execute()
        {
            
        }       

        public virtual void Invalidate()
        {
            
        }          
    }
}

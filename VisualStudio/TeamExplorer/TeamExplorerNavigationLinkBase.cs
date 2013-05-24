using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Controls;

namespace VisualStudio.TeamExplorer
{
    public abstract class TeamExplorerNavigationLinkBase : TeamExplorerBase, ITeamExplorerNavigationLink
    {
        private bool isEnabled;
        private bool isVisible;
        private string text;

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.isEnabled = value; this.RaisePropertyChanged(); }
        }

        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; this.RaisePropertyChanged(); }
        }

        public string Text
        {
            get { return this.text; }
            set { this.text = value; this.RaisePropertyChanged(); }
        }

        protected TeamExplorerNavigationLinkBase(IServiceProvider serviceProvider)
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

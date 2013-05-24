using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell.Interop;
using VisualStudio;
using VisualStudio.TeamExplorer;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerNavigationItem(BuildExplorerItem.ItemId, 201)]
    public class BuildExplorerItem : TeamExplorerNavigationItemBase
    {
        public const string ItemId = "{40E276FB-5521-4ACD-95AD-4098FD40642D}";

        private IWindowManager windowManager;

        [ImportingConstructor]
        public BuildExplorerItem([ImportServiceProvider]IServiceProvider serviceProvider, IWindowManager windowManager)
            : base(serviceProvider)
        {
            this.Text = "Build explorer";
            this.windowManager = windowManager;
        }

        public override void Execute()
        {            
            var window = this.windowManager.FindWindow<BuildsExplorer.BuildsExplorerWindow>(create:true);
            var frame = (IVsWindowFrame)window.Frame;

            frame.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell.Interop;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerNavigationItem(BuildExplorerItem.ItemId, 201)]
    public class BuildExplorerItem : Base.TeamExplorerNavigationItemBase
    {
        public const string ItemId = "{40E276FB-5521-4ACD-95AD-4098FD40642D}";

        [ImportingConstructor]
        public BuildExplorerItem([ImportServiceProvider]IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.Text = "Build explorer";            
        }

        public override void Execute()
        {
            var pkg = JenkinsBuildsPackage.Instance;
            var window = pkg.FindWindow<BuildsExplorer.BuildsExplorerWindow>(create:true);
            var frame = (IVsWindowFrame)window.Frame;

            frame.Show();
        }
    }
}

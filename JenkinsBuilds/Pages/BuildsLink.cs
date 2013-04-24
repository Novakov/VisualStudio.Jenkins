using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerNavigationItem(BuildsLink.LinkId, 200, TargetPageId = BuildsPage.PageId)]
    public class BuildsLink : Base.TeamExplorerNavigationItemBase
    {
        public const string LinkId = "E16E7060-3E3F-49B0-A29A-B907ABFA5E94";

        [ImportingConstructor]
        public BuildsLink([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.Text = "Jenkins";                      
        }

        public override void Execute()
        {
            var explorer = this.GetService<ITeamExplorer>();

            if (explorer != null)
            {
                explorer.NavigateToPage(new Guid(BuildsPage.PageId), null);
            }
        }        
    }
}

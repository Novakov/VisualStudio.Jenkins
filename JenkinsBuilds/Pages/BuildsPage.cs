using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Controls;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerPage(BuildsPage.PageId)]
    public class BuildsPage : Base.TeamExplorerPageBase
    {
        public const string PageId = "{363821C0-E453-464A-8E41-65B07807AB2C}";

        public BuildsPage()
        {
            this.Title = "Jenkins";
            this.PageContent = new BuildsPageView();
        }
    }
}

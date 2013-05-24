using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.TeamFoundation.Controls;
using Niles.Client;
using Niles.Model;
using VisualStudio.TeamExplorer;
//using Niles.Client;
//using Niles.Model;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerPage(BuildsPage.PageId)]
    public class BuildsPage : TeamExplorerPageBase<UserControl>
    {
        public const string PageId = "363821C0-E453-464A-8E41-65B07807AB2C";

        public BuildsPage()
        {
            this.Title = "Jenkins";            
        }

        public override void Loaded(object sender, PageLoadedEventArgs e)
        {
            this.Refresh();            
        }     

        protected override UserControl CreateView()
        {
            return null;
        }
    }
}

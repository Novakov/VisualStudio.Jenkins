using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Controls;
using Niles.Client;
using Niles.Model;
//using Niles.Client;
//using Niles.Model;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerPage(BuildsPage.PageId)]
    public class BuildsPage : Base.TeamExplorerPageBase
    {
        public const string PageId = "363821C0-E453-464A-8E41-65B07807AB2C";

        public BuildsPage()
        {
            this.Title = "Jenkins";
            //this.PageContent = new BuildsPageView();

            
        }

        public override void Loaded(object sender, PageLoadedEventArgs e)
        {
            this.Refresh();            
        }

        public override void Refresh()
        {
            //var jenkins = new JsonJenkinsClient();
            //var node = jenkins.GetResource<Node>(new Uri("http://localhost/jenkins/"), "jobs[name,color,displayName,url,lastBuild[url,number,building,result,timestamp]]");

            //((BuildsPageView)this.PageContent).LoadJobs(node.Jobs);
        }
    }
}

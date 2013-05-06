using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Model;

namespace JenkinsBuilds.BuildsExplorer
{
    public class AllJobsModel : JobModel
    {
        public AllJobsModel()
        {
            this.DisplayName = "(All jobs)";
        }
    }
}

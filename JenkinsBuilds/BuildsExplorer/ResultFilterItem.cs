using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Model;

namespace JenkinsBuilds.BuildsExplorer
{
    public class ResultFilterItem
    {
        public BuildStatus? Status { get; set; }

        public string DisplayName { get; set; }
    }
}

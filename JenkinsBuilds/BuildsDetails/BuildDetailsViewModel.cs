using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Model;

namespace JenkinsBuilds.BuildsDetails
{
    [PropertyChanged.ImplementPropertyChanged]
    public class BuildDetailsViewModel
    {
        public BuildModel Build { get; set; }

        public JobModel Job { get; set; }
    }
}

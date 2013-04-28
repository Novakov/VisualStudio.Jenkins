using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class ExtendedBuildModel : BuildModel
    {
        public string FullDisplayName { get; set; }

        public TimeSpan Duration { get; set; }

        public WarningsModel Warnings { get; set; }
    }
}

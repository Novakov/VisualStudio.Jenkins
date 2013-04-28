using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Niles.Model;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class ExtendedBuildModel : BuildModel
    {
        public new const string FetchTree = BuildModel.FetchTree + ",fullDisplayName,duration";

        public string FullDisplayName { get; set; }

        public TimeSpan Duration { get; set; }

        public new ExtendedBuildModel LoadFrom(Build build)
        {
            base.LoadFrom(build);

            this.FullDisplayName = build.FullDisplayName;
            this.Duration = TimeSpan.FromMilliseconds(build.Duration);

            return this;
        }
    }
}

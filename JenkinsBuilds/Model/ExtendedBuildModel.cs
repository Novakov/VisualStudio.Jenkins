using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Jenkins;
using Niles.Model;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class ExtendedBuildModel : BuildModel
    {
        public new const string FetchTree = BuildModel.FetchTree + ",duration,actions[causes[shortDescription]]";        

        public TimeSpan Duration { get; set; }

        public string Cause { get; set; }

        public new ExtendedBuildModel LoadFrom(ExtendedBuild build)
        {
            base.LoadFrom(build);

            this.FullDisplayName = build.FullDisplayName;
            this.Duration = TimeSpan.FromMilliseconds(build.Duration);

            this.Cause = build.Actions.GetAction<BuildCause[]>("causes").Select(x => x.ShortDescription).FirstOrDefault();

            return this;
        }
    }
}

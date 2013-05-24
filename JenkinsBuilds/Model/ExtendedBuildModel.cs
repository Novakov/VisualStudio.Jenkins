using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons.Paths;
using Jenkins.Model;
using Niles.Model;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class ExtendedBuildModel : BuildModel
    {
        public new const string FetchTree = BuildModel.FetchTree + ",artifacts[fileName,relativePath],duration,actions[causes[shortDescription]]," + ChangeSetModel.FetchTree;        

        public TimeSpan Duration { get; set; }

        public string Cause { get; set; }

        public ChangeSetModel[] ChangeSets { get; set; }

        public Item[] Artifacts { get; set; }

        public ExtendedBuildModel LoadFrom(ExtendedBuild build)
        {
            base.LoadFrom(build);

            this.FullDisplayName = build.FullDisplayName;
            this.Duration = TimeSpan.FromMilliseconds(build.Duration);

            this.Cause = build.Actions.GetAction<BuildCause[]>("causes").Select(x => x.ShortDescription).FirstOrDefault();

            if (build.ChangeSet != null)
            {
                this.ChangeSets = build.ChangeSet.Items.Select(x => new ChangeSetModel().LoadFrom(x)).ToArray();
            }
            else
            {
                this.ChangeSets = new ChangeSetModel[0];
            }

            var loader = new PathTreeLoader();

            this.Artifacts = loader.Load(build.Artifacts.Select(x => x.RelativePath))
                                .ConvertToLeaves()
                                .JoinPassthroughNodes()
                                .ToArray()
                                .FixUpParents()
                                .ToArray();

            return this;
        }        
    }
}

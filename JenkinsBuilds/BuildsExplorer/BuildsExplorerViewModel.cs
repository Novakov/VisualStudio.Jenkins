using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JenkinsBuilds.Model;

namespace JenkinsBuilds.BuildsExplorer
{
    [PropertyChanged.ImplementPropertyChanged]
    public class BuildsExplorerViewModel
    {
        public List<Configuration.JenkinsInstance> Instances { get; set; }

        public Configuration.JenkinsInstance SelectedInstance { get; set; }

        public List<Model.JobModel> Jobs { get; set; }

        public JobModel SelectedJob { get; set; }

        public ICommand SearchBuildsCommand { get; set; }

        public List<BuildModel> Builds { get; set; }

        public ICommand OpenBuildDetailsCommand { get; set; }

        public ResultFilterItem SelectedStatus { get; set; }
    }
}

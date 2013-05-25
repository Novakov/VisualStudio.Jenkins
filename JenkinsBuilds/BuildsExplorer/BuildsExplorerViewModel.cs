using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Commons.Wpf;
using FluentValidation;
using JenkinsBuilds.Model;

namespace JenkinsBuilds.BuildsExplorer
{
    [PropertyChanged.ImplementPropertyChanged]
    [InjectValidation]
    public class BuildsExplorerViewModel : ViewModelBase
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

    public class BuildsExplorerViewModelValidator : AbstractValidator<BuildsExplorerViewModel>
    {
        public BuildsExplorerViewModelValidator()
        {
            this.RuleFor(x => x.SelectedInstance).NotNull();
        }
    }
}

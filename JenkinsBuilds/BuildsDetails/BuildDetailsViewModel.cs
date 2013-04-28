using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JenkinsBuilds.Model;

namespace JenkinsBuilds.BuildsDetails
{
    [PropertyChanged.ImplementPropertyChanged]
    public class BuildDetailsViewModel
    {
        public ExtendedBuildModel Build { get; set; }

        public JobModel Job { get; set; }

        public ICommand OpenBuildPageCommand { get; set; }

        public ICommand RebuildCommand { get; set; }

        public ICommand OpenConsoleLogCommand { get; set; }

        public ICommand OpenWarningCommand { get; set; }
    }
}

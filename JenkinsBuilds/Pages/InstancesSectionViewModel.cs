using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JenkinsBuilds.Pages
{
    [PropertyChanged.ImplementPropertyChanged]
    public class InstancesSectionViewModel : Base.ViewModelBase
    {
        public ICommand AddInstanceCommand { get; set; }
        public IEnumerable<Configuration.JenkinsInstance> Instances { get; set; }
        public ICommand ViewJobsCommand { get; set; }
    }
}

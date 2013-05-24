using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VisualStudio.TeamExplorer;

namespace JenkinsBuilds.Pages
{
    [PropertyChanged.ImplementPropertyChanged]
    public class InstancesSectionViewModel : ViewModelBase
    {
        public ICommand AddInstanceCommand { get; set; }
        public ObservableCollection<Configuration.JenkinsInstance> Instances { get; set; }
        public ICommand ViewJobsCommand { get; set; }
        public ICommand RemoveInstanceCommand { get; set; }
    }
}

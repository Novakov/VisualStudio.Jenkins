using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JenkinsBuilds.Model;

namespace JenkinsBuilds.Pages
{
    [PropertyChanged.ImplementPropertyChanged]
    public class BuildsSectionViewModel : Base.ViewModelBase
    {
        public ObservableCollection<JobModel> Jobs { get; set; }

        public ICommand RemoveFromFavorites { get; set; }

        public ICommand BuildNowCommand { get; set; }

        public ICommand OpenBuildDetailsCommand { get; set; }
    }
}

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
    public class JobsPageViewModel : Base.ViewModelBase
    {
        public ObservableCollection<JobModel> Jobs { get; set; }

        public ICommand AddToFavoritesCommand { get; set; }

        public ICommand RemoveFromFavoritesCommand { get; set; }
    }
}

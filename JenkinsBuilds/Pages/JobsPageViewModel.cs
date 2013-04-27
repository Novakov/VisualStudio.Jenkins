using System;
using System.Collections.Generic;
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
        public IEnumerable<JobModel> Jobs { get; set; }
        public ICommand AddToFavouritesCommand { get; set; }
    }
}

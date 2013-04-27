using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JenkinsBuilds.Pages
{
    public class AddJenkinsPageViewModel : Base.ViewModelBase
    {
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}

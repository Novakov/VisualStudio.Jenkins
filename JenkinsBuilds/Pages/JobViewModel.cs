using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenkinsBuilds.Pages
{
    public class JobViewModel : Base.NotifyPropertyChangeBase
    {
        public string DisplayName { get; set; }

        public Uri JobUrl { get; set; }

        public string LastStatus { get; set; }

        public DateTime LastBuildTimestamp { get; set; }

        private bool isFavourite;

        public bool IsFavourite
        {
            get { return isFavourite; }
            set { isFavourite = value; this.RaisePropertyChanged(); }
        }
        
    }
}

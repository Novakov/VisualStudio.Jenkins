using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Niles.Model;

namespace JenkinsBuilds.Pages
{
    public class JobViewModel : Base.NotifyPropertyChangeBase
    {
        public string DisplayName { get; set; }

        public Uri JobUrl { get; set; }

        public string JobStatus { get; set; }

        public DateTime? LastBuildTimestamp { get; set; }

        private bool isFavourite;

        public bool IsFavourite
        {
            get { return isFavourite; }
            set { isFavourite = value; this.RaisePropertyChanged(); }
        }

        public JobViewModel LoadFrom(Job job)
        {
            this.DisplayName = job.DisplayName;
            this.JobUrl = job.Url;

            if (job.LastBuild !=null)
            {
                this.LastBuildTimestamp = job.LastBuild.Timestamp;
                this.JobStatus = job.LastBuild.Result;
            }
            else
            {
                this.LastBuildTimestamp = null;
                this.JobStatus = null;
            }            

            return this;
        }

        public JobViewModel MarkFavourite(bool isFavourite)
        {
            this.IsFavourite = isFavourite;
            return this;
        }
    }
}

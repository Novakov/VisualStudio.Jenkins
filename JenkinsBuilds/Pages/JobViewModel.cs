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
        public const string FetchTree = "name,color,displayName,url,lastBuild[url,number,building,result,timestamp]";

        public string Name { get; set; }

        private string displayName;
        public string DisplayName
        {
            get { return this.displayName; }
            set { this.displayName = value; this.RaisePropertyChanged(); }
        }

        public Uri JobUrl { get; set; }

        private string jobStatus;
        public string JobStatus
        {
            get { return this.jobStatus; }
            set { this.jobStatus = value; this.RaisePropertyChanged(); }
        }

        private DateTime? lastBuildTimestamp;
        public DateTime? LastBuildTimestamp
        {
            get { return this.lastBuildTimestamp; }
            set { this.lastBuildTimestamp = value; this.RaisePropertyChanged(); }
        }

        public Uri ServerUrl
        {
            get
            {
                return this.JobUrl.AppendPath("../../");
            }
        }

        private bool isFavourite;

        public bool IsFavourite
        {
            get { return isFavourite; }
            set { isFavourite = value; this.RaisePropertyChanged(); }
        }

        public JobViewModel LoadFrom(Job job)
        {
            this.Name = job.Name;
            this.DisplayName = job.DisplayName;
            this.JobUrl = job.Url;

            LoadFrom(job.LastBuild);

            return this;
        }

        public void LoadFrom(Build build)
        {
            if (build != null)
            {
                this.LastBuildTimestamp = build.Timestamp;
                this.JobStatus = build.Result;

                if (build.Building)
                {
                    this.JobStatus = "Building";
                }
            }
            else
            {
                this.LastBuildTimestamp = null;
                this.JobStatus = null;
            }
        }

        public JobViewModel MarkFavourite(bool isFavourite)
        {
            this.IsFavourite = isFavourite;
            return this;
        }
    }
}

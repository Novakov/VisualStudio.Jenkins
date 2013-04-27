using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Niles.Model;

namespace JenkinsBuilds.Pages
{
    [PropertyChanged.ImplementPropertyChanged]
    public class JobViewModel : Base.NotifyPropertyChangeBase
    {
        public const string FetchTree = "name,color,displayName,url,lastBuild[url,number,building,result,timestamp]";

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Uri JobUrl { get; set; }

        public string JobStatus { get; set; }

        public DateTime? LastBuildTimestamp { get; set; }

        public Uri ServerUrl
        {
            get
            {
                return this.JobUrl.AppendPath("../../");
            }
        }
        
        public bool IsFavourite { get; set; }

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

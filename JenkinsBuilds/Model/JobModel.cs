using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JenkinsBuilds.Properties;
using Niles.Model;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class JobModel
    {
        public const string FetchTree = "name,displayName,url,lastBuild[" + BuildModel.FetchTree + "]";

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Uri Url { get; set; }

        public BuildModel LastBuild { get; set; }

        public bool IsFavorite { get; set; }

        public Uri ServerUrl
        {
            get
            {
                return this.Url.AppendPath("../../");
            }
        }        

        public JobModel LoadFrom(Job job)
        {
            this.Name = job.Name;
            this.DisplayName = job.DisplayName;
            this.Url = job.Url;

            this.IsFavorite = Settings.Default.IsFavorite(job.Url);

            this.LastBuild = LoadBuild(job.LastBuild);

            return this;
        }

        private static BuildModel LoadBuild(Build b2)
        {            
            if (b2 == null)
            {
                return new NullBuildModel();
            }
            else
            {
                return new BuildModel().LoadFrom(b2);
            }
        }
    }
}

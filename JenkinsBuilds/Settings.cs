using System.ComponentModel.Composition;
using JenkinsBuilds.Configuration;
using System.Linq;
using System;

namespace JenkinsBuilds.Properties
{    
    internal sealed partial class Settings
    {
        public JenkinsInstance this[Uri serverUrl]
        {
            get { return this.Instances.SingleOrDefault(x => new Uri(x.Url) == serverUrl); }
        }

        protected override void OnSettingsLoaded(object sender, System.Configuration.SettingsLoadedEventArgs e)
        {
            base.OnSettingsLoaded(sender, e);           

            if (this.Instances == null)
            {
                this.Instances = new Configuration.JenkinsInstances();
            }
        }

        public void AddJob(Uri serverUrl, Uri jobUri)
        {
            var server = this[serverUrl];

            server.FavouriteJobs.Add(jobUri.ToString());
        }

        public bool IsFavourite(Uri jobUrl)
        {
            var q = from i in this.Instances
                    from j in i.FavouriteJobs
                    where new Uri(j) == jobUrl
                    select j;

            return q.Any();
        }

        public JenkinsInstance GetInstanceFromJob(Uri job)
        {
            var q = from i in this.Instances
                    from j in i.FavouriteJobs
                    where new Uri(j) == job
                    select i;

            return q.SingleOrDefault();
        }

        public void RemoveJob(Uri job)
        {
            var server = this.GetInstanceFromJob(job);

            server.FavouriteJobs.Remove(job.ToString());
        }
    }
}

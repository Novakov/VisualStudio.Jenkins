using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Niles.Client;
using Niles.Model;

namespace JenkinsBuilds
{
    public class JenkinsMonitor
    {
        private const string FetchJobsTree = "jobs[name,url,lastBuild[number,result,building]]";

        private Uri url;

        private IDictionary<string, Build> lastBuilds;
        private JsonJenkinsClient client;

        public string JobFetchTree { get; set; }

        public event EventHandler<BuildChangedEventArgs> BuildChanged;

        public JenkinsMonitor(Uri url)
        {
            this.url = url;
            this.client = new JsonJenkinsClient();

            this.lastBuilds = new Dictionary<string, Build>();
        }

        public void Poll()
        {
            var jobs = this.client.GetResource<Node>(this.url, FetchJobsTree).Jobs;

            var changedBuilds = jobs.Where(x => IsChanged(x)).ToArray();

            foreach (var item in changedBuilds)
            {
                var changeType = GetChangeType(item);

                OnBuildChanged(item, changeType);
            }

            foreach (var item in jobs)
            {
                UpdateStatus(item);
            }
        }

        private ChangeType GetChangeType(Job job)
        {
            if (job.LastBuild.Building)
            {
                return ChangeType.Started;
            }
            else
            {
                return ChangeType.Finished;
            }
        }

        private void UpdateStatus(Job job)
        {
            this.lastBuilds[job.Name] = job.LastBuild;
        }

        private bool IsChanged(Job job)
        {
            Build lastBuild;

            if (!this.lastBuilds.TryGetValue(job.Name, out lastBuild))
            {
                return false;
            }

            if (lastBuild == null && job.LastBuild == null)
            {
                return false;
            }

            return false
                || (job.LastBuild != null && lastBuild == null)
                || job.LastBuild.Number != lastBuild.Number
                || job.LastBuild.Building != lastBuild.Building
                ;
        }

        private void OnBuildChanged(Job job, ChangeType changeType)
        {
            var expandedJob = this.client.Expand(job, this.JobFetchTree);

            if (this.BuildChanged != null)
            {
                this.BuildChanged(this, new BuildChangedEventArgs(expandedJob, changeType));
            }
        }
    }
}

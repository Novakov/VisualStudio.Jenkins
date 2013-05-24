using System;
using System.Threading.Tasks;
using Commons;
using JenkinsBuilds.Model;
namespace JenkinsBuilds
{
    public interface IArtifactDownloader
    {
        Task Fetch(string targetPath, BuildModel build, string path, IProgress<ProgressReport> progress);
        string GetTargetPath(JenkinsBuilds.Model.JobModel job, JenkinsBuilds.Model.BuildModel build, string path);
    }
}

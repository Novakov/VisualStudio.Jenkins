using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons;
using JenkinsBuilds.Model;

namespace JenkinsBuilds
{    
    [Export(typeof(IArtifactDownloader))]
    public class ArtifactDownloader : IArtifactDownloader
    {
        private readonly IClientFactory clientFactory;

        [ImportingConstructor]
        public ArtifactDownloader(IClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public string GetTargetPath(JobModel job, BuildModel build, string path)
        {
            return Path.Combine(Path.GetTempPath(), "VS_JenkinsBuilds", job.Name, build.Number.Value.ToString(), path);
        }

        public async Task Fetch(string targetPath, BuildModel build, string path, IProgress<ProgressReport> progress)
        {
            var client = this.clientFactory.GetClient(build.ServerUrl);

            if (!File.Exists(targetPath))
            {
                var dirName = Path.GetDirectoryName(targetPath);

                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }

                var url = build.GetArtifactUrl(path); 

                using (var dest = File.Create(targetPath))
                {
                    await client.DownloadFileAsync(dest, url, progress);
                }
            }
        }
    }
}

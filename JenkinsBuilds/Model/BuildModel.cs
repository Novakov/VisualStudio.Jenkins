using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Niles.Model;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class BuildModel
    {
        public const string FetchTree = "fullDisplayName,number,url,timestamp,result,building";

        public Uri Url { get; set; }

        public int? Number { get; set; }

        public BuildStatus Status { get; set; }

        public DateTime? Timestamp { get; set; }

        public string FullDisplayName { get; set; }

        public Uri WarningsReportUrl
        {
            get 
            {
                return this.Url.AppendPath("warnings32Result/");
            }
        }

        public Uri TestReportUrl
        {
            get
            {
                return this.Url.AppendPath("testReport/");
            }
        }

        public Uri JobUrl
        {
            get
            {
                return this.Url.AppendPath("..");
            }
        }

        public Uri ServerUrl
        {
            get
            {
                return this.Url.AppendPath("../..");
            }
        }

        public BuildModel LoadFrom(Build build)
        {            
            this.Timestamp = build.Timestamp;
            this.Status = DecodeBuildStatus(build);
            this.Number = build.Number;
            this.Url = build.Url;
            this.FullDisplayName = build.FullDisplayName;            

            return this;
        }

        private static BuildStatus DecodeBuildStatus(Build build)
        {
            if (build.Building)
            {
                return BuildStatus.Building;
            }

            switch ((build.Result ?? "").ToLower())
            {
                case "success":
                    return BuildStatus.Success;
                case "aborted":
                    return BuildStatus.Aborted;
                case "failure":
                    return BuildStatus.Failure;
                case "unstable":
                    return BuildStatus.Unstable;
                default:
                    return BuildStatus.None;
            }
        }
    }
}

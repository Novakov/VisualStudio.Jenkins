using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Niles.Model;

namespace JenkinsBuilds.Jenkins
{
    public class BuildWarnings : IResource
    {
        public Uri Url { get; set; }

        public int NumberOfFixedWarnings { get; set; }

        public int NumberOfNewWarnings { get; set; }

        public int NumberOfWarnings { get; set; }

        public int WarningsDelta { get; set; }

        public IList<Warning> Warnings { get; set; }

        public BuildWarnings()
        {
            this.Warnings = new List<Warning>();
        }
    }
}

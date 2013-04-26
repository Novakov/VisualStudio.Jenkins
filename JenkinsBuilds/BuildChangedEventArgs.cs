using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Niles.Model;

namespace JenkinsBuilds
{
    public class BuildChangedEventArgs : EventArgs
    {
        public Job Job { get; set; }

        public ChangeType ChangeType { get; set; }

        public BuildChangedEventArgs(Job job, ChangeType changeType)
        {
            this.Job = job;
            this.ChangeType = changeType;
        }
    }
}

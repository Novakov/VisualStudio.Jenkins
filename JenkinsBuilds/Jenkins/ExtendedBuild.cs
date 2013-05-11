using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Niles.Model;

namespace JenkinsBuilds.Jenkins
{
    public class ExtendedBuild : Build
    {
        public BuildActions Actions { get; set; }
    }
}

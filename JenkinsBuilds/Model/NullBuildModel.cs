using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenkinsBuilds.Model
{    
    public class NullBuildModel : BuildModel
    {
        public NullBuildModel()
        {
            this.Number = null;
            this.Status = BuildStatus.None;
            this.Timestamp = null;
            this.Url = null;            
        }
    }
}

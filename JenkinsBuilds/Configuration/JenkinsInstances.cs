using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenkinsBuilds.Configuration
{
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public class JenkinsInstances : List<JenkinsInstance>
    {        
    }
}

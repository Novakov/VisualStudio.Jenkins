using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class WarningsModel
    {
        public int WarningsDelta { get; set; }

        public int NumberOfWarnings { get; set; }

        public int NumberOfFixedWarnings { get; set; }

        public int NumberOfNewWarnings { get; set; }

        public List<WarningModel> Warnings { get; set; }
    }
}

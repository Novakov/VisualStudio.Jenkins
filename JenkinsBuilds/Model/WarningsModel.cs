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
        public const string FetchTree = "warningsDelta,numberOfWarnings,numberOfNewWarnings,numberOfFixedWarnings,warnings[" + WarningModel.FetchTree + "]";

        public int Delta { get; set; }

        public int Count { get; set; }

        public int FixedCount { get; set; }

        public int NewCount { get; set; }

        public List<WarningModel> Warnings { get; set; }
    }
}

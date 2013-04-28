using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class WarningModel
    {
        public string FileName { get; set; }

        public int LineNumber { get; set; }

        public int Key { get; set; }

        public string Message { get; set; }

        public string Priority { get; set; }
    }
}

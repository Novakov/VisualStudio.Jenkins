using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jenkins.Model;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class WarningModel
    {
        public const string FetchTree = "fileName,key,message,primaryLineNumber,priority";

        public string FileName { get; set; }

        public int LineNumber { get; set; }

        public int Key { get; set; }

        public string Message { get; set; }

        public string Priority { get; set; }

        public WarningModel LoadFrom(Warning warning)
        {
            this.FileName = warning.FileName;
            this.LineNumber = warning.PrimaryLineNumber;
            this.Key = warning.Key;
            this.Message = warning.Message;
            this.Priority = warning.Priority;

            return this;
        }
    }
}

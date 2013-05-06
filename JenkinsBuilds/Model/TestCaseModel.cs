using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JenkinsBuilds.Jenkins;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class TestCaseModel
    {
        public const string FetchTree = "status,skippedMessage,skipped,name,errorStackTrace,errorDetails,className,duration";

        public string Status { get; set; }

        public string SkippedMessage { get; set; }

        public bool Skipped { get; set; }

        public string Name { get; set; }

        public string ErrorStackTrace { get; set; }

        public string ErrorDetails { get; set; }

        public string ClassName { get; set; }

        public TimeSpan Duration { get; set; }

        public TestCaseModel LoadFrom(TestCase testCase)
        {
            this.Duration = TimeSpan.FromSeconds(testCase.Duration);
            this.ClassName = testCase.ClassName;
            this.ErrorDetails = testCase.ErrorDetails;
            this.ErrorStackTrace = testCase.ErrorStackTrace;
            this.Name = testCase.Name;
            this.Skipped = testCase.Skipped;
            this.SkippedMessage = testCase.SkippedMessage;
            this.Status = testCase.Status;

            return this;
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JenkinsBuilds.Jenkins;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    [DebuggerDisplay("Suite = {Name}")]
    public class TestSuiteModel
    {
        public const string FetchTree = "duration,name,cases[" + TestCaseModel.FetchTree + "]";

        public TimeSpan Duration { get; set; }

        public string Name { get; set; }

        public List<TestCaseModel> Cases { get; set; }

        public int PassCount
        {
            get { return this.Cases.Count(x => x.Status == TestCaseStatus.Passed); }
        }

        public int FailCount
        {
            get { return this.Cases.Count(x => x.Status == TestCaseStatus.Failed); }
        }

        public int SkipCount
        {
            get { return this.Cases.Count(x => x.Status == TestCaseStatus.Skipped); }
        }

        public TestSuiteModel LoadFrom(Suite suite)
        {
            this.Duration = TimeSpan.FromSeconds(suite.Duration);

            this.Name = suite.Name;

            this.Cases = suite.Cases.Select(x => new TestCaseModel().LoadFrom(x)).ToList();

            return this;
        }        
    }
}

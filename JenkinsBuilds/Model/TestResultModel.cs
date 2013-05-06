﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Jenkins;

namespace JenkinsBuilds.Model
{
    [PropertyChanged.ImplementPropertyChanged]
    public class TestResultModel
    {
        public const string FetchTree = "duration,skipCount,passCount,failCount,suites[" + TestSuiteModel.FetchTree + "]";

        public TimeSpan Duration { get; set; }

        public int SkipCount { get; set; }

        public int PassCount { get; set; }

        public int FailCount { get; set; }

        public List<TestSuiteModel> Suites { get; set; }

        public TestResultModel LoadFrom(TestResults results)
        {
            this.Duration = TimeSpan.FromSeconds(results.Duration);
            this.FailCount = results.FailCount;
            this.PassCount = results.PassCount;
            this.SkipCount = results.SkipCount;

            this.Suites = results.Suites.Select(x => new TestSuiteModel().LoadFrom(x)).ToList();

            return this;
        }


    }
}

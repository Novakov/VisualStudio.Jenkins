using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jenkins.Model
{
    public class TestResults
    {
        public double Duration { get; set; }

        public int FailCount { get; set; }

        public int PassCount { get; set; }

        public int SkipCount { get; set; }

        public IList<Suite> Suites { get; set; }

        public TestResults()
        {
            this.Suites = new List<Suite>();
        }
    }
}

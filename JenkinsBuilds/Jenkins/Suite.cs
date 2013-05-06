using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JenkinsBuilds.Jenkins
{
    public class Suite
    {
        public double Duration { get; set; }

        public string Name { get; set; }

        public IList<TestCase> Cases { get; set; }

        public Suite()
        {
            this.Cases = new List<TestCase>();
        }
    }
}

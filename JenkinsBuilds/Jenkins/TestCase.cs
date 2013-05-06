using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JenkinsBuilds.Jenkins
{
    public class TestCase
    {
        public string ClassName { get; set; }

        public double Duration { get; set; }

        public string ErrorDetails { get; set; }

        public string ErrorStackTrace { get; set; }

        public string Name { get; set; }

        public bool Skipped { get; set; }

        public string SkippedMessage { get; set; }

        public string Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jenkins.Model
{
    public class Warning
    {
        public string Priority { get; set; }

        public string Message { get; set; }

        public int Key { get; set; }

        public int PrimaryLineNumber { get; set; }

        public string FileName { get; set; }
    }
}

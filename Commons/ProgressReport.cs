using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class ProgressReport
    {
        public long Total { get; private set; }
        public long Completed { get; private set; }

        public ProgressReport(long completed, long total)
        {
            this.Completed = completed;
            this.Total = total;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons;
using Microsoft.VisualStudio.Shell.Interop;

namespace JenkinsBuilds
{   
    public class StatusBarProgressReporter : IProgress<ProgressReport>
    {
        private readonly IVsStatusbar statusBar;
        private uint cookie;
        private string text;        

        public StatusBarProgressReporter(IVsStatusbar statusBar, string text)
        {
            this.statusBar = statusBar;
            this.cookie = 0;
            this.text = text;            
        }

        public void Report(ProgressReport value)
        {
            this.statusBar.Progress(ref cookie, value.Completed == value.Total ? 0 : 1, text, (uint)value.Completed, (uint)value.Total);
        }
    }

    public static class StatusBarExtensions
    {
        public static IProgress<ProgressReport> ProgressReporter(this IVsStatusbar @this, string text = "")
        {
            return new StatusBarProgressReporter(@this, text);
        }
    }
}

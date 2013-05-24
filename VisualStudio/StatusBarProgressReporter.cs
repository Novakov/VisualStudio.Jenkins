using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons;
using Microsoft.VisualStudio.Shell.Interop;

namespace VisualStudio
{
    public class StatusBarProgressReporter : IProgress<ProgressReport>
    {
        private readonly IStatusBar statusBar;
        private uint cookie;
        private string text;

        public StatusBarProgressReporter(IStatusBar statusBar, string text)
        {
            this.statusBar = statusBar;
            this.cookie = 0;
            this.text = text;
        }

        public void Report(ProgressReport value)
        {
            this.statusBar.Progress(this.text, value.Completed == value.Total, (uint)value.Completed, (uint)value.Total);
        }
    }

    public static class StatusBarExtensions
    {
        public static IProgress<ProgressReport> ProgressReporter(this IStatusBar @this, string text = "")
        {
            return new StatusBarProgressReporter(@this, text);
        }
    }
}

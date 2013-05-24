using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;

namespace JenkinsBuilds
{
    [Export(typeof(IStatusBar))]
    public class StatusBar : IStatusBar
    {
        private readonly IVsStatusbar statusBar;

        public StatusBar()
        {
            this.statusBar = (IVsStatusbar)JenkinsBuildsPackage.GetGlobalService(typeof(SVsStatusbar));
        }

        public void Progress(string text, bool isCompleted, uint completed, uint total)
        {
            uint cookie = 0;

            this.statusBar.Progress(ref cookie, isCompleted ? 0 : 1, text, completed, total);
        }
    }
}

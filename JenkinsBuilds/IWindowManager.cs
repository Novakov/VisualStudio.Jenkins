using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell;

namespace JenkinsBuilds
{
    public interface IWindowManager
    {
        void OpenBuildDetails(Uri uri, Model.BuildModel buildModel);

        TWindow FindWindow<TWindow>(bool create, int id = 0)
            where TWindow : ToolWindowPane;

        void OpenFile(string destPath);
    }
}

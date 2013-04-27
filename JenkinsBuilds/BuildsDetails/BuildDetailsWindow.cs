using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace JenkinsBuilds.BuildsDetails
{    
    public class BuildDetailsWindow : ToolWindowPane
    {
        private BuildDetailsViewModel viewModel;

        public BuildDetailsWindow()
        {
            this.viewModel = new BuildDetailsViewModel();

            this.Content = new BuildDetailsView
            {
                DataContext = this.viewModel
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsBuilds.Commands;
using Microsoft.TeamFoundation.Controls;

namespace JenkinsBuilds.Pages
{
    [TeamExplorerPage(AddJenkinsPage.PageId, ParentPageId = BuildsPage.PageId)]
    public class AddJenkinsPage : Base.TeamExplorerPageBase
    {
        public const string PageId = "{1256AA90-290A-4656-8BEB-4AF1B171B6BB}";
        
        private AddJenkinsView view;

        [ImportingConstructor]
        public AddJenkinsPage([ImportServiceProvider]IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.Title = "Add Jenkins";
            this.IsBusy = false;
            
            this.PageContent = this.view = new AddJenkinsView();

            this.view.CancelCommand = new DelegateCommand(Back);
            this.view.SaveCommand = new DelegateCommand(Save);
        }

        private void Save(object obj)
        {
            Properties.Settings.Default.Instances.Add(new Configuration.JenkinsInstance
            {
                DisplayName = this.view.DisplayName,
                Url = this.view.Url
            });

            Properties.Settings.Default.Save();

            this.Close();
        }

        private void Back(object obj)
        {            
            this.Close();
        }
    }
}

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
    public class AddJenkinsPage : Base.TeamExplorerPageBase<AddJenkinsPageView>
    {
        public const string PageId = "{1256AA90-290A-4656-8BEB-4AF1B171B6BB}";

        public new AddJenkinsPageViewModel ViewModel { get { return (AddJenkinsPageViewModel)base.ViewModel; } }

        public AddJenkinsPage()            
        {
            this.Title = "Add Jenkins";
            this.IsBusy = false;      
        }

        private void Save(object obj)
        {
            Properties.Settings.Default.Instances.Add(new Configuration.JenkinsInstance
            {
                DisplayName = this.ViewModel.DisplayName,
                Url = this.ViewModel.Url
            });

            Properties.Settings.Default.Save();

            this.Close();
        }

        private void Back(object obj)
        {            
            this.Close();
        }

        protected override AddJenkinsPageView CreateView()
        {
            return new AddJenkinsPageView();
        }

        protected override Base.ViewModelBase CreateViewModel()
        {
            return new AddJenkinsPageViewModel
            {
                CancelCommand = new DelegateCommand(Back),
                SaveCommand = new DelegateCommand(Save)
            };
        }
    }
}

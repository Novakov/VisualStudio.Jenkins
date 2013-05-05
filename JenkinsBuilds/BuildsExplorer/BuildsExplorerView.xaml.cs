using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JenkinsBuilds.Model;
using Niles.Model;

namespace JenkinsBuilds.BuildsExplorer
{
    /// <summary>
    /// Interaction logic for BuildsExplorerView.xaml
    /// </summary>
    public partial class BuildsExplorerView : UserControl
    {
        private Func<Uri, JenkinsClient> clientFactory;

        public BuildsExplorerViewModel ViewModel
        {
            get { return (BuildsExplorerViewModel)this.DataContext; }
            set { this.DataContext = value; }
        }

        public BuildsExplorerView()
        {
            InitializeComponent();

            this.clientFactory = u => new JenkinsClient(u);
        }

        private async void SelectedInstanceChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ViewModel.Jobs = await this.GetJobsFromInstance(this.ViewModel.SelectedInstance.Url);
        }

        private async Task<List<JobModel>> GetJobsFromInstance(string instanceUrl)
        {
            var client = this.clientFactory(new Uri(this.ViewModel.SelectedInstance.Url));

            var node = await client.GetResourceAsync<Node>(this.ViewModel.SelectedInstance.Url, "jobs[displayName,url]");

            return node.Jobs.Select(x => new JobModel().LoadFrom(x)).ToList();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var build = (sender as DataGrid).SelectedItem as BuildModel;

            this.ViewModel.OpenBuildDetailsCommand.ExecuteIfCan(build);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using VisualStudio;
using Commons.Wpf;

namespace JenkinsBuilds.BuildsExplorer
{
    /// <summary>
    /// Interaction logic for BuildsExplorerView.xaml
    /// </summary>
    public partial class BuildsExplorerView : UserControl
    {        
        public JobModel PreselectedJob { get; set; }

        public BuildsExplorerViewModel ViewModel
        {
            get { return (BuildsExplorerViewModel)this.DataContext; }
            set { this.DataContext = value; }
        }

        public BuildsExplorerView()
        {
            InitializeComponent();
        }                       

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var build = (sender as DataGrid).SelectedItem as BuildModel;

            this.ViewModel.OpenBuildDetailsCommand.ExecuteIfCan(build);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel.SelectedStatus = ((IEnumerable<ResultFilterItem>)this.Resources["resultFilterItems"]).FirstOrDefault();

            ((INotifyPropertyChanged)this.ViewModel).PropertyChanged += ViewModelPropertyChanged;
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Jobs")
            {
                this.ViewModel.SelectedJob = this.ViewModel.Jobs.FirstOrDefault();
            }
        }        
    }
}

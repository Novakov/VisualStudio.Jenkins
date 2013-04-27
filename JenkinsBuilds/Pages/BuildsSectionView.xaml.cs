using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace JenkinsBuilds.Pages
{
    /// <summary>
    /// Interaction logic for BuildsSectionView.xaml
    /// </summary>
    public partial class BuildsSectionView : UserControl
    {
        public BuildsSectionViewModel ViewModel { get { return (BuildsSectionViewModel)this.DataContext; } }

        public BuildsSectionView()
        {
            InitializeComponent();
        }

        private void BuildNowClick(object sender, RoutedEventArgs e)
        {
            var param = ((ICommandSource)sender).CommandParameter;
            this.ViewModel.BuildNowCommand.ExecuteIfCan(param);
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var job = (JobViewModel)((ListViewItem)sender).DataContext;

            Process.Start(job.JobUrl.ToString());
        }

        private void RemoveFromFavouritesClick(object sender, RoutedEventArgs e)
        {
            var param = ((ICommandSource)sender).CommandParameter;
            this.ViewModel.RemoveFromFavourites.ExecuteIfCan(param);
        }
    }
}

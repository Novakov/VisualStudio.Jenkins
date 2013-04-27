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
using Microsoft.TeamFoundation.MVVM;

namespace JenkinsBuilds.Pages
{
    public partial class InstancesSectionView : UserControl
    {
        public InstancesSectionViewModel ViewModel { get { return (InstancesSectionViewModel)this.DataContext; } }

        public InstancesSectionView()
        {
            InitializeComponent();
        }

        private void ViewJobsClick(object sender, RoutedEventArgs e)
        {
            var param = ((ICommandSource)sender).CommandParameter;
            this.ViewModel.ViewJobsCommand.ExecuteIfCan(param);
        }

        private void RemoveInstanceClick(object sender, RoutedEventArgs e)
        {
            var param = ((ICommandSource)sender).CommandParameter;
            this.ViewModel.RemoveInstanceCommand.ExecuteIfCan(param);
        }
    }
}

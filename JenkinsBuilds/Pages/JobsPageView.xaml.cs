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
using Niles.Model;

namespace JenkinsBuilds.Pages
{
    public partial class JobsPageView : UserControl
    {
        public JobsPageViewModel ViewModel { get { return (JobsPageViewModel)this.DataContext; } }

        public JobsPageView()
        {
            this.DataContext = this;

            InitializeComponent();
        }

        private void AddToFavouritesClick(object sender, RoutedEventArgs e)
        {
            var param = ((ICommandSource)sender).CommandParameter;
            this.ViewModel.AddToFavouritesCommand.ExecuteIfCan(param);
        }
    }
}

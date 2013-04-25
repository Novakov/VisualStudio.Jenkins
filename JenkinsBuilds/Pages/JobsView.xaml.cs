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
    /// <summary>
    /// Interaction logic for JobsView.xaml
    /// </summary>
    public partial class JobsView : UserControl
    {
        public IEnumerable<JobViewModel> Jobs
        {
            get { return (IEnumerable<JobViewModel>)GetValue(JobsProperty); }
            set { SetValue(JobsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Jobs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JobsProperty =
            DependencyProperty.Register("Jobs", typeof(IEnumerable<JobViewModel>), typeof(JobsView), new PropertyMetadata(null));

        public ICommand AddToFavouritesCommand
        {
            get { return (ICommand)GetValue(AddToFavouritesCommandProperty); }
            set { SetValue(AddToFavouritesCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddToFavouritesCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddToFavouritesCommandProperty =
            DependencyProperty.Register("AddToFavouritesCommand", typeof(ICommand), typeof(JobsView), new PropertyMetadata(null));

        public JobsView()
        {
            this.DataContext = this;

            InitializeComponent();
        }

        private void AddToFavouritesClick(object sender, RoutedEventArgs e)
        {
            var param = ((ICommandSource)sender).CommandParameter;

            if (this.AddToFavouritesCommand != null && this.AddToFavouritesCommand.CanExecute(param))
            {
                this.AddToFavouritesCommand.Execute(param);
            }
        }
    }
}

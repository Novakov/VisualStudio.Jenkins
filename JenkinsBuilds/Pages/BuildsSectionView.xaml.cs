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
        public ObservableCollection<JobViewModel> Jobs
        {
            get { return (ObservableCollection<JobViewModel>)GetValue(JobsProperty); }
            set { SetValue(JobsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Jobs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JobsProperty =
            DependencyProperty.Register("Jobs", typeof(ObservableCollection<JobViewModel>), typeof(BuildsSectionView), new PropertyMetadata());

        public ICommand BuildNowCommand
        {
            get { return (ICommand)GetValue(BuildNowCommandProperty); }
            set { SetValue(BuildNowCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BuildNowCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BuildNowCommandProperty =
            DependencyProperty.Register("BuildNowCommand", typeof(ICommand), typeof(BuildsSectionView), new PropertyMetadata(null));

        public BuildsSectionView()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        private void BuildNowClick(object sender, RoutedEventArgs e)
        {
            var param = ((ICommandSource)sender).CommandParameter;
            if (this.BuildNowCommand != null && this.BuildNowCommand.CanExecute(param))
            {
                this.BuildNowCommand.Execute(param);
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var job = (JobViewModel)((ListViewItem)sender).DataContext;

            Process.Start(job.JobUrl.ToString());
        }
    }
}

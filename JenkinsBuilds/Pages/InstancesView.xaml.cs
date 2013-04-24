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
    /// <summary>
    /// Interaction logic for InstancesView.xaml
    /// </summary>
    public partial class InstancesView : UserControl
    {
        public ICommand AddInstanceCommand
        {
            get { return (ICommand)GetValue(AddInstanceCommandProperty); }
            set { SetValue(AddInstanceCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddInstanceCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddInstanceCommandProperty =
            DependencyProperty.Register("AddInstanceCommand", typeof(ICommand), typeof(InstancesView), new PropertyMetadata(null));        

        public InstancesView()
        {
            this.DataContext = this;
            InitializeComponent();

            //this.a.Command = new RelayCommand(x => { this.LoadInstances(new Configuration.JenkinsInstance[0]); });
        }

        public void LoadInstances(IEnumerable<Configuration.JenkinsInstance> instances)
        {
            this.instancesList.ItemsSource = instances;
        }
    }
}

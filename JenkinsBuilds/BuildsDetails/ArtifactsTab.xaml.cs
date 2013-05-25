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
using Commons.Paths;
using VisualStudio;
using Commons.Wpf;

namespace JenkinsBuilds.BuildsDetails
{
    /// <summary>
    /// Interaction logic for BuildArtifactsTab.xaml
    /// </summary>
    public partial class ArtifactsTab : UserControl
    {
        public BuildDetailsViewModel ViewModel
        {
            get { return (BuildDetailsViewModel)this.DataContext; }
        }

        public ArtifactsTab()
        {
            InitializeComponent();
        }

        private void ViewFile(object sender, RoutedEventArgs e)
        {
            var item = (FileItem)((FrameworkElement)sender).DataContext;
            var ancestorsAndSelf = new[] { item }.Union(item.Ancestors()).Reverse();

            var path = string.Join("/", ancestorsAndSelf.Select(x => x.Name));

            this.ViewModel.ViewFileCommand.ExecuteIfCan(path);
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            var item = (FileItem)((FrameworkElement)sender).DataContext;
            var ancestorsAndSelf = new[] { item }.Union(item.Ancestors()).Reverse();

            var path = string.Join("/", ancestorsAndSelf.Select(x => x.Name));

            this.ViewModel.SaveFileAsCommand.ExecuteIfCan(path);
        }
    }
}

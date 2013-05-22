using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JenkinsBuilds
{
    public class TypeBasedTemplateSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var element = container as FrameworkElement;

            return element.TryFindResource(item.GetType().Name) as DataTemplate;
        }
    }
}

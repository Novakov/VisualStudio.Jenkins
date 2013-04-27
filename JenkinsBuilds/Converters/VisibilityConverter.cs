using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace JenkinsBuilds.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public Visibility WhenTrue { get; set; }
        public Visibility WhenFalse { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return this.WhenTrue;
            }
            else
            {
                return this.WhenFalse;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

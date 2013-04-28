using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using JenkinsBuilds.Model;

namespace JenkinsBuilds.BuildsDetails
{
    public class WarningTextLinkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var w = (WarningModel)value;

            return string.Format("{0} ({1}):", w.FileName, w.LineNumber);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

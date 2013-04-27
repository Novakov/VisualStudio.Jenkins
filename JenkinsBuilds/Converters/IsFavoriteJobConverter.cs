using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using JenkinsBuilds.Model;
using JenkinsBuilds.Properties;

namespace JenkinsBuilds.Converters
{
    public class IsFavoriteJobConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var job = (JobModel)value;

            if (parameter != null)
            {
                return Settings.Default.IsFavorite(job.Url);
            }
            else
            {
                return !Settings.Default.IsFavorite(job.Url);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

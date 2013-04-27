using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using JenkinsBuilds.Model;

namespace JenkinsBuilds.Converters
{
    public class FavouriteJobMarkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var job = (JobModel)value;
            if (Properties.Settings.Default.IsFavourite(job.Url))
            {
                return new BitmapImage(new Uri("/JenkinsBuilds;component/Images/star.png", UriKind.Relative));
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

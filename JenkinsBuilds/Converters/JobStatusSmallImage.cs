using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace JenkinsBuilds.Converters
{
    public class JobStatusSmallImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            //if (!(value is ))
            //    throw new NotImplementedException();

            String path = null;
            path = "Images/results/16/" + value.ToString() + ".png";

            return new BitmapImage(new Uri("/JenkinsBuilds;component/" + path, UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

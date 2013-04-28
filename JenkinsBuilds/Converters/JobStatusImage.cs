using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace JenkinsBuilds.Converters
{
    public class JobStatusImage : IValueConverter
    {
        public string Size { get; set; }

        public JobStatusImage()
        {
            this.Size = "16";
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            
            var path = string.Format("/JenkinsBuilds;component/Images/results/{0}/{1}.png", this.Size, value);

            return new BitmapImage(new Uri(path, UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

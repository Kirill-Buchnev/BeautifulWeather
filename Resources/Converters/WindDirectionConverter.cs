using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BeautifulWeather.Resources.Converters
{
    class WindDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string resourceName = string.Empty;

            var angle = (int)value;

            if ((angle >= 0 && angle <= 45) || angle >= 315)
                resourceName = "wind_arrow_right";

            else if (angle > 225 && angle < 315)
                resourceName = "wind_arrow_top";

            else if (angle >= 135 && angle <= 225)
                resourceName = "wind_arrow_left";

            else if (angle > 45 && angle < 135)
                resourceName = "wind_arrow_bottom";

            var res = Application.Current.Resources[resourceName] as ControlTemplate;
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BeautifulWeather.Resources.Converters
{
    public class WeatherCodeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string resourceName;
            switch ((WeatherCodes)value)
            {
                case WeatherCodes.ClearSky:
                    resourceName = "clear_day";
                    break;

                case WeatherCodes.PartlyCloudly:
                    resourceName = "cloudly";
                    break;

                case WeatherCodes.Overcast:
                    resourceName = "overcast";
                    break;

                case WeatherCodes.Fog:
                case WeatherCodes.DepositingRimeFog:
                    resourceName = "fog";
                    break;

                case WeatherCodes.SlightRain:
                case WeatherCodes.ModerateRain:
                case WeatherCodes.HeavyRain:
                    resourceName = "rain";
                    break;

                case WeatherCodes.Windy:
                    resourceName = "wind";
                    break;

                case WeatherCodes.ModerateSnowfall:
                case WeatherCodes.SnowGrains:
                    resourceName = "moderate-snowfall";
                    break;

                default: return null;
            }

            return Application.Current.Resources[resourceName] as ControlTemplate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

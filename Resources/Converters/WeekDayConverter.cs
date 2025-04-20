using System.Globalization;
using System.Windows.Data;

namespace BeautifulWeather.Resources.Converters
{
    public class WeekDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null) return App.Current.Resources[value].ToString();
            return default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

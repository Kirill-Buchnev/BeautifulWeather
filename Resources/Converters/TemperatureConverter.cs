using BeautifulWeather.Services;
using BeautifulWeather.Services.Settings;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Windows.Data;

namespace BeautifulWeather.Resources.Converters
{
    public class TemperatureConverter : IValueConverter
    {
        private readonly ISettingsService _settingsService = ServiceLocator.ServiceProvider.GetService<ISettingsService>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var settings = _settingsService.Settings;
            var temperatureMeasure = settings.TemperatureMeasure;

            var temperature = (float)value;
            if (temperatureMeasure == Models.TemperatureMeasure.Fahrenheit) temperature += 32;

            var dimension = App.Current.Resources[temperatureMeasure.ToString()].ToString();
            return temperature + dimension;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

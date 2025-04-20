using BeautifulWeather.Services;
using BeautifulWeather.Services.Settings;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Windows.Data;

namespace BeautifulWeather.Resources.Converters
{
    public class PressureConverter : IValueConverter
    {
        private readonly ISettingsService _settingsService = ServiceLocator.ServiceProvider.GetService<ISettingsService>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var settings = _settingsService.Settings;
            var pressureMeasure = settings.PressureMeasure;

            var pressure = (float)value;
            if (pressureMeasure == Models.PressureMeasure.MmHg) pressure *= (float)0.7501;

            var dimension = App.Current.Resources[pressureMeasure.ToString()].ToString();
            return Math.Round(pressure) + $" {dimension}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

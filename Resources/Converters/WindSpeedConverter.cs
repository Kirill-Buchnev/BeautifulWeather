using BeautifulWeather.Services.Settings;
using BeautifulWeather.Services;
using System.Globalization;
using System.Windows.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BeautifulWeather.Resources.Converters
{
    public class WindSpeedConverter : IValueConverter
    {
        private readonly ISettingsService _settingsService = ServiceLocator.ServiceProvider.GetService<ISettingsService>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var settings = _settingsService.Settings;
            var windSpeedMeasure = settings.WindSpeedMeasure;

            var windSpeed = (float)value;
            switch (windSpeedMeasure)
            {
                case Models.WindSpeedMeasure.Mph : windSpeed *= (float)2.23694; break;
                case Models.WindSpeedMeasure.Kmh : windSpeed *= (float)3.6; break;
                case Models.WindSpeedMeasure.Kn : windSpeed *= (float)1.94384466; break;
            }

            var dimension = App.Current.Resources[windSpeedMeasure.ToString()].ToString();
            return Math.Round(windSpeed, 2) + $" {dimension}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

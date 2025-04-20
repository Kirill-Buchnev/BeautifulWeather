using BeautifulWeather.Services.Settings;
using BeautifulWeather.Services;
using System.Globalization;
using System.Windows.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BeautifulWeather.Resources.Converters
{
    public class PrecipitationConverter : IValueConverter
    {
        private readonly ISettingsService _settingsService = ServiceLocator.ServiceProvider.GetService<ISettingsService>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var settings = _settingsService.Settings;
            var precipitationMeasure = settings.PrecipitationMeasure;

            var pressure = (float)value;
            switch (precipitationMeasure)
            {
                case Models.PrecipitationMeasure.Cm : pressure *= (float)0.1; break;
                case Models.PrecipitationMeasure.Inch : pressure /= (float)25.4; break;
            }

            var dimension = App.Current.Resources[precipitationMeasure.ToString()].ToString();
            return Math.Round(pressure, 2) + $" {dimension}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

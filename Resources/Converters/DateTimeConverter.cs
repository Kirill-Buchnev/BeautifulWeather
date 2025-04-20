using BeautifulWeather.Services;
using BeautifulWeather.Services.Settings;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Windows.Data;

namespace BeautifulWeather.Resources.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        private readonly ISettingsService _settingsService = ServiceLocator.ServiceProvider.GetService<ISettingsService>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return default;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(_settingsService.Settings.Culture.ToString());
            return string.Format($"{(DateTime)value : HH : mm}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

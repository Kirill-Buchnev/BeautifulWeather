using BeautifulWeather.Models;
using BeautifulWeather.Services;
using BeautifulWeather.Services.Settings;
using BeautifulWeather.Storages.Days;
using Microsoft.Extensions.DependencyInjection;

namespace BeautifulWeather.ViewModels
{
    public class HomeViewViewModel : ViewModelBase
    {
        private List<DayForecastModel> forecastDays;
        private IWeatherProvider _weatherStorage;

        private WeatherForecast currentWeather;

        public List<DayForecastModel> ForecastDays
		{
			get => forecastDays;
			set 
			{
				forecastDays = value;
                OnPropertyChanged();
			}
		}

        private DayForecastModel selectedDay;

        public DayForecastModel SelectedDay
        {
            get => selectedDay;
            set
            {
                selectedDay = value;
                OnPropertyChanged();
            }
        }

        private string weekDay;
        private readonly ISettingsService _settingsService = ServiceLocator.ServiceProvider.GetService<ISettingsService>();

        public string WeekDay
        {
            get => weekDay;
            set
            {
                weekDay = value;
                OnPropertyChanged();
            }
        }

        public HomeViewViewModel(IWeatherProvider weatherStorage)
        {
            _weatherStorage = weatherStorage;
        }

        public void TryUpdateWeather()
        {
            var settings = _settingsService.Settings;
            var selectedLocation = settings.SelectedLocation;
            if (currentWeather == null || selectedLocation.Latitude != currentWeather?.Location?.Latitude || selectedLocation.Longitude != currentWeather?.Location?.Longitude)
            {
                var weather = _weatherStorage.Get(selectedLocation.Latitude,
                    selectedLocation.Longitude,
                    new ForecastMeasureModel { TemperatureMeasure = settings.TemperatureMeasure},
                    selectedLocation.Name);
                currentWeather = weather;
                ForecastDays = weather.DayForecasts;
            }
        }
    }
}

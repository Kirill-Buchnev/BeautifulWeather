using BeautifulWeather.Models;
using BeautifulWeather.Services.GeoCoder;
using BeautifulWeather.Services.Weather;

namespace BeautifulWeather.Storages.Days
{
    public class WeatherDataStorage : IWeatherProvider
    {
        private readonly OpenMeteoProvider _openMeteoProvider;
        private readonly DatabaseContext _databaseContext;

        public WeatherDataStorage(OpenMeteoProvider openMeteoProvider, DatabaseContext databaseContext)
        {
                _openMeteoProvider = openMeteoProvider;
                _databaseContext = databaseContext;
        }
        public WeatherForecast Get(float latitude, float longitude, ForecastMeasureModel measures, string name)
        {
            var weather = _openMeteoProvider.GetWeather(latitude, longitude, measures);
            var location = weather.Location;
            location.Name = name;

            if (location.Description == null) location.Description = "Undefined";
            AddWeather(weather, location);
            return weather;
        }

        private void AddWeather(WeatherForecast weather, GeoLocation? location)
        {
            _databaseContext.GeoLocations.ToList()
                        .ForEach(x =>
                        {
                            if (x.Latitude == location.Latitude && x.Longitude == location.Longitude)
                            {
                                weather.Location = null;
                                _databaseContext.WeatherForecasts.Add(weather);
                                _databaseContext.SaveChanges();
                                weather.Location = x;
                                _databaseContext.SaveChanges();
                            }
                        });
        }
    }
}

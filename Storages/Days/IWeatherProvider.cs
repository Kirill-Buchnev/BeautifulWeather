using BeautifulWeather.Models;
using System.Globalization;

namespace BeautifulWeather.Storages.Days
{
    public interface IWeatherProvider
    {
        WeatherForecast Get(float latitude, float longitude, ForecastMeasureModel measures, string name);
    }
}

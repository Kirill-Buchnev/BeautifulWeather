using BeautifulWeather.Models;
using BeautifulWeather.Storages;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace BeautifulWeather.Services.Weather
{
    public class OpenMeteoProvider
    {
        private readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast/")
        };

        public WeatherForecast GetWeather(float latitude, float longitude, ForecastMeasureModel measures)
        {
            var url = new StringBuilder();
            url.Append("?latitude=" + latitude.ToString(CultureInfo.InvariantCulture));
            url.Append("&longitude=" + longitude.ToString(CultureInfo.InvariantCulture));
            url.Append("&temperature_unit=" + measures.TemperatureMeasure.ToString().ToLower());
            url.Append("&timezone=auto");
            url.Append("&past_days=2");
            url.Append("&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,rain_sum,showers_sum,snowfall_sum,precipitation_hours,weathercode,sunrise,sunset,windspeed_10m_max,windgusts_10m_max,winddirection_10m_dominant");
            url.Append("&hourly=temperature_2m,relativehumidity_2m,apparent_temperature,surface_pressure,windspeed_10m,winddirection_10m,weathercode");

            DailyApiResponse response;
            try
            {
                response = httpClient.GetFromJsonAsync<DailyApiResponse>(url.ToString()).Result;
            }
            catch
            {
                return null!;
            }

            return ToWeatherForecastModel(response!, measures, latitude, longitude);
        }

        private WeatherForecast ToWeatherForecastModel(DailyApiResponse apiModel, ForecastMeasureModel measures, float latitude, float longitude)
        {
            WeatherForecast weatherForecast = new WeatherForecast();

            weatherForecast.Location = new()
            {
                Latitude = latitude,
                Longitude = longitude,
            };

            weatherForecast.Measures = new ForecastMeasureModel()
            {
                TemperatureMeasure = measures.TemperatureMeasure,
            };

            int hoursCounter = 0;
            for (int i = 0; i < apiModel?.Daily?.Time?.Count; i++)
            {
                DayForecastModel day = new()
                {
                    Date = apiModel.Daily.Time[i],
                    Weather = (WeatherCodes)apiModel.Daily.Weathercode[i],
                    MaxTemperature = apiModel.Daily.Temperature_2m_max[i],
                    MinTemperature = apiModel.Daily.Temperature_2m_min[i],
                    WindDirection = apiModel.Daily.Winddirection_10m_dominant[i],
                    WindSpeed = apiModel.Daily.Windspeed_10m_max[i]
                };
                for (int j = hoursCounter; j < hoursCounter + 24; j++)
                {
                    HourlyForecastModel hour = new()
                    {
                        ApparentTemperature = apiModel.Hourly.Apparent_temperature[j],
                        RelativeHumidity = apiModel.Hourly.Relativehumidity_2m[j],
                        SurfacePressure = apiModel.Hourly.Surface_pressure[j],
                        Temperature = apiModel.Hourly.Temperature_2m[j],
                        Time = apiModel.Hourly.Time[j],
                        Weather = (WeatherCodes)apiModel.Hourly.Weathercode[j],
                        WindDirection = apiModel.Hourly.Winddirection_10m[j],
                        WindSpeed = apiModel.Hourly.Windspeed_10m[j]
                    };
                    day.HourlyForecasts.Add(hour);
                }

                day.Pressure = day.HourlyForecasts.Sum(hour => hour.SurfacePressure) / 24;
                hoursCounter += 24;
                weatherForecast.DayForecasts.Add(day);
            }

            weatherForecast.StartDate = apiModel.Daily.Time.First();
            weatherForecast.EndDate = apiModel.Daily.Time.Last();

            return weatherForecast;
        }
    }
}

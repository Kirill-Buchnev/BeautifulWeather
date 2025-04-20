using Microsoft.EntityFrameworkCore;

namespace BeautifulWeather.Models
{
    [PrimaryKey("Guid")]
    public class DayForecastModel
    {
        public Guid Guid { get; set; } = new Guid();
        public DateTime Date { get; set; }
        public string? WeekDay { get; set; }
        public float MaxTemperature { get; set; }
        public float MinTemperature { get; set; }
        public float Pressure { get; set; }
        public float WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public string? Location { get; set; }
        public WeatherCodes Weather { get; set; }
        public List<HourlyForecastModel> HourlyForecasts { get; set; } = new List<HourlyForecastModel>();
    }
}

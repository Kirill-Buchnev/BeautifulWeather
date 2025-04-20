using BeautifulWeather.Services.GeoCoder;
using Microsoft.EntityFrameworkCore;

namespace BeautifulWeather.Models
{
    [PrimaryKey("Guid")]
    public class WeatherForecast
    {
        public Guid Guid { get; set; }
        public GeoLocation? Location { get; set; }
        public ForecastMeasureModel? Measures { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<DayForecastModel?> DayForecasts { get; set; } = new List<DayForecastModel>();
    }
}
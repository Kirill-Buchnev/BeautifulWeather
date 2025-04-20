using Microsoft.EntityFrameworkCore;

namespace BeautifulWeather.Models
{
    [PrimaryKey("Guid")]
    public class HourlyForecastModel
    {
        public Guid Guid { get; set; } = new Guid();
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public float ApparentTemperature { get; set; }
        public float RelativeHumidity { get; set; }
        public float SurfacePressure { get; set; }
        public float WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public WeatherCodes Weather { get; set; }
    }
}

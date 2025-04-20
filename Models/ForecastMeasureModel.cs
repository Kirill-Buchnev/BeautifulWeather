using Microsoft.EntityFrameworkCore;

namespace BeautifulWeather.Models
{
    [PrimaryKey("Guid")]
    public class ForecastMeasureModel
    {
        public Guid? Guid { get; set; }
        public TemperatureMeasure? TemperatureMeasure { get; set; }
        public PressureMeasure? PressureMeasure { get; set; }
        public PrecipitationMeasure? PrecipitationMeasure { get; set; }
        public WindSpeedMeasure? WindSpeedMeasure { get; set; }
    }
}
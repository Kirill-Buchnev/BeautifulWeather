using BeautifulWeather.Models;
using BeautifulWeather.Services.GeoCoder;
using BeautifulWeather.Views.Settings;
using Microsoft.EntityFrameworkCore;

namespace BeautifulWeather.Services.Settings
{
    [PrimaryKey("Guid")]
    public class Settings
    {
        public Guid Guid { get; set; }
        public Cultures Culture { get; set; } = Cultures.RU;
        public TemperatureMeasure TemperatureMeasure { get; set; } = TemperatureMeasure.Celsius;
        public PressureMeasure PressureMeasure { get; set; } = PressureMeasure.HPa;
        public PrecipitationMeasure PrecipitationMeasure { get; set; } = PrecipitationMeasure.Mm;
        public WindSpeedMeasure WindSpeedMeasure { get; set; } = WindSpeedMeasure.Ms;
        public GeoLocation? SelectedLocation { get; set; } = new GeoLocation
        {
            Name = "Санкт-Петербург",
            Latitude = 30.314997F,
            Longitude = 59.938784F,
            Description = "Россия"
        };
    }
}

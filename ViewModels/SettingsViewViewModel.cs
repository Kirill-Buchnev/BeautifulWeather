using BeautifulWeather.Models;
using BeautifulWeather.Services.Localization;
using BeautifulWeather.Services.Settings;
using BeautifulWeather.Views.Settings;
using System.Net.WebSockets;

namespace BeautifulWeather.ViewModels
{
    public class SettingsViewViewModel : ViewModelBase
    {
		private readonly ILocalizationService _localizationService;
        private readonly ISettingsService settingsService;

        public SettingsViewViewModel(ILocalizationService localizationService, ISettingsService settingsService)
        {
            _localizationService = localizationService;
            this.settingsService = settingsService;
            var currentSettings = settingsService.Settings;
            languageController = currentSettings.Culture == Cultures.RU;
            temperatureController = currentSettings.TemperatureMeasure == TemperatureMeasure.Fahrenheit;
            pressureController = currentSettings.PressureMeasure == PressureMeasure.MmHg;
            precipitationController = currentSettings.PrecipitationMeasure == PrecipitationMeasure.Inch;
            windSpeedController = currentSettings.WindSpeedMeasure == WindSpeedMeasure.Kmh;
        }

        private bool languageController;

        public bool LanguageController
        {
            get { return languageController; }
            set
            {
                var settings = settingsService.Settings;
                if (!value)
                {
                    settings.Culture = Cultures.EN;
                }
                else
                {
                    settings.Culture = Cultures.RU;
                }
                _localizationService.SetCulture(settings.Culture);
                Set(ref languageController, value, nameof(LanguageController));
            }
        }

        private bool temperatureController;

        public bool TemperatureController
        {
            get { return temperatureController; }
            set
            {
                var settings = settingsService.Settings;
                if (!value)
                {
                    settings.TemperatureMeasure = TemperatureMeasure.Celsius;
                }
                else
                {
                    settings.TemperatureMeasure = TemperatureMeasure.Fahrenheit;
                }
                Set(ref temperatureController, value, nameof(TemperatureController));
            }
        }

        private bool pressureController;

        public bool PressureController
        {
            get { return pressureController; }
            set
            {
                var settings = settingsService.Settings;
                if (!value)
                {
                    settings.PressureMeasure = PressureMeasure.HPa;
                }
                else
                {
                    settings.PressureMeasure = PressureMeasure.MmHg;
                }
                Set(ref pressureController, value, nameof(TemperatureController));
            }
        }

        private bool precipitationController;

        public bool PrecipitationController
        {
            get { return precipitationController; }
            set
            {
                var settings = settingsService.Settings;
                if (!value)
                {
                    settings.PrecipitationMeasure = PrecipitationMeasure.Mm;
                }
                else
                {
                    settings.PrecipitationMeasure = PrecipitationMeasure.Inch;
                }
                Set(ref precipitationController, value, nameof(PrecipitationController));
            }
        }

        private bool windSpeedController;

        public bool WindSpeedController
        {
            get { return windSpeedController; }
            set
            {
                var settings = settingsService.Settings;
                if (!value)
                {
                    settings.WindSpeedMeasure = WindSpeedMeasure.Ms;
                }
                else
                {
                    settings.WindSpeedMeasure = WindSpeedMeasure.Kmh;
                }
                Set(ref windSpeedController, value, nameof(WindSpeedController));
            }
        }

        private bool Set(ref bool field, bool value, string propertyName)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

    }
}

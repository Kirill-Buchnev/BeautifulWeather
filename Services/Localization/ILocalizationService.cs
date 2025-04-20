using BeautifulWeather.Views.Settings;

namespace BeautifulWeather.Services.Localization
{
    public interface ILocalizationService
    {
        void SetCulture(Cultures culture);
    }
}
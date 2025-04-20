using BeautifulWeather.Storages;
using BeautifulWeather.Views.Settings;

namespace BeautifulWeather.Services.Settings
{
    public interface ISettingsService
    {
        Settings Settings { get; }
        void Store();
    }
}
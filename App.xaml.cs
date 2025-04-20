using BeautifulWeather.Services;
using BeautifulWeather.Services.GeoCoder;
using BeautifulWeather.Services.Localization;
using BeautifulWeather.Services.Settings;
using BeautifulWeather.Services.Weather;
using BeautifulWeather.Storages;
using BeautifulWeather.Storages.Days;
using BeautifulWeather.Storages.FavoriteLocations;
using BeautifulWeather.Storages.Users;
using BeautifulWeather.ViewModels;
using BeautifulWeather.ViewModels.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace BeautifulWeather
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<HomeViewViewModel>();
                    services.AddSingleton<AuthorizationWindowViewModel>();
                    services.AddSingleton<RegistrationWindowViewModel>();

                    services.AddSingleton<GeoCoderService>();
                    services.AddSingleton<OpenMeteoProvider>();

                    services.AddSingleton<ILocalizationService, LocalizationService>();
                    services.AddSingleton<ISettingsService, SettingsService>();
                    services.AddSingleton<IWeatherProvider, WeatherDataStorage>();
                    services.AddSingleton<IUserStorage, UserStorage>();
                    services.AddSingleton<IFavoriteLocationsStorage, FavoriteLocationsStorage>();

                    var connectionString = "Data Source=beautifulWeather.db";
                    services.AddDbContext<DatabaseContext>(options => options.UseSqlite(connectionString));
                })
                .Build();
        }

        protected async void OnStartup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();
            ServiceLocator.ServiceProvider = _host.Services;

            var mainWindow = _host.Services.GetService<MainWindow>();
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                var settingsService = _host.Services.GetService<ISettingsService>();
                settingsService.Store();
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }
            base.OnExit(e);
        }
    }

}

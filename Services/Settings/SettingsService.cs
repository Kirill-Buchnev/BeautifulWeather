using BeautifulWeather.Storages;
using BeautifulWeather.Storages.Days;
using Microsoft.EntityFrameworkCore;

namespace BeautifulWeather.Services.Settings;
public class SettingsService : ISettingsService
    {
    public Settings Settings { get; }
    private readonly DatabaseContext _databaseContext;

    public SettingsService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        Settings = _databaseContext.Settings.Include(x => x.SelectedLocation).ToList().FirstOrDefault() ?? new Settings();
    }

    public void Store()
    {
        ClearTable(_databaseContext);
        if (Settings.SelectedLocation.Description == null) Settings.SelectedLocation.Description = "Undefined";
        _databaseContext.Settings.Add(Settings);
        _databaseContext.SaveChanges();
    }

    private void ClearTable(DatabaseContext _databaseContext)
    {
        foreach (var item in _databaseContext.Settings)
        {
            _databaseContext.Settings.Remove(item);
        }
        _databaseContext.SaveChanges();
    }
}

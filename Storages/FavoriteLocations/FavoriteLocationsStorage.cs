using BeautifulWeather.Services.GeoCoder;
using BeautifulWeather.Services.Settings;
using Microsoft.EntityFrameworkCore;

namespace BeautifulWeather.Storages.FavoriteLocations
{
    public class FavoriteLocationsStorage : IFavoriteLocationsStorage
    {
        private readonly DatabaseContext _databaseContext;

        public FavoriteLocationsStorage(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(GeoLocation location)
        {
            _databaseContext.WeatherForecasts.ToList()
                        .ForEach(x =>
                        {
                            if (x.Location.Latitude == location.Latitude && x.Location.Longitude == location.Longitude)
                            {
                                x.Location = null;
                                _databaseContext.GeoLocations.Add(location);
                                _databaseContext.SaveChanges();
                                x.Location = location;
                                _databaseContext.SaveChanges();
                                return;
                            }
                        });
            _databaseContext.GeoLocations.Add(location);
            _databaseContext.SaveChanges();
        }

        public List<GeoLocation> GetAll()
        {
            return _databaseContext.GeoLocations.ToList();
        }

        public void Remove(GeoLocation location)
        {
            _databaseContext.GeoLocations.Remove(location);
            _databaseContext.SaveChanges();
            _databaseContext.WeatherForecasts.ToList().ForEach(weather => 
            {
                if (weather.Location is null)
                {
                    _databaseContext.WeatherForecasts.Remove(weather);
                }
            });
            _databaseContext.SaveChanges();
        }
    }
}

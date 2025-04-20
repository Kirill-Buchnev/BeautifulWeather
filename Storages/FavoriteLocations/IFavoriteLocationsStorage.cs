using BeautifulWeather.Services.GeoCoder;

namespace BeautifulWeather.Storages.FavoriteLocations
{
    public interface IFavoriteLocationsStorage
    {
        void Add(GeoLocation location);
        List<GeoLocation> GetAll();
        void Remove(GeoLocation location);
    }
}
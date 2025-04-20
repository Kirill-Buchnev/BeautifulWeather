using BeautifulWeather.Storages;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;

namespace BeautifulWeather.Services.GeoCoder
{
    public class GeoCoderService
    {
        private const string apiKey = "c5ec2eac-f84d-4eae-a25e-aee322402501";
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly DatabaseContext _databaseContext;

        public GeoCoderService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public List<GeoLocation> GetLocation (string place)
        {
            var existingLocations = _databaseContext.GeoLocations.Where(x => x.Name.Contains(place)).ToList();
            if (existingLocations.Count > 0)
            {
                return existingLocations;
            }
            string url = $"https://geocode-maps.yandex.ru/1.x?apikey={apiKey}&geocode={place}&format=json";
            var response = _httpClient.GetFromJsonAsync<ApiResponse>(url).Result;
            var geoLocations = ToGeoLocation(response);
            return geoLocations;
        }

        private List<GeoLocation> ToGeoLocation(ApiResponse? response)
        {
            var locations = new List<GeoLocation>();
            foreach (var item in response.Response.GeoObjectCollection.FeatureMember)
            {
                var location = new GeoLocation();
                location.Name = item.GeoObject.Name;
                location.Description = item.GeoObject.Description;

                var points = item.GeoObject.Point.Pos.Split();
                location.Longitude = float.Parse(points[0], CultureInfo.InvariantCulture.NumberFormat);
                location.Latitude = float.Parse(points[1], CultureInfo.InvariantCulture.NumberFormat);

               locations.Add(location);
            }

            return locations;
        }
    }
}

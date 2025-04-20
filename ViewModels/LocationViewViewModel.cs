using BeautifulWeather.Services.GeoCoder;
using BeautifulWeather.Services.Settings;
using BeautifulWeather.Storages.FavoriteLocations;
using System.Windows.Input;

namespace BeautifulWeather.ViewModels
{
    public class LocationViewViewModel : ViewModelBase
    {
        private readonly GeoCoderService geoCoderService;
        private readonly ISettingsService settingsService;
        private readonly IFavoriteLocationsStorage favoriteLocationsStorage;

        public ICommand SelectLocationCommand { get; set; }
        public ICommand RemoveLocationCommand { get; set; }

        public LocationViewViewModel(GeoCoderService geoCoderService, ISettingsService settingsService, IFavoriteLocationsStorage favoriteLocationsStorage)
        {
            this.geoCoderService = geoCoderService;
            this.settingsService = settingsService;
            this.favoriteLocationsStorage = favoriteLocationsStorage;
            SelectedLocation = settingsService.Settings.SelectedLocation;
            SavedLocations = favoriteLocationsStorage.GetAll();

            if (SavedLocations is null || SavedLocations.Count == 0)
            {
                var location = new GeoLocation
                {
                    Name = "Санкт-Петербург",
                    Latitude = 30.314997F,
                    Longitude = 59.938784F,
                    Description = "Россия"
                };
                SavedLocations = new List<GeoLocation>() { location };
                favoriteLocationsStorage.Add(location);
            }

            SelectLocationCommand = new RelayCommand(SelectLocation, CanSelectLocation);
            RemoveLocationCommand = new RelayCommand(RemoveLocation, CanRemoveLocation);
        }

        private void RemoveLocation(object obj)
        {
            if (obj is GeoLocation location)
            {
                favoriteLocationsStorage.Remove(location);
                SavedLocations = favoriteLocationsStorage.GetAll();
                SelectedLocation = GetLastLocation();
            }
        }

        private GeoLocation GetLastLocation()
        {
            return SavedLocations.LastOrDefault() ?? new GeoLocation
            {
                Name = "Санкт-Петербург",
                Latitude = 30.314997F,
                Longitude = 59.938784F,
                Description = "Россия"
            };
        }

        private bool CanRemoveLocation(object arg)
        {
            return true;
        }

        private void SelectLocation(object obj)
        {
            if (obj is GeoLocation location)
            {
                var settings = settingsService.Settings;
                settings.SelectedLocation = location;
            }
        }

        private bool CanSelectLocation(object arg)
        {
            return true;
        }

        private string locationSearch;
        public string LocationSearch
        {
            get => locationSearch;
            set
            {
                locationSearch = value;
                if (string.IsNullOrEmpty(value))
                {
                    SearchResults = null!;
                    return;
                }
                Task.Run(async () =>
                {
                    string search = value;
                    await Task.Delay(700);
                    if (search != locationSearch) return;
                    SearchResults = geoCoderService.GetLocation(value);
                });
            }
        }


        private List<GeoLocation> searchResults;
        public List<GeoLocation> SearchResults
        {
            get { return searchResults; }
            set
            {
                searchResults = value;
                OnPropertyChanged();
            }
        }

        private List<GeoLocation> savedLocations;
        public List<GeoLocation> SavedLocations
        {
            get { return savedLocations; }
            set
            {
                savedLocations = value;
                OnPropertyChanged();
            }
        }

        private GeoLocation selectedLocation;
        public GeoLocation SelectedLocation
        {
            get
            {
                return selectedLocation;
            }
            set
            {
                selectedLocation = value;
                settingsService.Settings.SelectedLocation = value;
                AddFavoriteLocation(value);

                OnPropertyChanged();
            }
        }

        private void AddFavoriteLocation(GeoLocation value)
        {
            if (value is not null)
            {
                if (favoriteLocationsStorage.GetAll().Contains(value))
                {
                    return;
                }
                else
                {
                    favoriteLocationsStorage.Add(value);
                    SavedLocations = favoriteLocationsStorage.GetAll();
                }
            }
            else
            {
                SelectedLocation = SavedLocations.LastOrDefault() ?? new GeoLocation();
            }
        }
    }
}

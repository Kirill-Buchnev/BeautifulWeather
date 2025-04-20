using BeautifulWeather.Models;
using BeautifulWeather.Services.GeoCoder;
using BeautifulWeather.Services.Localization;
using BeautifulWeather.Services.Settings;
using BeautifulWeather.Storages.FavoriteLocations;
using BeautifulWeather.Storages.Users;
using BeautifulWeather.ViewModels.Auth;
using BeautifulWeather.Views.Auth;
using System.Windows;
using System.Windows.Input;

namespace BeautifulWeather.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IUserStorage _userStorage;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingsService settingsService;
        private readonly GeoCoderService geoCoderService;
        private readonly IFavoriteLocationsStorage favoriteLocationsStorage;

        public ICommand HomeCommand { get; }
        public ICommand LocationCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand SignInCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand RegisterCommand { get; }

        private ViewModelBase selectedContent;
        private readonly HomeViewViewModel homeViewViewModel;
        private readonly AuthorizationWindowViewModel authorizationWindowViewModel;
        private readonly RegistrationWindowViewModel registrationWindowViewModel;

        public ViewModelBase SelectedContent
        {
            get { return selectedContent; }
            set
            {
                selectedContent = value;
                OnPropertyChanged();
            }
        }

        private bool signInButtonIsVisible;

        public bool SignInButtonIsVisible
        {
            get { return signInButtonIsVisible; }
            set {
                signInButtonIsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool registerButtonIsVisible;

        public bool RegisterButtonIsVisible
        {
            get { return registerButtonIsVisible; }
            set
            {
                registerButtonIsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool signOutButtonIsVisible;

        public bool SignOutButtonIsVisible
        {
            get { return signOutButtonIsVisible; }
            set
            {
                signOutButtonIsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool personalDeskIsVisible;

        public bool PersonalDeskIsVisible
        {
            get { return personalDeskIsVisible; }
            set
            {
                personalDeskIsVisible = value;
                OnPropertyChanged();
            }
        }

        private string personalDeskContent;        

        public string PersonalDeskContent
        {
            get { return personalDeskContent; }
            set
            {
                personalDeskContent = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel(IUserStorage userStorage,
            HomeViewViewModel homeViewViewModel,
            AuthorizationWindowViewModel authorizationWindowViewModel,
            RegistrationWindowViewModel registrationWindowViewModel,
            ILocalizationService localizationService,
            ISettingsService settingsService,
            GeoCoderService geoCoderService,
            IFavoriteLocationsStorage favoriteLocationsStorage)
        {
            HomeCommand = new RelayCommand(OpenHomeView, CanOpenHomeView);
            LocationCommand = new RelayCommand(OpenLocationView, CanOpenLocationView);
            SettingsCommand = new RelayCommand(OpenSettingsView, CanOpenSettingsView);
            CloseCommand = new RelayCommand(ApplyCloseCommand, CanApplyCloseCommand);
            SignInCommand = new RelayCommand(SignIn, CanSignIn);
            SignOutCommand = new RelayCommand(SignOut, CanSignOut);
            RegisterCommand = new RelayCommand(Register, CanRegister);

            this.homeViewViewModel = homeViewViewModel;
            this.authorizationWindowViewModel = authorizationWindowViewModel;
            this.registrationWindowViewModel = registrationWindowViewModel;
            this._userStorage = userStorage;
            _localizationService = localizationService;
            this.settingsService = settingsService;
            _localizationService.SetCulture(settingsService.Settings.Culture);
            this.geoCoderService = geoCoderService;
            this.favoriteLocationsStorage = favoriteLocationsStorage;

            SignInButtonIsVisible = true;
            SignOutButtonIsVisible = false;
            RegisterButtonIsVisible = true;
            CheckUser();
        }

        private void Authorized(User user)
        {
            PersonalDeskContent = $"Logged in: {user?.Login}";
            PersonalDeskIsVisible = true;
            SignOutButtonIsVisible = true;
            SignInButtonIsVisible = false;
            RegisterButtonIsVisible = false;
        }

        private void UnAuthorized()
        {
            _userStorage.SignOut();
            PersonalDeskIsVisible = false;
            SignOutButtonIsVisible = false;
            SignInButtonIsVisible = true;
            RegisterButtonIsVisible = true;
        }

        private void CheckUser()
        {
            var user = _userStorage.TryGetSignedInUser();
            if (user != null) Authorized(user);
            else UnAuthorized();
        }

        private bool CanRegister(object arg)
        {
            return true;
        }

        private bool CanSignOut(object arg)
        {
            return true;
        }

        private bool CanSignIn(object arg)
        {
            return true;
        }

        private bool CanApplyCloseCommand(object arg)
        {
            return true;
        }

        private bool CanOpenSettingsView(object arg)
        {
            return true;
        }

        private bool CanOpenLocationView(object arg)
        {
            return true;
        }
        private bool CanOpenHomeView(object arg)

        {
            return true;
        }

        private void Register(object obj)
        {
            var registration = new RegistrationWindow(registrationWindowViewModel);
            registration.ShowDialog();
            CheckUser();
        }

        private void SignOut(object obj)
        {
            UnAuthorized();
        }

        private void SignIn(object obj)
        {
            var authorization = new AuthorizationWindow(authorizationWindowViewModel);
            authorizationWindowViewModel.ProcessAborted = true;
            authorization.ShowDialog();
            if ((authorization.AuthorizationRememberMe_CheckBox.IsChecked ?? false) && !authorizationWindowViewModel.ProcessAborted) Authorized(_userStorage.TryGetSignedInUser());
            else UnAuthorized();
            authorization.AuthorizationRememberMe_CheckBox.IsChecked = false;
        }

        private void ApplyCloseCommand(object obj)
        {
            Application.Current.MainWindow.Close();
        }

        private void OpenSettingsView(object obj)
        {
            SelectedContent = new SettingsViewViewModel(_localizationService, settingsService);
        }

        private void OpenLocationView(object obj)
        {
            SelectedContent = new LocationViewViewModel(geoCoderService, settingsService, favoriteLocationsStorage);
        }

        private void OpenHomeView(object obj)
        {
            homeViewViewModel.TryUpdateWeather();
            SelectedContent = homeViewViewModel;
        }
    }
}

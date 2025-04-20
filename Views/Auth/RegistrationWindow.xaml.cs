using BeautifulWeather.Models;
using BeautifulWeather.Services;
using BeautifulWeather.Storages;
using BeautifulWeather.Storages.Users;
using BeautifulWeather.ViewModels.Auth;
using System.Windows;

namespace BeautifulWeather.Views.Auth
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow(RegistrationWindowViewModel registrationWindowViewModel)
        {
            InitializeComponent();
            DataContext = registrationWindowViewModel;
            Closing += (s, e) => registrationWindowViewModel.OnCloseWindow();
        }
    }
}

using BeautifulWeather.Storages;
using BeautifulWeather.ViewModels.Auth;
using System.Windows;

namespace BeautifulWeather.Views.Auth
{
    /// <summary>
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow(AuthorizationWindowViewModel authorizationWindowViewModel)
        {
            InitializeComponent();
            DataContext = authorizationWindowViewModel;
            Closing += (s, e) => authorizationWindowViewModel.OnCloseWindow();
        }
    }
}

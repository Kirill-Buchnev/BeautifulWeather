using BeautifulWeather.Models;
using BeautifulWeather.Storages.Users;
using System.Windows;
using System.Windows.Input;

namespace BeautifulWeather.ViewModels.Auth
{
    public partial class RegistrationWindowViewModel : ViewModelBase
    {
        private readonly IUserStorage userStorage;

        public ICommand RegistrationCommand { get; }

        private string newLogin;

        public string NewLogin
        {
            get { return newLogin; }
            set
            {
                newLogin = value;
                OnPropertyChanged();
            }
        }

        private string newPassword;

        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                newPassword = value;
                OnPropertyChanged();
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public RegistrationWindowViewModel(IUserStorage userStorage)
        {
            RegistrationCommand = new RelayCommand(TryRegister, CanTryRegister);
            this.userStorage = userStorage;
        }

        private bool CanTryRegister(object arg)
        {
            return true;
        }

        private void TryRegister(object obj)
        {
            if (newLogin.Equals(string.Empty) || newPassword.Equals(string.Empty) || confirmPassword.Equals(string.Empty)) return;
            var inputLogin = NewLogin;
            var inputPassword = NewPassword;
            var confirmInputPassword = ConfirmPassword;

            if (!inputPassword.Equals(confirmInputPassword))
            {
                MessageBox.Show("Пароли не совпадают! Проверьте данные ввода!");
                return;
            }

            if (userStorage.TryGetUserByLogin(inputLogin) != null)
            {
                MessageBox.Show("Аккаунт с таким именем уже зарегистрирован!");
                return;
            }

            var registeredUser = new User(inputLogin, inputPassword, true);
            userStorage.Add(registeredUser);

            MessageBox.Show("Аккаунт успешно зарегистрирован!");
            OnCloseWindow();
            (obj as Window).Close();
        }

        public void OnCloseWindow()
        {
            NewLogin = string.Empty;
            NewPassword = string.Empty;
            ConfirmPassword = string.Empty;
        }
    }
}

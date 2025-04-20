using BeautifulWeather.Storages.Users;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace BeautifulWeather.ViewModels.Auth
{
    public partial class AuthorizationWindowViewModel : ViewModelBase
    {
        private readonly IUserStorage userStorage;

        public ICommand LoginCommand { get; }

        public bool ProcessAborted;

        private string login;

        public string Login
        {
            get { return login; }
            set {
                login = value;
                OnPropertyChanged();
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private bool rememberMe;

        public bool RememberMe
        {
            get { return rememberMe; }
            set
            {
                rememberMe = value;
                OnPropertyChanged();
            }
        }

        public AuthorizationWindowViewModel(IUserStorage userStorage)
        {
            LoginCommand = new RelayCommand(TryLogin, CanTryLogin);
            this.userStorage = userStorage;
        }

        private bool CanTryLogin(object arg)
        {
            return true;
        }

        private void TryLogin(object obj)
        {
            if (login.Equals(string.Empty) || password.Equals(string.Empty)) return;
            var inputLogin = Login;
            var inputPassword = Password;

            if (string.IsNullOrEmpty(inputLogin) || string.IsNullOrEmpty(inputPassword)) return;

            if (!userStorage.CheckLogin(inputLogin))
            {
                MessageBox.Show("Пользователь с таким именем не зарегистрирован!");
                return;
            }

            var user = userStorage.TryGetUserByLogin(inputLogin);

            if (!userStorage.CheckPassword(user, inputPassword))
            {
                MessageBox.Show("Введен неверный пароль!");
                return;
            }

            userStorage.SignIn(user);
            MessageBox.Show("Вход выполнен успешно!");
            ProcessAborted = false;
            (obj as Window).Close();
        }

        public void OnCloseWindow()
        {
            Login = string.Empty;
            Password = string.Empty;
        }
    }
}

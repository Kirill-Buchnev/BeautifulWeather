using BeautifulWeather.Models;

namespace BeautifulWeather.Storages.Users
{
    public interface IUserStorage
    {
        User TryGetSignedInUser();
        void Add(User user);
        void SignIn(User user);
        void SignOut();
        List<User> GetAllUsers();
        bool CheckLogin(string login);
        User TryGetUserByLogin(string login);
        bool CheckPassword(User user, string password);
    }
}

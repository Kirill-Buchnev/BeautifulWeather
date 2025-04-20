using Microsoft.EntityFrameworkCore;

namespace BeautifulWeather.Models
{
    [PrimaryKey("Login", "Password")]
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsSignedIn { get; set; }

        // default constructor is needed for the proper deserialization
        public User() { }

        public User(string inputLogin, string inputPassword, bool isSingedIn)
        {
            Login = inputLogin;
            Password = inputPassword;
            IsSignedIn = isSingedIn;
        }
    }
}
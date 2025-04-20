using BeautifulWeather.Models;

namespace BeautifulWeather.Storages.Users
{
    public class UserStorage : IUserStorage
    {
        private List<User> users { get; set; }
        private readonly DatabaseContext _databaseContext;

        public UserStorage (DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public User TryGetSignedInUser()
        {
            users = GetAllUsers();
            return users.FirstOrDefault(x => x.IsSignedIn);
        }

        public void Add(User user)
        {
            _databaseContext.Users.Add(user);
            _databaseContext.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _databaseContext.Users.ToList() ?? new List<User>();
        }

        public void SignOut()
        {
            var signInUser = TryGetSignedInUser();
            if (signInUser != null)
            {
                users = GetAllUsers();
                var existingUser = users.FirstOrDefault(x => x.Login.Equals(signInUser.Login) && x.Password.Equals(signInUser.Password));
                existingUser.IsSignedIn = false;
                _databaseContext.SaveChanges();
            }
        }

        public bool CheckLogin(string login)
        {
            users = GetAllUsers();
            return users.Any(x => x.Login.Equals(login));
        }

        public User TryGetUserByLogin(string login)
        {
            users = GetAllUsers();
            return users.FirstOrDefault(x => x.Login.Equals(login));
        }

        public bool CheckPassword(User user, string password)
        {
            return user.Password.Equals(password);
        }

        public void SignIn(User user)
        {
            users = GetAllUsers();
            if (user != null)
            {
                var existingUser = users.FirstOrDefault(x => x.Login.Equals(user.Login) && x.Password.Equals(user.Password));
                existingUser.IsSignedIn = true;
                _databaseContext.SaveChanges();
            }
        }
    }
}

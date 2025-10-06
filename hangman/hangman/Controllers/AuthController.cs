using HangmanGameMVC.Models.User;
using HangmanGameMVC.Views;

namespace HangmanGameMVC.Controllers
{
    public class AuthController
    {
        private readonly IUserRepository _userRepo;
        private readonly ConsoleView _view;

        public AuthController(IUserRepository userRepo, ConsoleView view)
        {
            _userRepo = userRepo;
            _view = view;
        }

        public User Login()
        {
            _view.DisplayMessage("=== LOGIN ===");
            string username = _view.ReadInput("Enter username: ");
            string password = _view.ReadInput("Enter password: ");

            var user = _userRepo.GetUser(username, password);
            if (user != null)
            {
                _view.DisplayMessage($"Login successful! Welcome {user.Username} ({user.Role})");
                return user;
            }
            else
            {
                _view.DisplayMessage("Invalid username or password.");
                return null;
            }
        }

        public User Register()
        {
            _view.DisplayMessage("=== REGISTER ===");
            string username = _view.ReadInput("Enter new username: ");
            string password = _view.ReadInput("Enter new password: ");
            string role = "user";

            var newUser = new User(username, password, role);
            if (_userRepo.AddUser(newUser))
            {
                _view.DisplayMessage("Registration successful! You can now login.");
                return newUser;
            }
            else
            {
                _view.DisplayMessage("Username already exists. Try again.");
                return null;
            }
        }
    }
}

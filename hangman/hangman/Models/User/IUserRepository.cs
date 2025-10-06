using System.Collections.Generic;

namespace HangmanGameMVC.Models.User
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);
        bool AddUser(User user);
        List<User> GetAllUsers();
    }
}

using HangmanGameMVC.Utils;
using System.Collections.Generic;
using System.Linq;

namespace HangmanGameMVC.Models.User
{
    public class UserRepository : IUserRepository
    {
        private readonly string _filePath = "Data/users.csv";

        public User GetUser(string username, string password)
        {
            var users = CsvHelper.ReadCsv(_filePath);
            foreach (var u in users)
            {
                if (u[0] == username && u[1] == password)
                    return new User(u[0], u[1], u[2]);
            }
            return null;
        }

        public bool AddUser(User user)
        {
            var users = CsvHelper.ReadCsv(_filePath);
            if (users.Any(u => u[0] == user.Username)) return false;

            users.Add(new string[] { user.Username, user.Password, user.Role });
            CsvHelper.WriteCsv(_filePath, users);
            return true;
        }

        public List<User> GetAllUsers()
        {
            var users = CsvHelper.ReadCsv(_filePath);
            return users.Select(u => new User(u[0], u[1], u[2])).ToList();
        }
    }
}

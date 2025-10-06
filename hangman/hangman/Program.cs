using HangmanGameMVC.Controllers;
using HangmanGameMVC.Models.User;
using HangmanGameMVC.Models.Word;
using HangmanGameMVC.Views;
using System;

namespace HangmanGameMVC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userRepo = new UserRepository();
            var wordRepo = new WordRepository();
            var view = new ConsoleView();
            var authController = new AuthController(userRepo, view);
            var gameController = new GameController(wordRepo, view);

            view.DisplayMessage("=== HANGMAN GAME ===");

            while (true)
            {
                view.DisplayMessage("\n1. Login\n2. Register\n3. Exit");
                string option = view.ReadInput("Select an option: ");

                switch (option)
                {
                    case "1":
                        var user = authController.Login();
                        if (user != null)
                        {
                            

                            // Admin or User
                            if (user.Role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                                gameController.AdminMenu(); // admin menu handles play/add/update/delete
                            else
                                gameController.PlayGameLoop(); // user plays game loop continuously

                            // After admin or user exits, program ends
                            return;
                        }
                        break;

                    case "2":
                        authController.Register();
                        break;

                    case "3":
                        view.DisplayMessage("Exiting program...");
                        return;

                    default:
                        view.DisplayMessage("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}

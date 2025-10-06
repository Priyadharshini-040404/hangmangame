using HangmanGameMVC.Models.Game;
using HangmanGameMVC.Models.User;
using HangmanGameMVC.Models.Word;
using HangmanGameMVC.Views;
using System;

namespace HangmanGameMVC.Controllers
{
    public class GameController
    {
        private readonly IWordRepository _wordRepo;
        private readonly ConsoleView _view;

        public GameController(IWordRepository wordRepo, ConsoleView view)
        {
            _wordRepo = wordRepo;
            _view = view;
        }

        public void AdminMenu()
        {
            bool exit = false;
            while (!exit)
            {
                _view.DisplayMessage("\n=== ADMIN MENU ===");
                _view.DisplayMessage("1. Play Game\n2. Add Word\n3. Update Word\n4. Delete Word\n5. Exit");
                string choice = _view.ReadInput("Select an option: ");

                switch (choice)
                {
                    case "1":
                        PlayGameLoop(); // modified to keep playing multiple words
                        break;
                    case "2":
                        string newWord = _view.ReadInput("Enter new word to add: ");
                        _wordRepo.AddWord(new Word(newWord));
                        _view.DisplayMessage("Word added successfully!");
                        break;
                    case "3":
                        string oldWord = _view.ReadInput("Enter word to update: ");
                        string updatedWord = _view.ReadInput("Enter new word: ");
                        _wordRepo.UpdateWord(oldWord, updatedWord);
                        _view.DisplayMessage("Word updated successfully!");
                        break;
                    case "4":
                        string deleteWord = _view.ReadInput("Enter word to delete: ");
                        _wordRepo.DeleteWord(deleteWord);
                        _view.DisplayMessage("Word deleted successfully!");
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        _view.DisplayMessage("Invalid option. Try again.");
                        break;
                }
            }
        }

        // New method to allow repeated play after login or admin
        public void PlayGameLoop()
        {
            bool playAgain = true;

            while (playAgain)
            {
                PlayGame(); // play single word

                // Ask if user wants to play next word
                string choice = _view.ReadInput("\nDo you want to play again? (y/n): ").Trim().ToLower();
                if (choice != "y")
                    playAgain = false;
                else
                    Console.Clear();
            }

            _view.DisplayMessage("\nThanks for playing! Exiting program...");
            Environment.Exit(0);
        }

        // Single word game logic
        public void PlayGame()
        {
            var wordObj = _wordRepo.GetRandomWord();
            if (wordObj == null)
            {
                _view.DisplayMessage("No words available. Please ask admin to add words.");
                return;
            }

            var gameLogic = new HangmanLogic(wordObj.Text);
            gameLogic.StartGame();

            while (!gameLogic.IsGameOver())
            {
                _view.DisplayMessage($"\nWord: {gameLogic.GetProgress()}");
                _view.DisplayMessage($"Attempts left: {gameLogic.GetAttempts()}");
                _view.DisplayMessage(gameLogic.GetHangmanUI());

                string input = _view.ReadInput("Guess a letter: ");
                if (string.IsNullOrEmpty(input)) continue;

                bool correct = gameLogic.Guess(input[0]);
                if (correct)
                    _view.DisplayMessage("Correct!");
                else
                    _view.DisplayMessage("Wrong!");
            }

            if (gameLogic.IsWordCompleted())
                _view.DisplayMessage("\nCONGRATULATIONS! Word guessed successfully!");
            else
                _view.DisplayMessage($"\nGAME OVER! You failed to guess the word. The word was: {wordObj.Text}");
        }
    }
}

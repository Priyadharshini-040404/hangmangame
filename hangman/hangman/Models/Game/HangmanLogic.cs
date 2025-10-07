using System;
using System.Collections.Generic; // ✅ Needed for HashSet
using System.Linq;

namespace HangmanGameMVC.Models.Game
{
    public class HangmanLogic : IGameLogic
    {
        private GameState _state;
        private bool _askPlayAgain;
        private HashSet<char> _guessedLetters = new HashSet<char>(); // ✅ Track guessed letters

        public HangmanLogic(string word)
        {
            _state = new GameState(word.ToUpper());
        }

        public void StartGame()
        {
            _state.LetterIndex = 0;
            _state.AttemptCount = 3;
            _state.GameOver = false;
            _state.WordCompleted = false;
            _askPlayAgain = false;
            _guessedLetters.Clear(); // ✅ Reset guessed letters each round

            for (int i = 0; i < _state.CurrentProgress.Length; i++)
                _state.CurrentProgress[i] = '_';
        }

        public bool Guess(char letter)
        {
            if (_state.GameOver) return false;

            letter = char.ToUpper(letter);
            bool correct = false;

            // ✅ Check if letter was already guessed (correct or wrong)
            if (_guessedLetters.Contains(letter))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYou already guessed '{letter}'. Try a different letter!");
                Console.ResetColor();
                return false; // Do NOT change attempts or progress
            }

            // ✅ Record the guessed letter
            _guessedLetters.Add(letter);

            // Fill all occurrences of guessed letter
            for (int i = 0; i < _state.WordToGuess.Length; i++)
            {
                if (_state.WordToGuess[i] == letter && _state.CurrentProgress[i] != letter)
                {
                    _state.CurrentProgress[i] = letter;
                    correct = true;
                }
            }

            if (correct)
            {
                _state.AttemptCount = 3; // reset attempts for next letter

                // Word completed
                if (!_state.CurrentProgress.Contains('_'))
                {
                    _state.WordCompleted = true;
                    _state.GameOver = true;
                    _askPlayAgain = true;

                    // Show happy human
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nCongratulations! You guessed the word correctly!");
                    Console.WriteLine(@"
     \O/
      |
     / \
  H A P P Y !");
                    Console.ResetColor();
                }
            }
            else
            {
                _state.AttemptCount--;

                if (_state.AttemptCount <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Set red color
                    Console.WriteLine(GetHangmanUI());
                    Console.WriteLine("     YOU ARE HANGED!");
                    Console.ResetColor(); // Reset back to default color

                    _state.GameOver = true;
                    _askPlayAgain = true;

                    // Reset word progress for next round
                    for (int i = 0; i < _state.CurrentProgress.Length; i++)
                        _state.CurrentProgress[i] = '_';
                    _state.LetterIndex = 0;
                    _state.AttemptCount = 3;
                }
            }

            return correct;
        }

        public bool ShouldAskPlayAgain() => _askPlayAgain;

        public void AskPlayAgainPrompt()
        {
            Console.Write("\nDo you want to play again? (y/n): ");
            string choice = Console.ReadLine()?.Trim().ToLower() ?? "n";

            if (choice == "y")
            {
                Console.Clear();
                Console.WriteLine("Starting a new round...");
                StartGame();
            }
            else
            {
                Console.WriteLine("\nThanks for playing! Exiting...");
                Environment.Exit(0);
            }
        }

        public string GetHangmanUI()
        {
            int wrongGuess = 3 - _state.AttemptCount;

            return wrongGuess switch
            {
                1 => @"
  +---+
  |   |
  |
  |
  |
  |
=========",
                2 => @"
  +---+
  |   |
  |   O
  |
  |
  |
=========",
                3 => @"
  +---+
  |   |
  |   O
  |  /|\
  |  / \
  |
=========",
                _ => ""
            };
        }

        public string GetProgress() => string.Join(" ", _state.CurrentProgress);

        public int GetAttempts() => _state.AttemptCount;

        public bool IsGameOver() => _state.GameOver;

        public bool IsWordCompleted() => _state.WordCompleted;
    }
}

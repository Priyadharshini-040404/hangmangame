using System.Collections.Generic;

namespace HangmanGameMVC.Models.Game
{
    public class GameState
    {
        public string WordToGuess { get; set; }
        public char[] CurrentProgress { get; set; }
        public int LetterIndex { get; set; }
        public int AttemptCount { get; set; } = 3; // max 3 attempts per letter
        public bool GameOver { get; set; } = false;
        public bool WordCompleted { get; set; } = false;

        public GameState(string word)
        {
            WordToGuess = word.ToUpper();
            CurrentProgress = new char[word.Length];
            for (int i = 0; i < CurrentProgress.Length; i++)
                CurrentProgress[i] = '_';
            LetterIndex = 0;
        }
    }
}


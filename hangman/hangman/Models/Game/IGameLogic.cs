namespace HangmanGameMVC.Models.Game
{
    public interface IGameLogic
    {
        void StartGame();
        bool Guess(char letter);
        string GetHangmanUI();
    }
}

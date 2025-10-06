using System;

namespace HangmanGameMVC.Views
{
    public class ConsoleView
    {
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}

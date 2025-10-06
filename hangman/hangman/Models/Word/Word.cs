namespace HangmanGameMVC.Models.Word
{
    public class Word
    {
        public string Text { get; set; }

        public Word() { }

        public Word(string text)
        {
            Text = text;
        }
    }
}

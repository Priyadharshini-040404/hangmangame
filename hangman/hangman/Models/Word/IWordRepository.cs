using System.Collections.Generic;

namespace HangmanGameMVC.Models.Word
{
    public interface IWordRepository
    {
        List<Word> GetAllWords();
        void AddWord(Word word);
        void UpdateWord(string oldWord, string newWord);
        void DeleteWord(string word);
        Word GetRandomWord();
    }
}

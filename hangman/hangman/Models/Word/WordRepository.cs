using HangmanGameMVC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HangmanGameMVC.Models.Word
{
    public class WordRepository : IWordRepository
    {
        private readonly string _filePath = "Data/words.csv";

        public List<Word> GetAllWords()
        {
            var lines = CsvHelper.ReadCsv(_filePath);
            return lines.Select(l => new Word(l[0])).ToList();
        }

        public void AddWord(Word word)
        {
            var words = CsvHelper.ReadCsv(_filePath);
            if (!words.Any(w => w[0] == word.Text))
            {
                words.Add(new string[] { word.Text });
                CsvHelper.WriteCsv(_filePath, words);
            }
        }

        public void UpdateWord(string oldWord, string newWord)
        {
            var words = CsvHelper.ReadCsv(_filePath);
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i][0] == oldWord)
                {
                    words[i][0] = newWord;
                    break;
                }
            }
            CsvHelper.WriteCsv(_filePath, words);
        }

        public void DeleteWord(string word)
        {
            var words = CsvHelper.ReadCsv(_filePath);
            words.RemoveAll(w => w[0] == word);
            CsvHelper.WriteCsv(_filePath, words);
        }

        public Word GetRandomWord()
        {
            var words = GetAllWords();
            if (words.Count == 0) return null;
            Random rnd = new Random();
            return words[rnd.Next(words.Count)];
        }
    }
}

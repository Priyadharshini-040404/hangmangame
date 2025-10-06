using System;
using System.Collections.Generic;
using System.IO;
namespace HangmanGameMVC.Utils { 
    public static class CsvHelper
    { public static List<string[]> ReadCsv(string path) 
        { var lines = new List<string[]>(); 
            if (!File.Exists(path)) return lines;
            foreach (var line in File.ReadAllLines(path)) {
                lines.Add(line.Split(','));
            }
            return lines;
        }
        public static void WriteCsv(string path, List<string[]> data) 
        {
            var lines = new List<string>();
            foreach (var row in data)
            { 
                lines.Add(string.Join(",", row));
            } File.WriteAllLines(path, lines); 
        } 
    } 
}
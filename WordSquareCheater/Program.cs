using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordSquareCheater
{
    class Program
    {
        private static int compareLengths(Word x, Word y)
        {
            return x.word.Length.CompareTo(y.word.Length);
        }

        private static int compareScore(Word x, Word y)
        {
            return x.score - y.score;
        }

        static void printResults(List<Word> results)
        {
            foreach (Word w in results)
            {
                System.Console.WriteLine(w.word + " " + w.score);
                System.Console.WriteLine(w.input);
            }
        }


        static void Main(string[] args)
        {
            Words dict = new Words();
            StreamReader d = new StreamReader("dictionary.txt");
            string word = d.ReadLine();
            while (word != null)
            {
                dict.add(word.ToUpper());
                word = d.ReadLine();
            }
            while (true)
            {
                System.Console.WriteLine("Enter a 16 letter square!");
                string square = System.Console.ReadLine();
                Square s = new Square(square.ToUpper(), dict);
                List<Word> longestWords = s.longestWords();
                longestWords.Sort(compareScore);
                printResults(longestWords);
            }
        }
    }
}

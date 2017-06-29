﻿using System;
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

        static void printResults(List<Word> results, int perLine, int width)
        {
            int min = 0;
            while (results.Count > min)
            {
                if (results.Count - min < perLine)
                {
                    perLine = results.Count - min;
                }
                string words = "";
                string scores = "";
                string[] pictures = new string[4];
                for (int i = 0; i < 4; i++)
                {
                    pictures[i] = "";
                }
                for (int i = 0; i < perLine; i++)
                {
                    words += results[i + min].word;
                    while (words.Length < width / perLine * (i+1))
                    {
                        words += " ";
                    }

                    scores += results[i + min].score;
                    while (scores.Length < width / perLine * (i+1))
                    {
                        scores += " ";
                    }

                    for (int j = 0; j < 4; j++)
                    {
                        pictures[j] += results[i + min].input[j];
                        while (pictures[j].Length < width / perLine * (i+1))
                        {
                            pictures[j] += " ";
                        }
                    }
                }
                System.Console.WriteLine(words);
                System.Console.WriteLine(scores);
                for (int i = 0; i < 4; i++)
                {
                    System.Console.WriteLine(pictures[i]);
                }

                min += perLine;
                System.Console.WriteLine();
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
                printResults(longestWords, 10, 119);
            }
        }
    }
}

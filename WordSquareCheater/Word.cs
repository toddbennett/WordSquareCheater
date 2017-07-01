using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSquareCheater
{
    
    struct Word
    {
        public string word;
        public int score;
        public int[] input;

        static int[] scores = { 1, 4, 4, 2, 1, 4, 3, 3, 1, 10, 5, 2, 4, 2, 1, 4, 10, 1, 1, 1, 2, 5, 4, 8, 3, 10 };

        public void updateScore(Bonus[] bonuses)
        {
            score = 0;
            int multi = 1;
            for (int i = 0; i < word.Length; i++)
            {
                switch (bonuses[input[i]])
                {
                    case Bonus.tl:
                        score += scores[word[i] - 65] * 3;
                        break;
                    case Bonus.dl:
                        score += scores[word[i] - 65] * 2;
                        break;
                    case Bonus.tw:
                        multi *= 3;
                        score += scores[word[i] - 65];
                        break;
                    case Bonus.dw:
                        multi *= 2;
                        score += scores[word[i] - 65];
                        break;
                    default:
                        score += scores[word[i] - 65];
                        break;
                }
            }
            score *= multi;
        }

        public Word(string inWord, Stack<int> inInput)
        {
            word = inWord;
            score = 0;

            foreach (char c in inWord)
            {
                score += scores[c - 65];
            }

            input = new int[inInput.Count];
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = inInput.ElementAt(input.Length - i - 1);
            }
        }

        public string[] getFormattedInput()
        {
            char[] temp = "................".ToCharArray();

            for (int i = 1; i < input.Length-1; i++)
            {
                temp[input[i]] = 'x';
            }
            temp[input[0]] = 'O';
            temp[input[input.Length - 1]] = 'X';

            string[] s = new string[4];
            for (int i = 0; i < 4; i++)
            {
                s[i] = "";

                for (int j = 0; j < 4; j++)
                {
                    s[i] += temp[i * 4 + j];
                }
            }

            return s;
        }
    }
}

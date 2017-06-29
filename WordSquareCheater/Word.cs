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
        public string[] input;

        static int[] scores = { 1, 4, 4, 2, 1, 4, 3, 3, 1, 10, 5, 2, 4, 2, 1, 4, 10, 1, 1, 1, 2, 5, 4, 8, 3 };

        public Word(string inWord, Stack<int> inInput)
        {
            word = inWord;
            score = 0;
            foreach (char c in inWord)
            {
                score += scores[c - 65];
            }
            char[] inputTemp = "................".ToCharArray();
            int final = 0;
            int start = inInput.Peek();
            foreach (int i in inInput)
            {
                final = i;
                inputTemp[i] = 'x';
            }
            inputTemp[final] = 'O';
            inputTemp[start] = 'X';

            input = new string[4];
            for (int i = 0; i < 4; i++)
            {
                input[i] = "";

                for (int j = 0; j < 4; j++)
                {
                    input[i] += inputTemp[i * 4 + j];
                }
            }
        }
    }
}

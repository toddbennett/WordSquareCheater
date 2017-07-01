using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WordSquareCheater
{
    enum Bonus
    {
        none,
        dl,
        tl,
        dw,
        tw
    };

    class Square
    {
        private string letters;
        private Words dict;
        private Bonus[] bonuses;

        public Square(string inLetters, Words inDict)
        {
            letters = inLetters;
            dict = inDict;

            bonuses = new Bonus[16];
            for (int i = 0; i < 16; i++)
            {
                bonuses[i] = Bonus.none;
            }
        }

        public List<Word> updateBonuses(Bonus b, int cell, List<Word> words)
        {
            bonuses[cell] = b;
            for (int i = 0; i < words.Count; i++)
            {
                Word w = words[i];
                w.updateScore(bonuses);
                words[i] = w;

            }
            return words;
        }


        // this is useful for navigating the board.
        // returns -1 if no more cells with current parent, need to backtrack
        private int nextCell(int last, int parent)
        {
            if (parent == -1)
            {
                if (last < 15)
                {
                    return last + 1;
                }
                else
                {
                    return -1;
                }
            }
            if (parent > 3)
            {
                // up-left
                if (last < parent - 5 && parent % 4 > 0)
                {
                    return parent - 5;
                }

                // up
                if (last < parent - 4)
                {
                    return parent - 4;
                }

                // up-right
                if (last < parent - 3 && parent % 4 < 3)
                {
                    return parent - 3;
                }
            }

            // left
            if (last < parent - 1 && parent % 4 > 0)
            {
                return parent - 1;
            }

            // right
            if (last < parent + 1 && parent % 4 < 3)
            {
                return parent + 1;
            }

            if (parent < 12)
            {
                // down-left
                if (last < parent + 3 && parent % 4 > 0)
                {
                    return parent + 3;
                }

                // down
                if (last < parent + 4)
                {
                    return parent + 4;
                }

                // down-right
                if (last < parent + 5 && parent % 4 < 3)
                {
                    return parent + 5;
                }
            }

            // none left
            return -1;
        }

        public List<Word> longestWords()
        {
            List<string> winners = new List<string>();
            List<Word> winWord = new List<Word>();

            Stack<int> possible = new Stack<int>();

            possible.Push(0);

            int last = 0;
            int parent = -1;
            int next = 0;

            while (true)
            {

                string testWord = "";
                foreach (int i in possible)
                {
                    testWord = letters[i] + testWord;
                }
                int result = dict.query(testWord);
                if (result == 2)
                {
                    winners.Add(testWord);
                    Word w = new Word(testWord, possible);
                    winWord.Add(w);
                }

                // try to go deeper
                if (result != 0)
                {
                    parent = possible.Peek();
                    next = nextCell(0, parent);
                    while (possible.Contains(next))
                    {
                        next = nextCell(next, parent);
                    }
                    if (!possible.Contains(next) && next != -1)
                    {
                        possible.Push(next);
                        continue;
                    }
                }

                // have to backtrack and try next branch

                do
                {
                    if (!possible.Any())
                    {
                        return winWord;
                    }
                    last = possible.Pop();
                    if (possible.Any())
                    {
                        parent = possible.Peek();
                    }
                    else
                    {
                        parent = -1;
                    }
                    next = nextCell(last, parent);
                } while (next == -1 || possible.Contains(next));

                if (!possible.Contains(next))
                {
                    possible.Push(next);
                    continue;
                }
            }
            

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSquareCheater
{
    class Words
    {
        public Words()
        {
            next = new Words[27];
            for (int i = 0; i < 27; i++)
            {
                next[i] = null;
            }
        }

        public Words(string word)
        {
            next = new Words[27];
            for (int i = 0; i < 27; i++)
            {
                next[i] = null;
            }
            add(word);
        }

        public void add(string word)
        {
            while (word.Any() && (word[0] < 65 || word[0] > 90))
            {
                word = word.Substring(1);
            }
            if (word.Any())
            {
                if (next[word[0] - 65] != null)
                {
                    next[word[0] - 65].add(word.Substring(1));
                }
                else
                {
                    next[word[0] - 65] = new Words(word.Substring(1));
                }
            }
            else
            {
                next[26] = new Words();
            }
        }

        public int query(string word)
        {
            if (word.Any())
            {
                Words n = next[word[0] - 65];
                if (n != null) {
                    return n.query(word.Substring(1));
                }
                return 0;
            }
            else
            {
                if (next[26] != null)
                {
                    return 2;
                }
                return 1;
            }
        }

        private Words[] next;
    }
}

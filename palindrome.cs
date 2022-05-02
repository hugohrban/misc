using System;
using System.Collections.Generic;

namespace palindrom
{
    class Program
    {
        static Dictionary<string, int> checkedWords = new();

        static bool IsPalindrome(string word)
        {
            if (word.Length == 1 || word.Length == 0)
                return true;

            if (word[0] == word[^1])
                return IsPalindrome(word[1..^1]);

            return false;
        }


        static bool AllLettersUnique(string word)
        {
            HashSet<char> letters = new();
            foreach (char c in word)
            {
                if (letters.Contains(c))
                    return false;
                letters.Add(c);
            }
            return true;
        }


        static int CheckWord(string word, int loc_max = 0)
        {
            if (word.Length == 0)
                return loc_max;

            if (AllLettersUnique(word))
                return loc_max + 1;

            char first = word[0];

            if (!word[1..].Contains(first))
                return CheckWord(word[1..], loc_max);

            for (int i = word.Length - 1; i > 0; i--)
            {
                int val;
                if (word[i] == first)
                {
                    if (checkedWords.ContainsKey(word[1..]))
                        val = checkedWords[word[1..]];
                    else
                    {
                        val = CheckWord(word[1..], loc_max);
                        checkedWords.Add(word[1..], val);
                    }
                return Math.Max(CheckWord(word[1..i], loc_max + 2), val);
                }
            }

            return loc_max;
        }


        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int max = 0;

            for (int i = 0; i < input.Length; i++)
                max = Math.Max(max, CheckWord(input[i..]));
            
            Console.WriteLine(max);
        }
    }
}

//outputs longest subsequence that is a palindrome from a given string

using System;

namespace palindrom
{
    class Program
    {
        public static string Best(string word)
        {
            int[,] best = new int[word.Length + 1, word.Length];
            string[,] bestPal = new string[word.Length + 1, word.Length];

            for (int i = 0; i < word.Length; i++)
            {
                best[0, i] = 0;
                best[1, i] = 1;
                bestPal[0, i] = "";
                bestPal[1, i] = word[i].ToString();
            }

            for (int len = 2; len <= word.Length; len++)
            {
                for (int start = 0; start <= word.Length - len; start++)
                {
                    int end = start + len - 1;
                    if (word[start] == word[end])
                    {
                        best[len, start] = best[len - 2, start + 1] + 2;
                        bestPal[len, start] = word[start].ToString() + bestPal[len - 2, start + 1] + word[end].ToString();
                    }
                    else
                    {
                        best[len, start] = Math.Max(best[len - 1, start], best[len - 1, start + 1]);
                        //(best[len - 1, start] < best[len - 1, start + 1]) ? bestPal[len, start] = bestPal[len - 1, start + 1] : bestPal[len, start] = bestPal[len - 1, start];

                        if (best[len - 1, start] < best[len - 1, start + 1])
                        {
                            bestPal[len, start] = bestPal[len - 1, start + 1];
                        }
                        else
                        {
                            bestPal[len, start] = bestPal[len - 1, start];
                        }
                    }
                }
            }
            return bestPal[word.Length, 0];
        }

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            var output = Best(input);
            Console.WriteLine($"{output.Length} {output}");
        }
    }
}

// Given a length of a key and maximum possible height of the key's pins,
// the program calculates how many possible keys there are such that two neighboring pin's heights differ by at most 1.

using System;
using System.Collections.Generic;

namespace keyHole
{
    class Program
    {
        static Dictionary<(int, int), int> memoized = new Dictionary<(int, int), int>();

        static int subkeysCount(int keyLength, int pinHeight, int firstPinHeight)
        {
            // returns number of keys of length `keyLength`, where first pin has height `firstPinHeight`
            // works recursively with help of memoization

            if (keyLength == 1)
            {
                memoized[(1, firstPinHeight)] = 1;
                return 1;
            }

            int temp;
            int sum = 0;

            for (int i = firstPinHeight - 1; i <= firstPinHeight + 1; i++)
            {
                if (i <= 0 || i > pinHeight)
                    continue;

                if (memoized.ContainsKey((keyLength - 1, i)))
                    temp = memoized[(keyLength - 1, i)];

                else
                {
                    temp = subkeysCount(keyLength - 1, pinHeight, i);
                    memoized[(keyLength - 1, i)] = temp;
                }

                sum += temp;
            }

            return sum;
        }

        static int keysCount(int keyLength, int pinHeight)
        {
            // returns sum through all possible pin heights

            int sum = 0;
            for (int i = 1; i <= pinHeight; i++)
            {
                sum += subkeysCount(keyLength - 1, pinHeight, i);
            }

            return sum;
        }

        static void Main(string[] args)
        {
            int keyLength = int.Parse(Console.ReadLine());
            int pinHeight = int.Parse(Console.ReadLine());
            Console.WriteLine(keysCount(keyLength + 1, pinHeight));
        }
    }
}

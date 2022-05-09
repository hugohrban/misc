// outputs names of all declared classes in given C# source code

using System;
using System.Collections.Generic;

namespace vypis_trid
{
    class Program
    {

        static string[] ReadByLine()
        {
            var line = Console.ReadLine();
            var words = line.Split();
            bool insideString = false;
            bool insideMultilineString = false;
            bool insideMultilineComment = false;
            bool returnNextWord = false;
            string word;
            char previous;
            int indexOfBraces;



            List<string> outputs = new List<string>();


            while (line != null)
            {
                foreach (string _ in words)
                {
                    word = _.Trim();

                    if (word == "")
                    {
                        continue;
                    }

                    if (returnNextWord)
                    {
                        if (word.Contains('{'))
                        {
                            indexOfBraces = word.IndexOf('{');
                            outputs.Add(word.Remove(indexOfBraces));
                        }

                        else
                        {
                            outputs.Add(word);
                        }

                        returnNextWord = false;
                        continue;
                    }

                    if (word.Contains("//") && !insideString && !insideMultilineString)
                    {
                        if (insideMultilineComment)
                            continue;
                        else
                            break;
                    }
                    
                    if (word.Contains('"') && !insideMultilineComment)
                    {
                        previous = word[0];
                        foreach (char c in word)
                        {
                            if (c == '"' && previous != '\\')
                            {
                                insideString = !insideString;
                            }
                            previous = c;
                        }


                    }


                    if (word.Contains("/*") && !insideMultilineComment && !insideString && !insideMultilineString)
                    {
                        insideMultilineComment = true;
                        //if (word.Contains("*/"))
                        //    insideMultilineComment = false;                       
                        continue;
                    }

                    if (word.Contains("*/") && insideMultilineComment && !insideString && !insideMultilineString)
                    {
                        insideMultilineComment = false;
                    }

                    if ((word == "class" || word.EndsWith("*/class") || word.EndsWith(";class") || word.EndsWith("}class")) && !insideMultilineComment && !insideString && !insideMultilineString)
                    {
                        returnNextWord = true;
                        continue;
                    }


                }

                line = Console.ReadLine();
                if (line != null)
                {
                    words = line.Split(' ');
                }
                else
                    break;

            }

            return outputs.ToArray();
        }
        
        static void Main(string[] args)
        {
            var output = ReadByLine();
            foreach (string s in output)
            {
                Console.WriteLine(s);
            }
        }
    }
}

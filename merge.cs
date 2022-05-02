using System;
using System.IO;

namespace slevani_souboru
{
    class Citacka
    {
        public StreamReader s1 = new StreamReader("A1.TXT");
        public StreamReader s2 = new StreamReader("A2.TXT");
        private bool EOF_A1 = false;
        private bool EOF_A2 = false;

        public Citacka() { }


        public int ReadNextInt(StreamReader s)
        {
            int num = 0;
            int i = s.Read();
            char c = (char)i;
            int sgn = 1;

            while (c != ' ')
            {
                if (c == '-')
                {
                    sgn = -1;
                }

                else
                {

                    if (i == -1)
                        if (s == s1)
                        {
                            EOF_A1 = true;
                            return sgn * num;
                        }
                        else
                        {
                            EOF_A2 = true;
                            return sgn * num;
                        }
                    else
                    {
                        num *= 10;
                        num += int.Parse(c.ToString());
                    }
                }

                i = s.Read();
                c = (char)i;
            }
            return sgn * num;
        }

        public void Merge()
        {
            int num1 = ReadNextInt(s1);
            int num2 = ReadNextInt(s2);
            using (StreamWriter sw = new StreamWriter("B.TXT"))
            {
                while (true)
                {
                    if (EOF_A1)
                    {
                        sw.Write(Math.Min(num1, num2).ToString() + " ");
                        sw.Write(Math.Max(num1, num2).ToString() + " ");
                        sw.Write(s2.ReadLine());
                        break;
                    }

                    else if (EOF_A2)
                    {
                        sw.Write(Math.Min(num1, num2).ToString() + " ");
                        sw.Write(Math.Max(num1, num2).ToString() + " ");
                        sw.Write(s1.ReadLine());
                        break;
                    }

                    if (num1 < num2)
                    {
                        sw.Write(num1.ToString() + " ");
                        num1 = ReadNextInt(s1);
                    }
                    else
                    {
                        sw.Write(num2.ToString() + " ");
                        num2 = ReadNextInt(s2);
                    }
                }
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Citacka c = new Citacka();
            c.Merge();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Postovni_prepazky
{
    class Zakaznik
    {
        public enum StavZakaznika { PredPostou, VoFronte, PriPrepazke, Vybaveny};
        public int casPrichodu;
        public int casPriPrepazke;
        public int casOdchodu = 0;
        public StavZakaznika stav;

        public Zakaznik(int casPrichodu, int casPriPrepazke)
        {
            this.casPrichodu = casPrichodu;
            this.casPriPrepazke = casPriPrepazke;
            stav = StavZakaznika.PredPostou;
        }  
    }



    class Posta
    {
        public int cas;
        public List<Zakaznik> fronta = new List<Zakaznik>();
        private int head = 0;
        private int tail = 1;
        public bool koniec = false;
        private bool poslednyZakaznikPriPrepazke = false;

        public Posta(List<Zakaznik> zoznam)
        {
            fronta = zoznam;
            cas = fronta[0].casPrichodu - 1;
            //fronta[0].stav = Zakaznik.StavZakaznika.VoFronte;
            
            poslednyZakaznikPriPrepazke = false;
            
        }

        public void Enqueue()
        {
            if (tail == fronta.Count)
                return;
            fronta[tail].stav = Zakaznik.StavZakaznika.VoFronte;
            tail++;
            //if (tail == fronta.Count)
            //    return;
        }

        public int Dequeue()
        {
            if (head == fronta.Count)
            {
                poslednyZakaznikPriPrepazke = true;
                return -1;
            }

            if (head >= tail)
                return -1;

            fronta[head].stav = Zakaznik.StavZakaznika.PriPrepazke;
            head++;
            return head - 1;
        }

        

        

        private int[] Prepazky = { -1, -1, -1 };
        private int[] PrazdnePrepazky = { -1, -1, -1 };
        
        public void ZavolatKuPrepazke()
        {
            if (head < tail)
            {
                for (int i = 0; i < Prepazky.Length; i++)
                {
                    if (Prepazky[i] == -1)
                    {
                        int zak = Dequeue();
                        if (zak == -1)
                            return;
                        Prepazky[i] = zak;
                        fronta[zak].casOdchodu = cas + fronta[zak].casPriPrepazke;
                        continue; //FIXME not sure if this works
                    }
                }
            }
        }

        

        public void IncrementTime()
        {
            cas++;


            for (int i = 0; i < Prepazky.Length; i++)       //niektori ludia su hotovi pri prepazkach
            {
                if (Prepazky[i] != -1)
                {
                    if (fronta[Prepazky[i]].casOdchodu == cas)
                    {
                        fronta[Prepazky[i]].stav = Zakaznik.StavZakaznika.Vybaveny;
                        Prepazky[i] = -1;
                    }
                }
            }



            int t = tail;           // ludia vojdu do posty a zaradia sa do fronty
            while (t <= fronta.Count - 1 && cas == fronta[t].casPrichodu)
            {
                t++;
                Enqueue();
            }



            //ak su vsetci vybaveni, zatvarame postu
            //if (head == fronta.Count && Prepazky.Equals(PrazdnePrepazky))
            if (head == fronta.Count && Enumerable.SequenceEqual(Prepazky, PrazdnePrepazky))
            {
                koniec = true;
                return;
            }

            if (!poslednyZakaznikPriPrepazke)
                ZavolatKuPrepazke();    //ludi z fronty (ak tam nejaki su) zavolame k volnym prepazkam
        }

        public string Vysledok()
        {
            int casZavretia = 0;
            float sumOfWaitTimes = 0;
            float avgWaitTime;
            foreach (Zakaznik z in fronta)
            {
                casZavretia = Math.Max(z.casOdchodu, casZavretia);
                sumOfWaitTimes += z.casOdchodu - z.casPriPrepazke - z.casPrichodu;
            }
            avgWaitTime = sumOfWaitTimes / fronta.Count;
            
            return casZavretia.ToString() + " " + Math.Round(avgWaitTime).ToString();
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] ludia = File.ReadAllLines(@"LIDI.TXT");
            List<Zakaznik> zakaznici = new();

            foreach (string zakaznik in ludia)
            {
                var s = zakaznik.Split();
                zakaznici.Add(new Zakaznik(int.Parse(s[0]), int.Parse(s[1])));
            }

            Posta p = new Posta(zakaznici);

            while (p.koniec == false)
                p.IncrementTime();

            Console.WriteLine(p.Vysledok());
        }
    }
}

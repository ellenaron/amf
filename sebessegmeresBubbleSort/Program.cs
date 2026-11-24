using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sebessegmeresBubbleSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] meretek = { 10, 50, 100, 1000, 10000, 50000 };
            const int meresekSzama = 3;

            Random random = new Random();
            Stopwatch stopper = new Stopwatch();

            Console.WriteLine("Buborékrendezés sebességmérése");
            Console.WriteLine("--------------------------------");

            foreach (int meret in meretek)
            {
                Console.WriteLine();
                Console.WriteLine($"Elemek száma: {meret}");

                long idoOsszeg = 0;
                long osszehasonlitasok = 0;

                for (int meres = 1; meres <= meresekSzama; meres++)
                {
                    // 1. Új, rendezetlen tömb létrehozása és feltöltése (NEM része a mérésnek)
                    int[] szamok = new int[meret];
                    for (int i = 0; i < szamok.Length; i++)
                    {
                        szamok[i] = random.Next(0, 100000); // 0 – 99 999
                    }

                    // 2. Csak a rendezés idejét mérjük
                    stopper.Reset();
                    stopper.Start();
                    osszehasonlitasok = BuborekRendezes(szamok);
                    stopper.Stop();

                    long ido = stopper.ElapsedMilliseconds;
                    idoOsszeg += ido;

                    Console.WriteLine($"{meres}. mérés: {ido} ms");
                }

                double atlag = (double)idoOsszeg / meresekSzama;
                Console.WriteLine($"Átlagos rendezési idő: {atlag:F1} ms");
                Console.WriteLine($"Összehasonlítások száma: {osszehasonlitasok:N0}");
            }

            Console.WriteLine();
            Console.WriteLine("Nyomj meg egy gombot a kilépéshez...");
            Console.ReadKey();
        }

        /// <summary>
        /// Növekvő sorrendbe rendezi a tömböt buborékrendezéssel.
        /// Visszaadja a végrehajtott összehasonlítások számát.
        /// </summary>
        static long BuborekRendezes(int[] tomb)
        {
            long osszehasonlitasok = 0;
            int n = tomb.Length;

            for (int kor = 0; kor < n - 1; kor++)
            {
                // Minden kör végére a legnagyobb megmaradt elem a helyére kerül,
                // ezért a belső ciklus egyre rövidebb.
                for (int j = 0; j < n - 1 - kor; j++)
                {
                    osszehasonlitasok++;
                    if (tomb[j] > tomb[j + 1])
                    {
                        int csere = tomb[j];
                        tomb[j] = tomb[j + 1];
                        tomb[j + 1] = csere;
                    }
                }
            }

            return osszehasonlitasok;
        }
    }
}

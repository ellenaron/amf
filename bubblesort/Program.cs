using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bubblesort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. A tömb létrehozása
            int[] szamok = { 42, 15, 8, 23, 4, 16, 9, 31 };

            // 2. A tömb kiírása rendezés előtt
            Console.WriteLine("A tömb elemei rendezés előtt:");

            for (int i = 0; i < szamok.Length; i++)
            {
                Console.Write(szamok[i] + " ");
            }

            Console.WriteLine();
            Console.WriteLine();

            // 3. Buborékrendezés
            // Külső ciklus: rendezési körök
            for (int i = 0; i < szamok.Length - 1; i++)
            {
                // Extra feladat: jelzi, történt-e csere ebben a körben
                bool tortentCsere = false;

                // Belső ciklus: szomszédos elemek vizsgálata
                // A "- i" miatt a már helyükre került elemeket nem vizsgáljuk újra
                for (int j = 0; j < szamok.Length - 1 - i; j++)
                {
                    // Ha a bal oldali elem nagyobb a jobb oldalinál
                    if (szamok[j] > szamok[j + 1])
                    {
                        // A két elem felcserélése ideiglenes változóval
                        int ideiglenes = szamok[j];
                        szamok[j] = szamok[j + 1];
                        szamok[j + 1] = ideiglenes;

                        tortentCsere = true;
                    }
                }

                // Ha nem történt csere, a tömb már rendezett
                if (!tortentCsere)
                {
                    break;
                }
            }

            // 4. A rendezett tömb kiírása
            Console.WriteLine("A tömb elemei növekvő sorrendben:");

            for (int i = 0; i < szamok.Length; i++)
            {
                Console.Write(szamok[i] + " ");
            }

            Console.WriteLine();
        }
    }
}

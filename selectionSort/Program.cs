using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selectionSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Selection Sort – minimumkiválasztásos rendezés");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine();

            // 1. Alapfeladat
            int[] szamok = { 42, 15, 8, 23, 4, 16, 9, 31 };

            Console.WriteLine("Eredeti tömb:");
            TombKiirasa(szamok);

            SelectionSort(szamok);

            Console.WriteLine();
            Console.WriteLine("Rendezett tömb:");
            TombKiirasa(szamok);

            // 2. Extra: a rendezési lépések megjelenítése
            Console.WriteLine();
            Console.WriteLine("=== Extra: rendezési lépések ===");
            int[] kezi = { 8, 3, 7, 2, 5 };
            SelectionSort(kezi, true, out _, out _);

            // 3. Kötelező tesztelés (+ extra: számlálók)
            Console.WriteLine();
            Console.WriteLine("=== Tesztelés ===");
            Teszt("Már rendezett tömb", new int[] { 2, 4, 6, 8, 10 });
            Teszt("Fordított sorrendű tömb", new int[] { 10, 8, 6, 4, 2 });
            Teszt("Azonos elemeket tartalmazó tömb", new int[] { 5, 2, 5, 1, 2, 8, 1 });
            Teszt("Negatív számokat tartalmazó tömb", new int[] { 4, -3, 8, 0, -7, 2 });
            Teszt("Eredeti adatsor", new int[] { 42, 15, 8, 23, 4, 16, 9, 31 });

            // 4. Extra: véletlen tömb
            Console.WriteLine();
            Console.WriteLine("=== Extra: véletlen tömb ===");
            Console.Write("Add meg a tömb méretét: ");
            string beolvasott = Console.ReadLine();

            if (int.TryParse(beolvasott, out int meret) && meret > 0)
            {
                Random random = new Random();
                int[] veletlen = new int[meret];
                for (int i = 0; i < veletlen.Length; i++)
                {
                    veletlen[i] = random.Next(0, 1000); // 0..999
                }

                Console.WriteLine("Eredeti tömb:");
                TombKiirasa(veletlen);

                SelectionSort(veletlen);

                Console.WriteLine("Rendezett tömb:");
                TombKiirasa(veletlen);
                Console.WriteLine("Rendezett-e? " + (NovekvoE(veletlen) ? "Igen" : "NEM"));
            }
            else
            {
                Console.WriteLine("Érvénytelen méret, a véletlen tömb kimarad.");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Minimumkiválasztásos rendezés: közvetlenül a kapott tömböt módosítja.
        /// </summary>
        static void SelectionSort(int[] tomb)
        {
            SelectionSort(tomb, false, out _, out _);
        }

        /// <summary>
        /// Selection Sort lépéskiírással és számlálókkal.
        /// </summary>
        static void SelectionSort(int[] tomb, bool lepeseketMutat, out long osszehasonlitasok, out long cserek)
        {
            osszehasonlitasok = 0;
            cserek = 0;

            if (lepeseketMutat)
            {
                Console.Write("Kiinduló állapot: ");
                TombKiirasa(tomb);
            }

            // Elég az utolsó előtti elemig menni: az utolsó már a helyén lesz.
            for (int i = 0; i < tomb.Length - 1; i++)
            {
                int minimumIndex = i;

                for (int j = i + 1; j < tomb.Length; j++)
                {
                    osszehasonlitasok++;
                    if (tomb[j] < tomb[minimumIndex])
                    {
                        minimumIndex = j;
                    }
                }

                if (minimumIndex != i)
                {
                    int seged = tomb[i];
                    tomb[i] = tomb[minimumIndex];
                    tomb[minimumIndex] = seged;
                    cserek++;
                }

                if (lepeseketMutat)
                {
                    Console.Write($"{i + 1}. kör után:    ");
                    TombKiirasa(tomb);
                }
            }
        }

        /// <summary>
        /// A tömb elemeinek kiírása szóközökkel elválasztva.
        /// </summary>
        static void TombKiirasa(int[] tomb)
        {
            for (int i = 0; i < tomb.Length; i++)
            {
                Console.Write(tomb[i]);
                if (i < tomb.Length - 1)
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Ellenőrzi, hogy a tömb növekvő (nem csökkenő) sorrendű-e.
        /// </summary>
        static bool NovekvoE(int[] tomb)
        {
            for (int i = 1; i < tomb.Length; i++)
            {
                if (tomb[i - 1] > tomb[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Egy teszteset lefuttatása: kiírás, rendezés, ellenőrzés, számlálók.
        /// </summary>
        static void Teszt(string nev, int[] tomb)
        {
            Console.WriteLine();
            Console.WriteLine($"--- {nev} ---");
            Console.Write("Eredeti:  ");
            TombKiirasa(tomb);

            SelectionSort(tomb, false, out long osszehasonlitasok, out long cserek);

            Console.Write("Rendezett: ");
            TombKiirasa(tomb);
            Console.WriteLine("Növekvő sorrendű? " + (NovekvoE(tomb) ? "Igen" : "NEM"));
            Console.WriteLine($"Összehasonlítások száma: {osszehasonlitasok}");
            Console.WriteLine($"Cserék száma: {cserek}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Beolvasas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeDir = Path.GetDirectoryName(exePath);
            string csvFilePath = Path.Combine(exeDir, "Preferences", "naprendszer.csv");

            if (!File.Exists(csvFilePath))
            {
                Console.WriteLine($"A fájl nem található: {csvFilePath}");
                return;
            }

            var culture = CultureInfo.InvariantCulture;

            string[] datas = File.ReadAllLines(csvFilePath).Skip(1).ToArray();
            // verzió 1
            foreach (string data in datas)
            {
                string[] fields = data.Split(';');
                if (fields.Length != 10)
                {
                    Console.WriteLine($"Hibás sor: {data}");
                    continue;
                }
                int bolygoId = 0;
                if (!int.TryParse(fields[0], NumberStyles.Number, culture, out bolygoId))
                {
                    Console.WriteLine($"hibás típusazonosító: {fields[0]}");
                    continue;
                }

            }
            // verzió 2
            //for (int i = 1; i < datas.Length; i++)
            //{
            //    string data = datas[i];
            //}
            for (int i = 1; i < sorok.Length; i++)
            {
                string sor = sorok[i];

                // Üres sorok kihagyása
                if (string.IsNullOrWhiteSpace(sor))
                    continue;

                // Sor feldarabolása pontosvessző mentén
                string[] adatok = sor.Split(';');

                // Ellenőrizzük az oszlopok számát
                if (adatok.Length != 10)
                {
                    Console.WriteLine(
                        $"Hibás CSV sor ({i + 1}. sor): {sor}"
                    );
                    continue;
                }

                // Adatok konvertálása
                bool sikeres =
                    int.TryParse(adatok[0], NumberStyles.Number, culture, out int bolygoId) &&
                    int.TryParse(adatok[2], NumberStyles.Number, culture, out int tipusId) &&
                    decimal.TryParse(adatok[3], NumberStyles.Number, culture, out decimal tomeg) &&
                    decimal.TryParse(adatok[4], NumberStyles.Number, culture, out decimal egyenlitoSugar) &&
                    int.TryParse(adatok[5], NumberStyles.Number, culture, out int holdakSzama) &&
                    decimal.TryParse(adatok[6], NumberStyles.Number, culture, out decimal napTavolsag) &&
                    decimal.TryParse(adatok[7], NumberStyles.Number, culture, out decimal foldTavolsag);

                // Ha valamelyik konvertálás sikertelen
                if (!sikeres)
                {
                    Console.WriteLine(
                        $"Hibás adat a CSV {i + 1}. sorában!"
                    );
                    continue;
                }

                // Bolygo objektum létrehozása
                Bolygo bolygo = new Bolygo()
                {
                    BolygoId = bolygoId,
                    Neve = adatok[1],
                    TipusId = tipusId,
                    Tomeg = tomeg,
                    EgyenlitoSugar = egyenlitoSugar,
                    Holdakszama = holdakSzama,
                    NapTavolsag = napTavolsag,
                    FoldTavolsag = foldTavolsag,
                    Leiras = adatok[8],
                    URL = adatok[9]
                };

                // Objektum hozzáadása a listához
                bolygok.Add(bolygo);
            }
        }
    }
}

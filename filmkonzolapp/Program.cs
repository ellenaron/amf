using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmkonzolapp
{
    internal class Program
    {
        // Adatbázis-kapcsolat
        private static string connectionString =
            "Server=localhost;" +
            "Port=3307;" +
            "Database=filmek_db;" +
            "Uid=root;" +
            "Pwd=;";

        // Memóriabeli listák
        private static List<Rendezo> rendezok = new List<Rendezo>();
        private static List<Film> filmek = new List<Film>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==================================");
            Console.WriteLine("       FILMNYILVÁNTARTÓ");
            Console.WriteLine("==================================");

            // Adatok betöltése
            if (!AdatokBetoltese())
            {
                Console.WriteLine("\nA program nem tudta betölteni az adatokat.");
                Console.WriteLine("Ellenőrizd az adatbázis-kapcsolatot!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nAdatok sikeresen betöltve!");
            Console.WriteLine($"Rendezők száma: {rendezok.Count}");
            Console.WriteLine($"Filmek száma: {filmek.Count}");

            // Menü indítása
            Menu();
        }

        // ==========================================
        // ADATOK BETÖLTÉSE
        // ==========================================

        static bool AdatokBetoltese()
        {
            try
            {
                using (MySqlConnection connection =
                    new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Rendezők betöltése
                    string rendezoQuery =
                        "SELECT Id, Nev, SzuletesiEv, Nemzetiseg " +
                        "FROM Rendezo";

                    using (MySqlCommand command =
                        new MySqlCommand(rendezoQuery, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rendezo rendezo = new Rendezo(
                                reader.GetInt32("Id"),
                                reader.GetString("Nev"),
                                reader.GetInt32("SzuletesiEv"),
                                reader.GetString("Nemzetiseg")
                            );

                            rendezok.Add(rendezo);
                        }
                    }

                    // Filmek betöltése
                    string filmQuery =
                        "SELECT Id, Cim, MegjelenesiEv, RendezoId, OscarDijasE " +
                        "FROM Film";

                    using (MySqlCommand command =
                        new MySqlCommand(filmQuery, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Film film = new Film(
                                reader.GetInt32("Id"),
                                reader.GetString("Cim"),
                                reader.GetInt32("MegjelenesiEv"),
                                reader.GetInt32("RendezoId"),
                                reader.GetBoolean("OscarDijasE")
                            );

                            filmek.Add(film);
                        }
                    }
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("MySQL hiba történt!");
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Váratlan hiba történt!");
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // ==========================================
        // MENÜ
        // ==========================================

        static void Menu()
        {
            bool fut = true;

            while (fut)
            {
                Console.WriteLine("\n==================================");
                Console.WriteLine("              MENÜ");
                Console.WriteLine("==================================");

                Console.WriteLine("1. Rendezők listázása és filmjeik száma");
                Console.WriteLine("2. Adott rendező filmjeinek listázása");
                Console.WriteLine("3. Filmek szűrése évszám szerint");
                Console.WriteLine("4. Oscar-díjas filmek listázása");
                Console.WriteLine("5. Kilépés");

                Console.WriteLine("==================================");
                Console.Write("Választás: ");

                string valasztas = Console.ReadLine();

                Console.WriteLine();

                switch (valasztas)
                {
                    case "1":
                        RendezokListazasa();
                        break;

                    case "2":
                        RendezoFilmjei();
                        break;

                    case "3":
                        FilmekSzureseEvSzerint();
                        break;

                    case "4":
                        OscarDijasFilmek();
                        break;

                    case "5":
                        fut = false;
                        Console.WriteLine("Kilépés a programból...");
                        break;

                    default:
                        Console.WriteLine("Érvénytelen menüpont!");
                        break;
                }
            }
        }

        // ==========================================
        // 1. RENDEZŐK LISTÁZÁSA
        // ==========================================

        static void RendezokListazasa()
        {
            Console.WriteLine("=== RENDEZŐK ÉS FILMJEIK SZÁMA ===\n");

            foreach (var r in rendezok)
            {
                int filmekSzama = filmek.Count(
                    f => f.RendezoId == r.Id
                );

                Console.WriteLine(
                    $"{r.Nev} | " +
                    $"Születési év: {r.SzuletesiEv} | " +
                    $"Nemzetiség: {r.Nemzetiseg} | " +
                    $"Filmek száma: {filmekSzama}"
                );
            }
        }

        // ==========================================
        // 2. ADOTT RENDEZŐ FILMJEINEK LISTÁZÁSA
        // ==========================================

        static void RendezoFilmjei()
        {
            Console.WriteLine("=== RENDEZŐ FILMJEI ===\n");

            Console.Write("Add meg a rendező nevét vagy annak részletét: ");

            string keresettNev = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(keresettNev))
            {
                Console.WriteLine("Nem adtál meg nevet!");
                return;
            }

            // Rendezők keresése
            var talalatok = rendezok
                .Where(r => r.Nev.ToLower().Contains(keresettNev.ToLower()))
                .ToList();

            if (talalatok.Count == 0)
            {
                Console.WriteLine("Nem található ilyen rendező!");
                return;
            }

            foreach (var rendezo in talalatok)
            {
                Console.WriteLine($"\nRendező: {rendezo.Nev}");

                // Az adott rendező filmjeinek lekérdezése
                var rendezoFilmek = filmek
                    .Where(f => f.RendezoId == rendezo.Id)
                    .ToList();

                if (rendezoFilmek.Count == 0)
                {
                    Console.WriteLine("Ennek a rendezőnek nincs filmje.");
                    continue;
                }

                foreach (var film in rendezoFilmek)
                {
                    Console.WriteLine(
                        $"  - {film.Cim} ({film.MegjelenesiEv}) | " +
                        $"Oscar-díjas: {(film.OscarDijasE ? "Igen" : "Nem")}"
                    );
                }
            }
        }

        // ==========================================
        // 3. FILMEK SZŰRÉSE ÉVSZÁM SZERINT
        // ==========================================

        static void FilmekSzureseEvSzerint()
        {
            Console.WriteLine("=== FILMEK SZŰRÉSE ÉVSZÁM SZERINT ===\n");

            Console.Write("Adj meg egy évszámot: ");

            if (!int.TryParse(Console.ReadLine(), out int ev))
            {
                Console.WriteLine("Érvénytelen évszám!");
                return;
            }

            Console.WriteLine("\nVálassz szűrési irányt:");
            Console.WriteLine("1. Régebbi filmek");
            Console.WriteLine("2. Újabb vagy azonos évű filmek");

            Console.Write("Választás: ");

            string irany = Console.ReadLine();

            List<Film> talalatok;

            if (irany == "1")
            {
                // A megadott évnél régebbi filmek
                talalatok = filmek
                    .Where(f => f.MegjelenesiEv < ev)
                    .OrderBy(f => f.MegjelenesiEv)
                    .ToList();
            }
            else if (irany == "2")
            {
                // A megadott évvel azonos vagy újabb filmek
                talalatok = filmek
                    .Where(f => f.MegjelenesiEv >= ev)
                    .OrderBy(f => f.MegjelenesiEv)
                    .ToList();
            }
            else
            {
                Console.WriteLine("Érvénytelen választás!");
                return;
            }

            if (talalatok.Count == 0)
            {
                Console.WriteLine("Nincs találat!");
                return;
            }

            Console.WriteLine("\nTalálatok:\n");

            foreach (var film in talalatok)
            {
                // Rendező megkeresése az idegen kulcs alapján
                var rendezo = rendezok.FirstOrDefault(
                    r => r.Id == film.RendezoId
                );

                string rendezoNev = rendezo?.Nev ?? "Ismeretlen";

                Console.WriteLine(
                    $"{film.Cim} | " +
                    $"Év: {film.MegjelenesiEv} | " +
                    $"Rendező: {rendezoNev}"
                );
            }
        }

        // ==========================================
        // 4. OSCAR-DÍJAS FILMEK LISTÁZÁSA
        // ==========================================

        static void OscarDijasFilmek()
        {
            Console.WriteLine("=== OSCAR-DÍJAS FILMEK ===\n");

            var oscarDijasFilmek = filmek
                .Where(f => f.OscarDijasE)
                .OrderBy(f => f.MegjelenesiEv)
                .ToList();

            if (oscarDijasFilmek.Count == 0)
            {
                Console.WriteLine("Nincs Oscar-díjas film az adatbázisban!");
                return;
            }

            foreach (var film in oscarDijasFilmek)
            {
                var rendezo = rendezok.FirstOrDefault(
                    r => r.Id == film.RendezoId
                );

                string rendezoNev = rendezo?.Nev ?? "Ismeretlen";

                Console.WriteLine(
                    $"{film.Cim} | " +
                    $"Év: {film.MegjelenesiEv} | " +
                    $"Rendező: {rendezoNev}"
                );
            }

            Console.WriteLine(
                $"\nÖsszesen {oscarDijasFilmek.Count} Oscar-díjas film található."
            );
        }
    }
}

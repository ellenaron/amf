using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmkonzolapp
{
    internal class Film
    {
        public int Id { get; set; }
        public string Cim { get; set; }
        public int MegjelenesiEv { get; set; }
        public int RendezoId { get; set; }
        public bool OscarDijasE { get; set; }

        public Film(
            int id,
            string cim,
            int megjelenesiEv,
            int rendezoId,
            bool oscarDijasE)
        {
            Id = id;
            Cim = cim;
            MegjelenesiEv = megjelenesiEv;
            RendezoId = rendezoId;
            OscarDijasE = oscarDijasE;
        }

        public override string ToString()
        {
            string oscar = OscarDijasE ? "Igen" : "Nem";

            return $"{Cim} ({MegjelenesiEv}) | Oscar-díjas: {oscar}";
        }
    }
}

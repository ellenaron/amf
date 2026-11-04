using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beolvasas
{
    internal class Bolygo
    {
        public int BolygoId { get; set; }
        public string Neve { get; set; }
        public int TipusId { get; set; }
        public decimal Tomeg { get; set; }
        public decimal EgyenlitoSugar { get; set; }
        public int HoldakSzama { get; set; }
        public decimal NapTavolsag { get; set; }
        public decimal FoldTavolsag { get; set; }
        public string Leiras { get; set; }
        public string URL { get; set; }

        //public Bolygo(int bolygoId, string neve, int tipusId, decimal tomeg, decimal egyenlitosugar, int holdakSzama, decimal napTavolsag, decimal foldTavolsag, string leiras, string url)
        //{
        //    BolygoId = bolygoId;
        //    Neve = neve;
        //    TipusId = tipusId;
        //    Tomeg = tomeg;
        //    EgyenlitoSugar = egyenlitosugar;
        //    HoldakSzama = holdakSzama;
        //    NapTavolsag = napTavolsag;
        //    FoldTavolsag = foldTavolsag;
        //    Leiras = leiras;
        //    URL = url;
        //}
    }
}

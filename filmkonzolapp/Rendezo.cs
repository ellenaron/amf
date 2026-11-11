using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filmkonzolapp
{
    internal class Rendezo
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public int SzuletesiEv { get; set; }
        public string Nemzetiseg { get; set; }

        public Rendezo(int id, string nev, int szuletesiEv, string nemzetiseg)
        {
            Id = id;
            Nev = nev;
            SzuletesiEv = szuletesiEv;
            Nemzetiseg = nemzetiseg;
        }

        public override string ToString()
        {
            return $"{Nev} ({SzuletesiEv}, {Nemzetiseg})";
        }
    }
}

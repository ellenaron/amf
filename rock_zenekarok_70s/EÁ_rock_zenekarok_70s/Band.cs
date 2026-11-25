using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EÁ_rock_zenekarok_70s
{
    internal class Band
    {
        public int Id { get; }
        public string Name { get; }
        public string Members { get; }
        public int ActiveYears { get; }
        public string Url { get; }
        public Band(int id, string name, string members, int activeYears, string url)
        {
            Id = id;
            Name = name;
            Members = members;
            ActiveYears = activeYears;
            Url = url;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioTerm.Player
{
    public class Station
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }

        public int DefiniteId { get; }
        public Station(string name, string url, int definiteid)
        {
            Name = name;
            Url = url;
            DefiniteId = definiteid;
        }
    }
}

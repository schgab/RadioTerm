using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioTerm
{
    public class Station
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public Station(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}

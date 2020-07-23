using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioTerm
{
    public class StationManager
    {
        public List<Station> Stations { get; private set; }

        public Station PlayingStation { get; private set; }
        private int index = 0;

        
        public StationManager()
        {
            Stations = new List<Station>();
        }
        public void AddStation(Station station)
        {
            Stations.Add(station);
        }
        public void AddStation((string name,string url) tuple)
        {
            Stations.Add(new Station(tuple.name,tuple.url));
        }
        public void AddStation(IEnumerable<Station> stations)
        {
            Stations.AddRange(stations);
        }

        public Station Next()
        {
            Stations[index].Active = false;
            index = (index + 1) % Stations.Count;
            Stations[index].Active = true;
            PlayingStation = Stations[index];
            return Stations[index];
        }
        public Station Previous()
        {
            Stations[index].Active = false;
            if(index == 0)
            {
                index = Stations.Count;
            }
            index--;
            Stations[index].Active = true;
            PlayingStation = Stations[index];
            return Stations[index];
        }

        public void ToggleActive()
        {
            Stations[index].Active = !Stations[index].Active;
        }
        public void DeleteStation(string name)
        {
            var st = Stations.FirstOrDefault(s => s.Name == name);
            if (st != null)
            {
                Stations.Remove(st);
            }
        }

        public void SetAllInactive()
        {
            Stations.ForEach(c => c.Active = false);
            PlayingStation = null;
        }

    }
}

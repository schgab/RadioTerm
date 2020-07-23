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

        private Station _playingStation;
        public Station PlayingStation 
        {
            get
            {
                if (_playingStation == null)
                {
                    SetLastActive();
                }
                return _playingStation;
            }
            private set
            {
                _playingStation = value;
            }
        }


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
            PlayingStation.Active = false;
            PlayingStation = Stations.Next(PlayingStation);
            PlayingStation.Active = true;
            return PlayingStation;
        }
        public Station Previous()
        {
            PlayingStation.Active = false;
            PlayingStation = Stations.Previous(PlayingStation);
            PlayingStation.Active = true;
            return PlayingStation;
        }

        public void ToggleActive()
        {
            PlayingStation.Active = !PlayingStation.Active;
        }
        public void DeleteStation(string name)
        {
            var st = Stations.FirstOrDefault(s => s.Name == name);
            if (st != null)
            {
                
                Stations.Remove(st);
                
            }
        }
        private void SetLastActive()
        {
            var s = Stations.Where(c => c.Active).FirstOrDefault();
            if (s == null)
            {
                PlayingStation = Stations[0];
            }
            else
            {
                PlayingStation = s;
            }
        }
        public void Reset()
        {
            Stations.ForEach(c => c.Active = false);
        }

    }
}

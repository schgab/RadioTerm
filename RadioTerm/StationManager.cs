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

        public Station PlayingStation { get; set; }


        public StationManager()
        {
            Stations = new List<Station>();
        }

        /// <summary>
        /// Adds a Station object to the list
        /// </summary>
        /// <param name="station"></param>
        public bool AddStation(Station station)
        {
            if (PlayabilityChecker.CheckIfPlayable(station))
            {
                Stations.Add(station);
                if (PlayingStation == null)
                {
                    PlayingStation = station;
                }
                return true;
            }
            return false;
            
        }
        /// <summary>
        /// Adds a new station with the supplied name and url
        /// </summary>
        /// <param name="tuple"></param>
        public bool AddStation((string name,string url) tuple)
        {
            return AddStation(new Station(tuple.name,tuple.url));
        }

        /// <summary>
        /// Returns the next station in the list or null if the list is empty
        /// </summary>
        /// <returns></returns>
        public Station Next()
        {
            if (Stations.Count == 0)
            {
                return null;
            }
            PlayingStation.Active = false;
            PlayingStation = Stations.Next(PlayingStation);
            PlayingStation.Active = true;
            return PlayingStation;
        }
        /// <summary>
        /// Returns the previous station in the list or null if the list is empty
        /// </summary>
        /// <returns></returns>
        public Station Previous()
        {
            if (Stations.Count == 0)
            {
                return null;
            }
            PlayingStation.Active = false;
            PlayingStation = Stations.Previous(PlayingStation);
            PlayingStation.Active = true;
            return PlayingStation;
        }

        public void ToggleActive()
        {
            PlayingStation.Active = !PlayingStation.Active;
        }

        /// <summary>
        /// Deletes all stations with the specified name
        /// </summary>
        /// <param name="name"></param>
        public void DeleteStation(string name)
        {
            var st = Stations.FirstOrDefault(s => s.Name == name);
            if (st != null)
            {
                Stations.Remove(st);
            }
        }



    }
}

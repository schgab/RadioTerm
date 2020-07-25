using Newtonsoft.Json;
using RadioTerm.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RadioTerm.Player
{
    public class StationManager
    {
        public List<Station> Stations { get; private set; }

        public Station PlayingStation { get; set; }


        public StationManager()
        {
            Stations = new List<Station>();
        }

        [JsonConstructor]
        public StationManager(List<Station> stations, Station playingStation)
        {
            Stations = stations ?? new List<Station>();
            if (Stations.Count > 0 && playingStation is Station)
            {
                PlayingStation = Stations.Single(s => s.DefiniteId == playingStation.DefiniteId);
            }
        }

        /// <summary>
        /// Adds a Station object to the list
        /// </summary>
        /// <param name="station"></param>
        public bool AddStation(Station station)
        {
            if (station.IsPlayable())
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
        public bool AddStation((string name, string url) tuple)
        {
            return AddStation(new Station(tuple.name, tuple.url, NextDefiniteID()));
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
        /// Deletes the stations with the specified id
        /// </summary>
        /// <param name="name"></param>
        public void DeleteStation(int id)
        {
            var st = Stations.FirstOrDefault(s => s.DefiniteId == id);
            if (st != null)
            {
                Stations.Remove(st);
                if (st == PlayingStation)
                {
                    PlayingStation = Next();
                    OnPlayingStationDeleted();
                }
            }
        }

        private int NextDefiniteID()
        {
            if (Stations.Count > 0)
            {
                return Stations.OrderByDescending(s => s.DefiniteId).Select(s => s.DefiniteId).First() + 1;
            }
            return 1;
        }

        public event EventHandler PlayingStationDeleted;

        private void OnPlayingStationDeleted()
        {
            PlayingStationDeleted?.Invoke(this,EventArgs.Empty);
        }

    }
}

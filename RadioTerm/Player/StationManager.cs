using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RadioTerm.Helpers;

namespace RadioTerm.Player
{
    public sealed class StationManager
    {
        private Station _lastSelectedStation;

        public StationManager() => Stations = new List<Station>();

        [JsonConstructor]
        public StationManager(
            [JsonProperty(nameof(Stations))]List<Station> stations,
            [JsonProperty(nameof(LastSelectedStationId))] int? lastSelectedStationId)
        {
            Stations = stations ?? new List<Station>();
            LastSelectedStationId = lastSelectedStationId;

            if (Stations.Count > 0 && Stations.All(station => station.Id != lastSelectedStationId))
                throw new ArgumentException("LastSelectedStationId does not found in stations list");
        }

        [JsonProperty(nameof(Stations))]
        public List<Station> Stations { get; }

        [JsonProperty(nameof(LastSelectedStationId))]
        public int? LastSelectedStationId { get; private set; }

        [JsonIgnore]
        public Station LastSelectedStation
        {
            get
            {
                if (_lastSelectedStation is null && LastSelectedStationId.HasValue)
                {
                    _lastSelectedStation = Stations
                        .Single(station => station.Id == LastSelectedStationId.Value);
                }

                return _lastSelectedStation;
            }
            private set
            {
                _lastSelectedStation = value;

                if (_lastSelectedStation != null)
                    LastSelectedStationId = _lastSelectedStation.Id;
            }
        }

        /// <summary>
        /// Adds a new station with the supplied name and url
        /// </summary>
        public bool AddStation(string name,string url)
        {
            return AddStation(new Station(GetNextId(), name, url));

            #region Local function
            int GetNextId()
            {
                if (Stations.Count == 0)
                    return 1;

                return Stations
                    .OrderByDescending(st => st.Id)
                    .Select(st => st.Id)
                    .First() + 1;
            }
            #endregion
        }

        /// <summary>
        /// Returns the next station in the list or null if the list is empty
        /// </summary>
        /// <returns></returns>
        public Station Next() => ChangePlayingStation(Stations.Next);

        /// <summary>
        /// Returns the previous station in the list or null if the list is empty
        /// </summary>
        /// <returns></returns>
        public Station Previous() => ChangePlayingStation(Stations.Previous);

        private Station ChangePlayingStation(Func<Station, Station> getNewValueAction)
        {
            if (Stations.Count == 0)
                return null;

            LastSelectedStation.Active = false;
            LastSelectedStation = getNewValueAction(LastSelectedStation);
            LastSelectedStation.Active = true;

            return LastSelectedStation;
        }

        public void ToggleActive() => LastSelectedStation.Active = !LastSelectedStation.Active;

        /// <summary>
        /// Deletes station by id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteStation(int id)
        {
            var station = Stations.FirstOrDefault(st => st.Id == id);
            if (station is null)
                return;

            Stations.Remove(station);
        }

        private bool AddStation(Station station)
        {
            if (!station.IsPlayable())
                return false;

            Stations.Add(station);

            if (LastSelectedStation == null)
                LastSelectedStation = station;

            return true;
        }
    }
}

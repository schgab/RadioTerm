using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RadioTerm.Helpers;

namespace RadioTerm.Player;

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
        if (Stations.Count > 0 && playingStation is not null)
        {
            PlayingStation = Stations.Single(s => s.Id == playingStation.Id);
        }
    }

    /// <summary>
    /// Adds a Station object to the list
    /// </summary>
    /// <param name="station"></param>
    public bool AddStation(Station station)
    {
        if (!station.IsPlayable())
        {
            return false;
        }

        Stations.Add(station);
        PlayingStation ??= station;
        return true;
    }

    /// <summary>
    /// Adds a new station with the supplied name and url
    /// </summary>
    /// <param name="tuple"></param>
    public bool AddStation((string name, string url) tuple)
    {
        return AddStation(new Station(tuple.name, tuple.url));
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
    /// <param name="id"></param>
    public void DeleteStation(Guid id)
    {
        var st = Stations.FirstOrDefault(s => s.Id == id);
        if (st is null)
        {
            return;
        }
        Stations.Remove(st);
        if (st == PlayingStation)
        {
            PlayingStation = Next();
            OnPlayingStationDeleted();
        }
    }

    public event EventHandler PlayingStationDeleted;

    private void OnPlayingStationDeleted()
    {
        PlayingStationDeleted?.Invoke(this, EventArgs.Empty);
    }
}
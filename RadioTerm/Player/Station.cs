using System;

namespace RadioTerm.Player;

public class Station
{
    public string Name { get; }
    public string Url { get; }
    public bool Active { get; set; }

    public Guid Id { get; init; } = Guid.NewGuid();

    public Station(string name, string url)
    {
        Name = name;
        Url = url;
    }
}
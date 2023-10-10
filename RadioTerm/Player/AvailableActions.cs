using System;
using System.Collections.Generic;
using System.Linq;

namespace RadioTerm.Player;

public static class AvailableActions
{
    public enum PlayerAction
    {
        None,
        Quit,
        Add,
        VolumeDown,
        VolumeUp,
        Next,
        Previous,
        Pause,
        Delete
    }

    public static readonly Dictionary<PlayerAction, string> Mapping = new()
    {
        { PlayerAction.Quit, "q" },
        { PlayerAction.Add, "a" },
        { PlayerAction.VolumeDown, "-" },
        { PlayerAction.VolumeUp, "+" },
        { PlayerAction.Next, "n" },
        { PlayerAction.Previous, "p" },
        { PlayerAction.Pause, "space" },
        { PlayerAction.Delete, "d" },
    };

    public static PlayerAction ToPlayerAction(this ConsoleKeyInfo info) => info.KeyChar switch
    {
        ' ' => PlayerAction.Pause,
        _ => Mapping.FirstOrDefault(s => s.Value[0] == info.KeyChar).Key
    };
}
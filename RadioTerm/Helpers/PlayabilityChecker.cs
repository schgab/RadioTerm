using System;
using System.Threading;
using LibVLCSharp.Shared;
using RadioTerm.Player;

namespace RadioTerm.Helpers;

public static class PlayabilityChecker
{
    public static bool IsPlayable(this Station station)
    {
        using var vlc = new LibVLC();
        using var player = new MediaPlayer(vlc);
        vlc.Log += (_, _) => { }; // disable default log handler
        var previousVolume = player.Volume;
        player.Volume = 0;
        try
        {
            player.Play(new Media(vlc, station.Url, FromType.FromLocation));
            Thread.Sleep(1000); // dirty hack to let vlc some time to load the stream
            var playable = player.IsPlaying;
            player.Stop();
            player.Volume = previousVolume; // restore the volume
            return playable;
        }
        catch
        {
            return false;
        }
    }
}
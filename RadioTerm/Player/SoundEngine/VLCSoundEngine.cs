using LibVLCSharp.Shared;

namespace RadioTerm.Player.SoundEngine;

public sealed class VLCSoundEngine : ISoundEngine
{
    private readonly LibVLC _libVlc = new();
    private readonly MediaPlayer _mediaPlayer;

    public VLCSoundEngine()
    {
        _libVlc.Log += (_,_) => { }; // disable vlc log
        _mediaPlayer = new MediaPlayer(_libVlc);
        _mediaPlayer.Volume = 50;
    }

    public void Pause(Station currentStation)
    {
        switch (_mediaPlayer.State)
        {
            case VLCState.Stopped:
                Play(currentStation);
                break;
            case VLCState.Playing:
                _mediaPlayer.Pause();
                currentStation.Active = false;
                break;
            case VLCState.Paused:
                _mediaPlayer.Play();
                currentStation.Active = true;
                break;
        }
    }

    public void Play(Station stationToPlay)
    {
        if (stationToPlay is not null)
        {
            _mediaPlayer.Play(new Media(_libVlc, stationToPlay.Url, FromType.FromLocation));
        }
    }

    public void Stop()
    {
        _mediaPlayer.Stop();
    }

    public void VolumeDown() => _mediaPlayer.Volume =
        _mediaPlayer.Volume <= 10 ? _mediaPlayer.Volume = 0 : _mediaPlayer.Volume - 10;

    public void VolumeUp() => _mediaPlayer.Volume += 10;
}
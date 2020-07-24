using NAudio.Wave;

namespace RadioTerm.Player
{
    public sealed class PlayerEngine
    {
        private readonly WaveOutEvent _outputController = new WaveOutEvent();

        public StationManager StationManager { get; }

        public PlayerEngine(StationManager manager) => StationManager = manager;

        public void Play(Station station)
        {
            Stop();
            station.Active = true;
            using (var mf = new MediaFoundationReader(station.Url))
            {
                _outputController.Init(mf);
                _outputController.Play();
            }
        }

        public void PlayLastActive() => Play(StationManager.LastSelectedStation);

        public void Pause()
        {
            switch (_outputController.PlaybackState)
            {
                case PlaybackState.Paused:
                    _outputController.Play();
                    StationManager.LastSelectedStation.Active = true;
                    break;
                case PlaybackState.Playing:
                    _outputController.Pause();
                    StationManager.LastSelectedStation.Active = false;
                    break;
                case PlaybackState.Stopped when StationManager.LastSelectedStation != null:
                    Play(StationManager.LastSelectedStation);
                    break;
            }
        }

        public void Stop()
        {
            StationManager.LastSelectedStation.Active = false;
            _outputController.Stop();
        }

        public void VolumeDown() =>
            _outputController.Volume = _outputController.Volume < 0.1 ? 0.0f : _outputController.Volume - 0.1f;

        public void VolumeUp() =>
            _outputController.Volume = _outputController.Volume > 0.9 ? 1.0f : _outputController.Volume + 0.1f;

        public void Previous() => Play(StationManager.Previous());

        public void Next() => Play(StationManager.Next());
    }
}

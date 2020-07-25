using NAudio.Wave;

namespace RadioTerm.Player.SoundEngine
{
    public sealed class NAudioSoundEngine : ISoundEngine
    {

        private readonly WaveOutEvent outputDevice = new WaveOutEvent();

        public void Pause(Station currentStation)
        {
            switch (outputDevice.PlaybackState)
            {
                case PlaybackState.Stopped:
                    Play(currentStation);
                    break;
                case PlaybackState.Playing:
                    outputDevice.Pause();
                    currentStation.Active = false;
                    break;
                case PlaybackState.Paused:
                    outputDevice.Play();
                    currentStation.Active = true;
                    break;
                default:
                    break;
            }
        }

        public void Play(Station stationToPlay)
        {
            if (stationToPlay is Station)
            {
                Stop();
                using (var reader = new MediaFoundationReader(stationToPlay.Url))
                {
                    outputDevice.Init(reader);
                    outputDevice.Play();
                }
                stationToPlay.Active = true;
            }
        }

        public void Stop()
        {
            outputDevice.Stop();
        }

        public void VolumeDown() => outputDevice.Volume = outputDevice.Volume < 0.1f ? outputDevice.Volume = 0 : outputDevice.Volume - 0.1f;

        public void VolumeUp() => outputDevice.Volume = outputDevice.Volume > 0.9f ? outputDevice.Volume = 1.0f : outputDevice.Volume + 0.1f;

    }
}

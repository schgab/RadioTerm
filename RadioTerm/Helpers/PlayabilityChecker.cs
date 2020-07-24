using System;
using NAudio.Wave;
using RadioTerm.Player;

namespace RadioTerm.Helpers
{
    public static class PlayabilityChecker
    {
        public static bool IsPlayable(this Station station)
        {
            if (station is null)
                throw new ArgumentNullException(nameof(station));

            try
            {
                using (var outputController = new WaveOutEvent{ Volume = 0})
                using (var streamReader = new MediaFoundationReader(station.Url))
                {
                    outputController.Init(streamReader);
                    outputController.Play();
                    outputController.Stop();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

using NAudio.Wave;
using RadioTerm.Player;
using System;

namespace RadioTerm.Helpers
{
    public static class PlayabilityChecker
    {
        public static bool IsPlayable(this Station s)
        {
            try
            {
                using (var wo = new WaveOutEvent())
                using (var mf = new MediaFoundationReader(s.Url))
                {
                    float woVol = wo.Volume;
                    wo.Init(mf);
                    wo.Volume = 0;
                    wo.Play();
                    wo.Stop();
                    wo.Volume = woVol;
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

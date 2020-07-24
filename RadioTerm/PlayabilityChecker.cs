using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioTerm
{
    public static class PlayabilityChecker
    {
        public static bool CheckIfPlayable(Station s)
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

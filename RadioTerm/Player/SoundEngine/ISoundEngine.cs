using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioTerm.Player.SoundEngine
{
    public interface ISoundEngine
    {
        void Play(Station stationToPlay);
        void Stop();
        void Pause(Station currentStation);
        void VolumeDown();
        void VolumeUp();
    }
}

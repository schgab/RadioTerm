using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioTerm
{
    public interface IPlayer
    {
        void Play(Station s);
        void Stop();
        void Pause();
        void VolumeDown();
        void VolumeUp();
    }
}

using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioTerm
{
    class Player : IPlayer
    {

        private WaveOutEvent currentlyPlaying = new WaveOutEvent();
        public void Pause()
        {
            if(currentlyPlaying.PlaybackState == PlaybackState.Paused)
            {
                currentlyPlaying.Play();
            }
            else if(currentlyPlaying.PlaybackState == PlaybackState.Playing)
            {
                currentlyPlaying.Pause();
            }
        }

        public void Play(Station s)
        {
            Stop();
            using(var mf = new MediaFoundationReader(s.Url))
            {
                currentlyPlaying.Init(mf);
                currentlyPlaying.Play();
            }
        }

        public void Stop()
        {
            currentlyPlaying.Stop();
        }

        public void VolumeDown()
        {
            if (currentlyPlaying.Volume < 0.1)
            {
                currentlyPlaying.Volume = 0.0f;
            }
            else
            {
                currentlyPlaying.Volume -= 0.1f;
            }
        }

        public void VolumeUp()
        {
            if (currentlyPlaying.Volume > 0.9)
            {
                currentlyPlaying.Volume = 1.0f;
            }
            else
            {
                currentlyPlaying.Volume += 0.1f;
            }
        }
    }
}

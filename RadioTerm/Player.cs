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

        public StationManager RadioStationManager { get; private set; }

        private bool _HasSomethingToPlay;

        public bool HasSomethingToPlay
        {
            get 
            {
                _HasSomethingToPlay = RadioStationManager.Stations.Count > 0;
                return _HasSomethingToPlay; 
            }
            private set { _HasSomethingToPlay = value; }
        }


        public Player(StationManager manager)
        {
            RadioStationManager = manager;
           
        }
        public void Pause()
        {
            if(currentlyPlaying.PlaybackState == PlaybackState.Paused)
            {
                currentlyPlaying.Play();
                RadioStationManager.PlayingStation.Active = true;
            }
            else if(currentlyPlaying.PlaybackState == PlaybackState.Playing)
            {
                currentlyPlaying.Pause();
                RadioStationManager.PlayingStation.Active = false;
            }
            else if(currentlyPlaying.PlaybackState == PlaybackState.Stopped && HasSomethingToPlay)
            {
                Play(RadioStationManager.PlayingStation);
                RadioStationManager.PlayingStation.Active = true;
            }
        }


        public void Play(Station s)
        {
            if (s is Station)
            {
                Stop();
                using (var mf = new MediaFoundationReader(s.Url))
                {
                    currentlyPlaying.Init(mf);
                    currentlyPlaying.Play();
                }
            }
            
        }

        public void PlayLastActive()
        {
            if (RadioStationManager.PlayingStation is Station)
            {
                Play(RadioStationManager.PlayingStation);
                RadioStationManager.PlayingStation.Active = true;
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

        public void Next()
        {
            if (RadioStationManager.Next() is Station s)
            {
                Play(s);
            }
        }

        public void Previous()
        {
            if (RadioStationManager.Previous() is Station s)
            {
                Play(s);
            }
        }
    }
}

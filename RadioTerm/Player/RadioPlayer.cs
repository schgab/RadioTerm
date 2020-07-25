using RadioTerm.IO;
using RadioTerm.Player.SoundEngine;
using RadioTerm.Rendering;
using RadioTerm.Rendering.Message;
using System;

namespace RadioTerm.Player
{
    public class RadioPlayer : IPlayer
    {


        private readonly IDisplayEngine _displayEngine;
        private readonly ISoundEngine _soundEngine;

        public StationManager StationManager { get; }

        public RadioPlayer(IDisplayEngine displayEngine, ISoundEngine soundEngine)
        {
            _displayEngine = displayEngine;
            _soundEngine = soundEngine;
            StationManager = ApplicationDataHandler.Load();
            StationManager.PlayingStationDeleted += StationManager_PlayingStationDeleted;
        }

        private void StationManager_PlayingStationDeleted(object sender, EventArgs e) => _soundEngine.Stop();

        public void Run()
        {
            _soundEngine.Play(StationManager.PlayingStation);
            AvailableActions.PlayerAction playerAction = AvailableActions.PlayerAction.None;
            do
            {
                _displayEngine.Draw(StationManager.Stations);
                playerAction = Console.ReadKey().ToPlayerAction();
                switch (playerAction)
                {
                    case AvailableActions.PlayerAction.None:
                        break;
                    case AvailableActions.PlayerAction.Quit:
                        break;
                    case AvailableActions.PlayerAction.Add:
                        if(!StationManager.AddStation(_displayEngine.AddStation()))
                        {
                            _displayEngine.ShowMessage(new Message("This station could not be added. Check URL", MessageType.Critical));
                        }
                        break;
                    case AvailableActions.PlayerAction.VolumeDown:
                        _soundEngine.VolumeDown();
                        break;
                    case AvailableActions.PlayerAction.VolumeUp:
                        _soundEngine.VolumeUp();
                        break;
                    case AvailableActions.PlayerAction.Next:
                        _soundEngine.Play(StationManager.Next());
                        break;
                    case AvailableActions.PlayerAction.Previous:
                        _soundEngine.Play(StationManager.Previous());
                        break;
                    case AvailableActions.PlayerAction.Pause:
                        _soundEngine.Pause(StationManager.PlayingStation);
                        break;
                    case AvailableActions.PlayerAction.Delete:
                        StationManager.DeleteStation(_displayEngine.DeleteStation(StationManager.Stations));
                        break;
                    default:
                        break;
                }


            } while (playerAction != AvailableActions.PlayerAction.Quit);
            ApplicationDataHandler.Save(StationManager);
        }
    }
}

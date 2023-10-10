using System;
using RadioTerm.IO;
using RadioTerm.Player.SoundEngine;
using RadioTerm.Rendering;
using RadioTerm.Rendering.Message;

namespace RadioTerm.Player;

public class RadioPlayer : IPlayer
{
    private readonly IDisplayEngine _displayEngine;
    private readonly ISoundEngine _soundEngine;

    private readonly StationManager _stationManager;

    public RadioPlayer(IDisplayEngine displayEngine, ISoundEngine soundEngine)
    {
        _displayEngine = displayEngine;
        _soundEngine = soundEngine;
        _stationManager = ApplicationDataHandler.Load();
        _stationManager.PlayingStationDeleted += StationManager_PlayingStationDeleted;
    }

    private void StationManager_PlayingStationDeleted(object sender, EventArgs e) => _soundEngine.Stop();

    public void Run()
    {
        _soundEngine.Play(_stationManager.PlayingStation);
        AvailableActions.PlayerAction playerAction;
        do
        {
            _displayEngine.Draw(_stationManager.Stations);
            playerAction = Console.ReadKey().ToPlayerAction();
            switch (playerAction)
            {
                case AvailableActions.PlayerAction.None:
                    break;
                case AvailableActions.PlayerAction.Quit:
                    break;
                case AvailableActions.PlayerAction.Add:
                    if (!_stationManager.AddStation(_displayEngine.AddStation()))
                    {
                        _displayEngine.ShowMessage(new Message("This station could not be added. Check URL",
                            MessageType.Critical));
                    }
                    break;
                case AvailableActions.PlayerAction.VolumeDown:
                    _soundEngine.VolumeDown();
                    break;
                case AvailableActions.PlayerAction.VolumeUp:
                    _soundEngine.VolumeUp();
                    break;
                case AvailableActions.PlayerAction.Next:
                    _soundEngine.Play(_stationManager.Next());
                    break;
                case AvailableActions.PlayerAction.Previous:
                    _soundEngine.Play(_stationManager.Previous());
                    break;
                case AvailableActions.PlayerAction.Pause:
                    _soundEngine.Pause(_stationManager.PlayingStation);
                    break;
                case AvailableActions.PlayerAction.Delete:
                    _stationManager.DeleteStation(_displayEngine.DeleteStation(_stationManager.Stations));
                    break;
            }
        } while (playerAction != AvailableActions.PlayerAction.Quit);

        ApplicationDataHandler.Save(_stationManager);
    }
}
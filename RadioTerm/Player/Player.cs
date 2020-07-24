using System;
using RadioTerm.Helpers;
using RadioTerm.IO;
using RadioTerm.Renderer;

namespace RadioTerm.Player
{
    public sealed class Player
    {
        private readonly IPlayerRenderEngine _rendererEngine;
        private readonly IPlayerIOEngine _ioEngine;
        private readonly PlayerEngine _playerEngine;

        private bool _isQuitRequested;

        public Player(IPlayerRenderEngine rendererEngine, IPlayerIOEngine ioEngine)
        {
            _rendererEngine = rendererEngine ?? throw new ArgumentNullException(nameof(rendererEngine));
            _ioEngine = ioEngine ?? throw new ArgumentNullException(nameof(rendererEngine));
            _playerEngine = new PlayerEngine(StationsConfiguration.Load());
            _isQuitRequested = false;
        }

        public void Run()
        {
            _playerEngine.PlayLastActive();
            do
            {
                _rendererEngine.Draw(_playerEngine.StationManager.Stations);
                Handle(NextPlayerAction());
            }
            while (!_isQuitRequested);
            StationsConfiguration.Save(_playerEngine.StationManager);

            #region Local function
            PlayerAction NextPlayerAction() => Console.ReadKey().ToPlayerAction();
            #endregion
        }

        private void Handle(PlayerAction action)
        {
            switch (action)
            {
                case PlayerAction.Quit:
                    _isQuitRequested = true;
                    break;
                case PlayerAction.Add:
                    var (name, url) = _ioEngine.RunAddStationDialog();
                    var success = _playerEngine.StationManager.AddStation(name, url);
                    if (!success)
                        _ioEngine.OutputMessage("The station you tried to add cannot be played. Check the url", MessageKind.Error);
                    break;
                case PlayerAction.VolumeDown:
                    _playerEngine.VolumeDown();
                    break;
                case PlayerAction.VolumeUp:
                    _playerEngine.VolumeUp();
                    break;
                case PlayerAction.Next:
                    _playerEngine.Next();
                    break;
                case PlayerAction.Previous:
                    _playerEngine.Previous();
                    break;
                case PlayerAction.Pause:
                    _playerEngine.Pause();
                    break;
                case PlayerAction.Delete:
                    var deletingStationId = _ioEngine.RunStationDeleteDialog(_playerEngine.StationManager.Stations);
                    if (deletingStationId.HasValue)
                        _playerEngine.StationManager.DeleteStation(deletingStationId.Value);
                    break;
                case PlayerAction.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action));
            }
        }
    }
}
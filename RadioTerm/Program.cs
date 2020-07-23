using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RadioTerm
{
    class Program
    {
        static void Main(string[] args)
        {
            StationManager stationManager = LoadUpAndEnd.Load("stations.json");
            stationManager.Reset();
            DisplayEngine engine = new DisplayEngine();
            Player player = new Player();
            AvailableActions.PlayerAction k;
            bool run = true;
            do
            {
                if (!IsThereAStation(stationManager))
                {
                    stationManager.AddStation(engine.AddStationMenu());
                }
                engine.DrawMain(stationManager.Stations);
                k = Console.ReadKey().ToPlayerAction();
                switch (k)
                {
                    case AvailableActions.PlayerAction.Quit:
                        run = false;
                        break;
                    case AvailableActions.PlayerAction.Add:
                        stationManager.AddStation(engine.AddStationMenu());
                        engine.DrawMain(stationManager.Stations);
                        break;
                    case AvailableActions.PlayerAction.VolumeDown:
                        player.VolumeDown();
                        break;
                    case AvailableActions.PlayerAction.VolumeUp:
                        player.VolumeUp();
                        break;
                    case AvailableActions.PlayerAction.Next:
                        var next = stationManager.Next();
                        player.Play(next);
                        break;
                    case AvailableActions.PlayerAction.Previous:
                        var prev = stationManager.Previous();
                        player.Play(prev);
                        break;
                    case AvailableActions.PlayerAction.Pause:
                        if (stationManager.PlayingStation != null)
                        {
                            stationManager.ToggleActive();
                            player.Pause();
                        }   
                        break;
                    case AvailableActions.PlayerAction.Delete:
                        stationManager.DeleteStation(engine.DeleteStationMenu(stationManager.Stations));
                        engine.DrawMain(stationManager.Stations);
                        break;
                    default:
                        break;
                }

            } while (run);
            LoadUpAndEnd.Save(stationManager, "stations.json");
        }

        private static bool IsThereAStation(StationManager st)
        {
            if (st.Stations.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}

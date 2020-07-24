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
            DisplayEngine engine = new DisplayEngine();
            Player player = new Player(LoadUpAndEnd.Load());
            AvailableActions.PlayerAction k;
            bool run = true;            
            player.PlayLastActive();
            do
            {
                engine.DrawMain(player.RadioStationManager.Stations);
                k = Console.ReadKey().ToPlayerAction();
                switch (k)
                {
                    case AvailableActions.PlayerAction.Quit:
                        run = false;
                        break;
                    case AvailableActions.PlayerAction.Add:
                        var added = player.RadioStationManager.AddStation(engine.AddStationMenu());
                        if (!added)
                        {
                            engine.ShowAddingError();
                        }
                        break;
                    case AvailableActions.PlayerAction.VolumeDown:
                        player.VolumeDown();
                        break;
                    case AvailableActions.PlayerAction.VolumeUp:
                        player.VolumeUp();
                        break;
                    case AvailableActions.PlayerAction.Next:
                        player.Next();
                        break;
                    case AvailableActions.PlayerAction.Previous:
                        player.Previous();
                        break;
                    case AvailableActions.PlayerAction.Pause:
                        player.Pause();
                        break;
                    case AvailableActions.PlayerAction.Delete:
                        player.RadioStationManager.DeleteStation(engine.DeleteStationMenu(player.RadioStationManager.Stations));                       
                        break;
                    default:
                        break;
                }

            } while (run);
            LoadUpAndEnd.Save(player.RadioStationManager);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace RadioTerm
{
    public class DisplayEngine
    {

        private void DrawHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Welcome to RadioTerm");
            DrawBar();
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }

        private void DrawStations(IEnumerable<Station> sts)
        {
            foreach (var station in sts)
            {
                if(station.Active)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Now playing {station.Name}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(station.Name);
                }
            }
        }
        private void DrawBar()
        {
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.Write("-");
            }
        }

        public void ShowAddingError()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The station you tried to add cannot be played. Check the url");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void DrawFooter()
        {
            int bot = Console.WindowHeight;
            Console.SetCursorPosition(0, bot - 3);
            DrawBar();
            foreach (var corr in AvailableActions.Correspondence)
            {
                Console.Write($"{corr.Value} / {corr.Key}   ");
            }
        }

        public void DrawMain(IEnumerable<Station> stations)
        {
            Console.Clear();
            DrawHeader();
            DrawStations(stations);
            DrawFooter();
        }

        public (string name,string url) AddStationMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Add a radio");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("New Station name: ");
            string name = Console.ReadLine();
            Console.Write("New Station Url: ");
            string url = Console.ReadLine();
            return (name, url);

        }
        public string DeleteStationMenu(IEnumerable<Station> stations)
        {
            var stationList = stations.ToList();
            var activeStation = stationList.Where(s => s.Active).FirstOrDefault();
            if (activeStation == null)
            {
                return "";
            }
            stationList.Remove(activeStation);
            int index = 0;
            ConsoleKeyInfo k = new ConsoleKeyInfo() ;
            do
            {
                Console.Clear();
                if (k.Key == ConsoleKey.UpArrow)
                {
                    if(index == 0)
                    {
                        index = stations.Count() - 1;
                    }
                    else
                    {
                        index--;
                    }
                }
                else if(k.Key == ConsoleKey.DownArrow)
                {
                    if(index == stationList.Count() - 1)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
                for (int i = 0; i < stationList.Count(); i++)
                {
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.WriteLine(stationList[i].Name);
                }
                k = Console.ReadKey();
            } while (k.Key != ConsoleKey.Enter);
            return stationList[index].Name;
        }

    }
}

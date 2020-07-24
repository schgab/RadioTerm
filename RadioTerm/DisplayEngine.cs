using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            string title = "Welcome to RadioTerm";
            WriteToCenter(title,1);
            DrawBar(2,title.Length +18);
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }

        private void DrawStations(IEnumerable<Station> sts)
        {
            for (int i = 0; i < sts.Count(); i++)
            {
                var station = sts.ElementAt(i);
                if (station.Active)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Title = $"Playing {station.Name}";
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                WriteToCenter(station.Name, i + 4);
                Console.ForegroundColor = ConsoleColor.Gray;

            }
        }
        private void DrawBar()
        {
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.Write("-");
            }
        }
        private void DrawBar(int row, int length)
        {
            if (length % 2 == 0)
            {
                length++;
            }
            int middle = Console.WindowWidth / 2;
            Console.SetCursorPosition(middle - (int)Math.Ceiling(length / 2.0), row);
            for (int i = 0; i <= length; i++)
            {
                Console.Write("-");
            }
        }

        private void DrawBar(int row)
        {
            Console.SetCursorPosition(0, row);
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
            int bot = Console.WindowHeight - 3;
            DrawBar(bot);
            string footer = "";
            foreach (var corr in AvailableActions.Correspondence)
            {
                footer += $"{corr.Value} / {corr.Key}   ";
            }
            WriteToCenter(footer, bot+1);

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

        private void WriteToCenter(string st, int row)
        {
            int middle = Console.WindowWidth / 2 - st.Length / 2;
            Console.SetCursorPosition(middle, row);
            Console.WriteLine(st);

        }

    }
}

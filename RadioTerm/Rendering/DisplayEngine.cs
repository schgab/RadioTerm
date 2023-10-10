using System;
using System.Collections.Generic;
using System.Linq;
using RadioTerm.Helpers;
using RadioTerm.Player;
using RadioTerm.Rendering.Message;

namespace RadioTerm.Rendering;

public sealed class DisplayEngine : IDisplayEngine
{
    private void DrawHeader()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        string title = "Welcome to RadioTerm";
        WriteToCenter(title, 1);
        DrawBar(2, title.Length + 18);
        Console.WriteLine();
        Console.WriteLine();
        Console.ResetColor();
    }

    private void DrawStations(IEnumerable<Station> stations)
    {
        var enumeratedStations = stations.ToList();
        for (var i = 0; i < enumeratedStations.Count; i++)
        {
            var station = enumeratedStations[i];
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

    private void DrawFooter()
    {
        int bot = Console.WindowHeight - 3;
        DrawBar(bot);
        var footer = "";
        foreach (var (action, keyBoardKey) in AvailableActions.Mapping)
        {
            footer += $"{keyBoardKey} | {action}   ";
        }

        WriteToCenter(footer, bot + 1);
    }

    public void Draw(IEnumerable<Station> stations)
    {
        Console.Clear();
        DrawHeader();
        DrawStations(stations);
        DrawFooter();
    }

    public (string name, string url) AddStation()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Add a radio");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("New Station name: ");
        var name = Console.ReadLine();
        Console.Write("New Station Url: ");
        var url = Console.ReadLine();
        return (name, url);
    }

    public Guid DeleteStation(IEnumerable<Station> stations)
    {
        var stationList = stations.ToList();
        var selectedStation = stationList[0];
        var k = new ConsoleKeyInfo();
        do
        {
            Console.Clear();
            if (k.Key == ConsoleKey.UpArrow)
            {
                selectedStation = stationList.Previous(selectedStation);
            }
            else if (k.Key == ConsoleKey.DownArrow)
            {
                selectedStation = stationList.Next(selectedStation);
            }

            foreach (var st in stationList)
            {
                var tobePrinted = st.Name;
                if (st.Active)
                {
                    tobePrinted = $"{st.Name} [Playing]";
                }

                if (st == selectedStation)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }

                Console.WriteLine(tobePrinted);
                Console.ResetColor();
            }

            k = Console.ReadKey();
        } while (k.Key != ConsoleKey.Enter);

        return selectedStation.Id;
    }

    private void WriteToCenter(string st, int row)
    {
        int middle = Console.WindowWidth / 2 - st.Length / 2;
        Console.SetCursorPosition(middle, row);
        Console.WriteLine(st);
    }

    public void ShowMessage(IMessage message)
    {
        Console.ResetColor();
        Console.Clear();
        switch (message.Type)
        {
            case MessageType.Info:
                Console.WriteLine(message.MessageString);
                break;
            case MessageType.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message.MessageString);
                break;
            case MessageType.Critical:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message.MessageString);
                break;
        }

        Console.ResetColor();
        Console.ReadLine();
    }
}
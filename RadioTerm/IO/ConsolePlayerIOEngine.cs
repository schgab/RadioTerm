using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RadioTerm.Player;
using RadioTerm.Renderer;

namespace RadioTerm.IO
{
    public sealed class ConsolePlayerIOEngine : IPlayerIOEngine
    {
        private readonly IPlayerRenderEngine _renderer;

        public ConsolePlayerIOEngine(IPlayerRenderEngine renderer)
            => _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));

        public (string name, string url) RunAddStationDialog()
        {
            OutputMessage("New station name: ", MessageKind.Warning, withNewLine: false);
            var name = Console.ReadLine();

            OutputMessage("New station url: ", MessageKind.Warning, withNewLine: false);
            var url = Console.ReadLine();

            return (name, url);
        }

        public int? RunStationDeleteDialog(IEnumerable<Station> stations)
        {
            var stationsArray = stations?.ToArray() ?? Array.Empty<Station>();
            if (stationsArray.Length == 0)
                return null;

            var stationsEnumerable = new StationsCyclicEnumerable(stationsArray, acceptKey: ConsoleKey.Enter, ConsoleKey.Escape);

            OutputAllStations(stationsEnumerable.Stations.First());

            foreach (var currentStation in stationsEnumerable)
            {
                OutputAllStations(currentStation);
            }

            return stationsEnumerable.LastSelected?.Id;

            #region Local function
            void OutputAllStations(Station highlightStation)
            {
                Console.Clear();
                foreach (var entry in stationsEnumerable.Stations)
                {
                    Console.ForegroundColor = entry == highlightStation ? ConsoleColor.DarkYellow : ConsoleColor.Gray;
                    Console.WriteLine("{0}{1}", entry.Name, entry.Active ? " [Playing]" : string.Empty);
                }
            }
            #endregion
        }

        public void OutputMessage(string text, MessageKind kind, bool withNewLine = true)
            => _renderer.DrawMessage(new Message(text, kind, withNewLine));

        private sealed class StationsCyclicEnumerable : IEnumerable<Station>
        {
            public readonly IReadOnlyCollection<Station> Stations;

            private readonly ConsoleKey _acceptKey;

            private readonly ConsoleKey _cancelKey;

            public StationsCyclicEnumerable(Station[] stations, ConsoleKey acceptKey, ConsoleKey cancelKey)
            {
                Stations = stations ?? throw new ArgumentNullException(nameof(stations));
                _acceptKey = acceptKey;
                _cancelKey = cancelKey;
            }

            public Station LastSelected { get; private set; }

            public IEnumerator<Station> GetEnumerator()
                => new ConsoleCyclicalStationsEnumerator(this, Stations.ToArray(), _acceptKey, _cancelKey);

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();

            private sealed class ConsoleCyclicalStationsEnumerator : IEnumerator<Station>
            {
                private readonly StationsCyclicEnumerable _enumerable;

                private readonly Station[] _stations;

                private readonly ConsoleKey _acceptKey;

                private readonly ConsoleKey _cancelKey;

                private ConsoleKeyInfo _userInput;

                public ConsoleCyclicalStationsEnumerator(StationsCyclicEnumerable enumerable, Station[] stations,
                    ConsoleKey acceptKey, ConsoleKey cancelKey)
                {
                    _enumerable = enumerable;
                    _stations = stations;
                    _acceptKey = acceptKey;
                    _cancelKey = cancelKey;
                }

                public int CurrentIndex { get; private set; }

                public void Dispose() {}

                public bool MoveNext()
                {
                    _userInput = Console.ReadKey();

                    if (_userInput.Key == _acceptKey)
                        return false;

                    if (_userInput.Key == _cancelKey)
                    {
                        Reset();
                        return false;
                    }

                    _enumerable.LastSelected = Current = _stations[GetNextIndex(_userInput.Key)];
                    return true;
                }

                public void Reset()
                {
                    CurrentIndex = 0;
                    _enumerable.LastSelected = Current = null;
                }

                public Station Current { get; private set; }

                object IEnumerator.Current => Current;

                private int GetNextIndex(ConsoleKey key)
                {
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            return CurrentIndex= Mod(CurrentIndex - 1, _stations.Length);
                        case ConsoleKey.DownArrow:
                            return CurrentIndex = Mod(CurrentIndex + 1, _stations.Length);
                        default:
                            return CurrentIndex;
                    }

                    #region Local function
                    // (-1 % 5) => -1; Mod(-1, 5) => 4
                    int Mod(int x, int m) => (x % m + m) % m;
                    #endregion
                }
            }
        }
    }
}
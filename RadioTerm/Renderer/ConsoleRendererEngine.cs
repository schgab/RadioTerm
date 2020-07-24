using System;
using System.Collections.Generic;
using System.Linq;
using RadioTerm.Helpers;
using RadioTerm.Player;

namespace RadioTerm.Renderer
{
    public sealed class ConsoleRendererEngine : IPlayerRenderEngine
    {
        private static readonly string ActionsDescription = PlayerActionHelper.GetActionsDescription();

        public void Draw(IEnumerable<Station> stations)
        {
            Console.Clear();
            DrawHeader();
            DrawStations(stations);
            DrawFooter();
        }

        public void DrawMessage(IMessage message)
        {
            switch (message.Kind)
            {
                case MessageKind.Info:
                    RenderMessage(message, ConsoleColor.Cyan);
                    break;
                case MessageKind.Warning:
                    RenderMessage(message, ConsoleColor.Yellow);
                    break;
                case MessageKind.Error:
                    RenderMessage(message, ConsoleColor.Red);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(message.Kind));
            }

            #region Local functions
            void RenderMessage(IMessage msg, ConsoleColor color)
            {
                Console.Clear();
                Console.ForegroundColor = color;
                Console.Write(msg.Text);
                if (msg.WithNewLine)
                    Console.WriteLine();
                Console.ResetColor();
            }
            #endregion
        }

        private void DrawHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            const string title = "Welcome to RadioTerm";
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

        private void DrawFooter()
        {
            int bot = Console.WindowHeight - 3;
            DrawBar(bot);
            WriteToCenter(ActionsDescription, bot+1);
        }

        private void WriteToCenter(string st, int row)
        {
            int middle = Console.WindowWidth / 2 - st.Length / 2;
            Console.SetCursorPosition(middle, row);
            Console.WriteLine(st);
        }
    }
}

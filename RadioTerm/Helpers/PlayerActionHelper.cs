using System;
using System.Collections.Generic;
using System.Text;
using RadioTerm.Player;

namespace RadioTerm.Helpers
{
    public static class PlayerActionHelper
    {
        private static readonly Dictionary<char, PlayerAction> Correspondence = new Dictionary<char, PlayerAction>
        {
            { 'a', PlayerAction.Add },
            { 'd', PlayerAction.Delete },
            { 'p', PlayerAction.Previous },
            { 'n', PlayerAction.Next },
            { '+', PlayerAction.VolumeUp },
            { '-', PlayerAction.VolumeDown },
            { ' ', PlayerAction.Pause },
            { 'q', PlayerAction.Quit }
        };

        public static string GetActionsDescription()
        {
            var builder = new StringBuilder();
            foreach (var action in Correspondence)
            {
                builder.AppendFormat("[ {0} : {1} ] ", char.IsWhiteSpace(action.Key) ? '\u2423' : action.Key, action.Value);
            }

            return builder.ToString();
        }

        public static PlayerAction ToPlayerAction(this ConsoleKeyInfo userInput)
        {
            if (char.IsWhiteSpace(userInput.KeyChar))
                return PlayerAction.Pause;

            return Correspondence.ContainsKey(userInput.KeyChar)
                ? Correspondence[userInput.KeyChar]
                : PlayerAction.None;
        }
    }
}

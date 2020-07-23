using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioTerm
{
    public static class AvailableActions
    {
        public enum PlayerAction
        {
            Quit,
            Add,
            VolumeDown,
            VolumeUp,
            Next,
            Previous,
            Pause,
            Delete
        }

        public static Dictionary<PlayerAction, string> Correspondence = new Dictionary<PlayerAction, string>()
        {
            {PlayerAction.Quit, "q" },
            {PlayerAction.Add, "a" },
            {PlayerAction.VolumeDown, "-" },
            {PlayerAction.VolumeUp, "+" },
            {PlayerAction.Next, "n" },
            {PlayerAction.Previous, "p" },
            {PlayerAction.Pause, "space" },
            {PlayerAction.Delete, "d" },
        };

        public static PlayerAction ToPlayerAction(this ConsoleKeyInfo info)
        {
            switch (info.KeyChar)
            {
                case ' ':
                    return PlayerAction.Pause;
                default:
                    break;
            }
            return Correspondence.FirstOrDefault(s => s.Value[0] == info.KeyChar).Key;
        }

        

    }
}

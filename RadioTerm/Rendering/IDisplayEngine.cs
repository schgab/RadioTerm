using RadioTerm.Player;
using RadioTerm.Rendering.Message;
using System.Collections.Generic;

namespace RadioTerm.Rendering
{
    public interface IDisplayEngine
    {
        void Draw(IEnumerable<Station> stations);
        int DeleteStation(IEnumerable<Station> stations);

        void ShowMessage(IMessage message);

        (string name, string url) AddStation();
    }
}

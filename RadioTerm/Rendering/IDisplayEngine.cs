using System;
using System.Collections.Generic;
using RadioTerm.Player;
using RadioTerm.Rendering.Message;

namespace RadioTerm.Rendering;

public interface IDisplayEngine
{
    void Draw(IEnumerable<Station> stations);
    Guid DeleteStation(IEnumerable<Station> stations);

    void ShowMessage(IMessage message);

    (string name, string url) AddStation();
}
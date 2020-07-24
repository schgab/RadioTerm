using System.Collections.Generic;
using RadioTerm.Player;
using RadioTerm.Renderer;

namespace RadioTerm.IO
{
    public interface IPlayerIOEngine
    {
        (string name, string url) RunAddStationDialog();

        int? RunStationDeleteDialog(IEnumerable<Station> stations);

        void OutputMessage(string text, MessageKind kind, bool withNewLine = true);
    }
}
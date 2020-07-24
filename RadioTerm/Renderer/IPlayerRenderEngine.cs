using System.Collections.Generic;
using RadioTerm.Player;

namespace RadioTerm.Renderer
{
    public interface IPlayerRenderEngine
    {
        void Draw(IEnumerable<Station> stations);

        void DrawMessage(IMessage message);
    }
}
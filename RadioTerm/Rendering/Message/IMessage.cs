using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioTerm.Rendering.Message
{
    public interface IMessage
    {
        string MessageString { get; }

        MessageType Type { get; }
    }
}

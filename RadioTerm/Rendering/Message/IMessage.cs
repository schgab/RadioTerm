namespace RadioTerm.Rendering.Message
{
    public interface IMessage
    {
        string MessageString { get; }

        MessageType Type { get; }
    }
}

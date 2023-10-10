namespace RadioTerm.Rendering.Message;

public sealed class Message : IMessage
{
    public MessageType Type { get; }

    public string MessageString { get; }

    public Message(string messageString, MessageType type)
    {
        Type = type;
        MessageString = messageString;
    }
}
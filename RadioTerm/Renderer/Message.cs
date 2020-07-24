namespace RadioTerm.Renderer
{
    public sealed class Message : IMessage
    {
        public Message(string text, MessageKind kind, bool withNewLine)
        {
            Text = text ?? string.Empty;
            Kind = kind;
            WithNewLine = withNewLine;
        }

        public string Text { get; }

        public MessageKind Kind { get; }

        public bool WithNewLine { get; }
    }
}
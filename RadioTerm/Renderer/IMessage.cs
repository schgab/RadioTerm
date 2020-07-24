namespace RadioTerm.Renderer
{
    public interface IMessage
    {
        string Text { get; }

        MessageKind Kind { get; }

        bool WithNewLine { get; }
    }
}
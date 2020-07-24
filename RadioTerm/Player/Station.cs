namespace RadioTerm.Player
{
    public sealed class Station
    {
        public int Id { get; }

        public string Name { get; }

        public string Url { get; }

        public bool Active { get; set; }

        public Station(int id, string name, string url)
        {
            Id = id;
            Name = name;
            Url = url;
        }
    }
}

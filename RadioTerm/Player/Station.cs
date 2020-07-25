namespace RadioTerm.Player
{
    public class Station
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }

        public int DefiniteId { get; }
        public Station(string name, string url, int definiteid)
        {
            Name = name;
            Url = url;
            DefiniteId = definiteid;
        }
    }
}

using System.Collections.Generic;
using RadioTerm.Player;

namespace RadioTerm.Helpers
{
    public static class ListExtensions
    {
        public static Station Next(this List<Station> stations, Station current)
        {
            var index = stations.IndexOf(current);
            return stations[++index == stations.Count? 0 : index];
        }

        public static Station Previous(this List<Station> stations, Station current)
        {
            var index = stations.IndexOf(current);
            return stations[--index < 0 ? stations.Count - 1 : index];
        }
    }
}

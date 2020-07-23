﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RadioTerm
{
    public static class Extensions
    {
        public static Station Next(this List<Station> stations, Station current)
        {
            int index = stations.IndexOf(current);
            index++;
            if (index == stations.Count)
            {
                index = 0;
            }
            return stations[index];
        }
        public static Station Previous(this List<Station> stations, Station current)
        {
            int index = stations.IndexOf(current);
            index--;
            if (index < 0)
            {
                index = stations.Count - 1;
            }
            return stations[index];
        }
    }
}

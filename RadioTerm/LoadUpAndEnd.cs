﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RadioTerm
{
    public static class LoadUpAndEnd
    {
        public static void Save(object obj, string path)
        {
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(path, jsonString);
        }

        private static T ReadJsonObject<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }

        public static StationManager Load(string path)
        {
            if (File.Exists(path))
            {
                var manager = ReadJsonObject<StationManager>(path);
                //Fix reference to object in list
                manager.PlayingStation = manager.Stations.Where(c => c.Url == manager.PlayingStation.Url).FirstOrDefault();
                return manager;
            }
            else
            {
                return new StationManager();
            }
        }
    }
}

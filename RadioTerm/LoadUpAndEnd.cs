using Newtonsoft.Json;
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

        private static string ApplicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) , "RadioTerm");
        private static string StationsJsonPath = Path.Combine(ApplicationFolder ,"stations.json");

        public static void Save(object obj)
        {
            if (!Directory.Exists(ApplicationFolder))
            {
                Directory.CreateDirectory(ApplicationFolder);
            }
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(StationsJsonPath, jsonString);
        }

        private static T ReadJsonObject<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }

        public static StationManager Load()
        {
            if (File.Exists(StationsJsonPath))
            {
                var manager = ReadJsonObject<StationManager>(StationsJsonPath);
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

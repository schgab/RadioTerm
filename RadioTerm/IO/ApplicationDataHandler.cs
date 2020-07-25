using Newtonsoft.Json;
using RadioTerm.Player;
using System;
using System.IO;
using System.Linq;


namespace RadioTerm.IO
{
    public static class ApplicationDataHandler
    {

        private static string ApplicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RadioTerm");
        private static string StationsJsonPath = Path.Combine(ApplicationFolder, "stations.json");

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
                return manager;
            }
            else
            {
                return new StationManager();
            }
        }
    }
}

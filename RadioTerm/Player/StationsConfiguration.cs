using System;
using System.IO;
using Newtonsoft.Json;

namespace RadioTerm.Player
{
    public static class StationsConfiguration
    {
        private static readonly string ApplicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) , "RadioTerm");
        private static readonly string StationsJsonPath = Path.Combine(ApplicationFolder ,"stations.json");

        public static void Save(object obj)
        {
            if (!Directory.Exists(ApplicationFolder))
                Directory.CreateDirectory(ApplicationFolder);

            File.WriteAllText(StationsJsonPath, JsonConvert.SerializeObject(obj, Formatting.Indented));
        }

        private static T ReadJsonObject<T>(string path) => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

        public static StationManager Load()
            => !File.Exists(StationsJsonPath) ? new StationManager() : ReadJsonObject<StationManager>(StationsJsonPath);
    }
}

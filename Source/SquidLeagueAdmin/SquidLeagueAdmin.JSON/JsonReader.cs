using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SquidLeagueAdmin.JSON
{
    public class JsonReader
    {
        /// <summary>
        /// Read the json file in the specified path and convert it into T
        /// </summary>
        /// <typeparam name="T">The type of the model to convert the json to</typeparam>
        /// <param name="path">The path to the json file</param>
        /// <returns>Enumerable of type T of the deserialised json</returns>
        public T Read<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException("The file cannot be found in the reaource folder.");
            }

            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Writes an object to a file in json format.
        /// </summary>
        /// <typeparam name="T">The type of the item to save</typeparam>
        /// <param name="item">The item to convert</param>
        /// <param name="path">The path to the file</param>
        public void Write<T>(T item, string path)
        {
            string json = JsonConvert.SerializeObject(item, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}

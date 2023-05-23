using System.Text;

using Newtonsoft.Json;

using AnimalEditor.Model.Animals;
using AnimalEditor.Model.Serialization.SerializableAnimals;


namespace AnimalEditor.Model.Serialization
{
    internal class JsonSerializer : ISerializer

    {
        public void Serialize(List<Animal> animals, string fileName)
        {
            var animalsToSerialize = SerializeManager.ListToSerializableList(animals);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            var json = JsonConvert.SerializeObject(animalsToSerialize, settings);

            var file = File.CreateText(fileName);
            file.Dispose();

            File.WriteAllText(fileName, json);
        }

        public MemoryStream Serialize(List<Animal> animals)
        {
            var animalsToSerialize = SerializeManager.ListToSerializableList(animals);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            var json = JsonConvert.SerializeObject(animalsToSerialize, settings);

            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }

        public List<Animal> Deserialize(string fileName)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            var jsonStrings = File.ReadAllLines(fileName);
            var jsonText = string.Join("", jsonStrings);

            var serializedAnimals = JsonConvert.DeserializeObject<List<SerializableAnimal>>(jsonText, settings);

            var animals = SerializeManager.SerializableListToList(serializedAnimals);

            return animals;
        }

        public List<Animal> Deserialize(MemoryStream serializedStream)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            var jsonText = Encoding.UTF8.GetString(serializedStream.ToArray());

            var serializedAnimals = JsonConvert.DeserializeObject<List<SerializableAnimal>>(jsonText, settings);

            var animals = SerializeManager.SerializableListToList(serializedAnimals);

            return animals;
        }

        public string GetExtension()
        {
            return ".json";
        }
    }
}

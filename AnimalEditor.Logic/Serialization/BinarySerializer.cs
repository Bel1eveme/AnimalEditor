using System.Runtime.Serialization.Formatters.Binary;

using AnimalEditor.Model.Animals;
using AnimalEditor.Model.Serialization.SerializableAnimals;

namespace AnimalEditor.Model.Serialization
{
    internal class BinarySerializer : ISerializer
    {
        [Obsolete]
        public void Serialize(List<Animal> animals, string fileName)
        {
            var formatter = new BinaryFormatter();
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fileStream, SerializeManager.ListToSerializableList(animals));
            }
        }

        [Obsolete]
        public MemoryStream Serialize(List<Animal> animals)
        {
            var formatter = new BinaryFormatter();
            var serializedStream = new MemoryStream();

            formatter.Serialize(serializedStream, SerializeManager.ListToSerializableList(animals));

            serializedStream.Position = 0;
            return serializedStream;
        }

        [Obsolete]
        public List<Animal> Deserialize(string fileName)
        {
            var formatter = new BinaryFormatter();
            using var fileStream = new FileStream(fileName, FileMode.Open);
            return SerializeManager.SerializableListToList((List<SerializableAnimal>)formatter.Deserialize(fileStream));
        }

        [Obsolete]
        public List<Animal> Deserialize(MemoryStream serializedStream)
        {
            var formatter = new BinaryFormatter();
            return SerializeManager.SerializableListToList((List<SerializableAnimal>)formatter.Deserialize(serializedStream));
        }

        public string GetExtension()
        {
            return ".bin";
        }
    }
}

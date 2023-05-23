using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model.Serialization
{
    public interface ISerializer
    {
        void Serialize(List<Animal> animals, string fileName);

        MemoryStream Serialize(List<Animal> animals);

        List<Animal> Deserialize(string fileName);

        List<Animal> Deserialize(MemoryStream serializedStream);

        string GetExtension();

    }
}

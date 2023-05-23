using System.Text.Json.Serialization;

using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model.Serialization.SerializableAnimals
{
    [Serializable]
    [JsonDerivedType(typeof(Cat))]
    [JsonDerivedType(typeof(Dog))]
    [JsonDerivedType(typeof(Stork))]
    [JsonDerivedType(typeof(Owl))]
    public abstract class SerializableAnimal
    {
        public DateTime BirthDate { get; set; }

        protected SerializableAnimal()
        {
            BirthDate = DateOnly.FromDateTime(DateTime.Now).ToDateTime(TimeOnly.MinValue);
        }

        protected SerializableAnimal(Animal animal)
        {
            BirthDate = animal.BirthDate.ToDateTime(TimeOnly.MinValue);
        }
    }
}

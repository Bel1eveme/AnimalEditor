using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model.Serialization.SerializableAnimals
{


    [Serializable]
    public class SerializableDog : SerializableMammal
    {
        public DogBreed Breed { get; set; }

        public string Name { get; set; } = string.Empty;

        public SerializableDog() { }

        public SerializableDog(Dog dog) : base(dog)
        {
            Breed = dog.Breed;
            Name = dog.Name;
        }
    }
}

using System.Text.Json.Serialization;

using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model.Serialization.SerializableAnimals
{
    [Serializable]
    [JsonDerivedType(typeof(Stork))]
    [JsonDerivedType(typeof(Owl))]
    public abstract class SerializableBird : SerializableAnimal
    {
        public SerializableNest CurrentNest { get; set; }

        public int MaxFlightHeight { get; set; }

        protected SerializableBird()
        {
            CurrentNest = new SerializableNest();
        }

        protected SerializableBird(Bird bird) : base(bird)
        {
            MaxFlightHeight = bird.MaxFlightHeight;
            CurrentNest = new SerializableNest(bird.CurrentNest);
        }
    }
}

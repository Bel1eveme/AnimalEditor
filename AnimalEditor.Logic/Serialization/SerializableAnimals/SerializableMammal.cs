using System.Text.Json.Serialization;

using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model.Serialization.SerializableAnimals
{

    [Serializable]
    [JsonDerivedType(typeof(Cat))]
    [JsonDerivedType(typeof(Dog))]
    public abstract class SerializableMammal : SerializableAnimal
    {
        public int GestationAge { get; set; }

        protected SerializableMammal() { }

        protected SerializableMammal(Mammal mammal) : base(mammal)
        {
            GestationAge = mammal.GestationAge;
        }
    }
}
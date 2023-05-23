using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model.Serialization.SerializableAnimals
{

    [Serializable]
    public class SerializableCat : SerializableMammal
    {
        public CatColor Color { get; set; }

        public SerializableCat() { }

        public SerializableCat(Cat cat) : base(cat)
        {
            Color = cat.Color;
        }
    }
}

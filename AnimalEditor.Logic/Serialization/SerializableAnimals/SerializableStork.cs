using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model.Serialization.SerializableAnimals
{

    [Serializable]
    public class SerializableStork : SerializableBird
    {
        public int BeakLength { get; set; }

        public SerializableStork()
        {
            CurrentNest = new SerializableNest();
        }

        public SerializableStork(Stork stork) : base(stork)
        {
            BeakLength = stork.BeakLength;
        }
    }
}

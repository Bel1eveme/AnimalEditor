using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model.Serialization.SerializableAnimals
{

    [Serializable]
    public class SerializableNest
    {
        public int Capacity { get; set; }

        public bool IsUsed{ get; set; }

        public SerializableNest() { }

        public SerializableNest(Nest nest)
        {
            Capacity = nest.Capacity;
            IsUsed = nest.IsUsed;
        }
    }
}

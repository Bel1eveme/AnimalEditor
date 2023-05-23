using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model.Serialization.SerializableAnimals
{

    [Serializable]
    public class SerializableOwl : SerializableBird
    {
        public DateTime HuntStartTime { get; set; }

        public SerializableOwl()
        {
            CurrentNest = new SerializableNest();
        }

        public SerializableOwl(Owl owl) : base(owl)
        {
            HuntStartTime = DateTime.MinValue + owl.HuntStartTime.ToTimeSpan();
        }
    }
}

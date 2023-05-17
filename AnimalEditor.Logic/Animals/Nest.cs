namespace AnimalEditor.Model.Animals
{
    public class Nest
    {
        public int Capacity { get; set; }

        public bool IsUsed{ get; set; }

        public Nest(int capacity, bool isUsed)
        {
            Capacity = capacity;
            IsUsed = isUsed;
        }

        public Nest() { }
    }
}

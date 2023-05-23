namespace AnimalEditor.Model.Animals
{
    public class Stork : Bird
    {
        public int BeakLength { get; set; } 
        public Stork(DateOnly birthDate, int maxFlightHeight, Nest nest, int beakLength) : base(birthDate, maxFlightHeight, nest)
        {
            BeakLength = beakLength;
        }

        public Stork(DateOnly birthDate, int maxFlightHeight, int capacity, bool isUsed, int beakLength) : base(birthDate, maxFlightHeight, capacity, isUsed)
        {
            BeakLength = beakLength;
        }

        public Stork() { }
    }
}

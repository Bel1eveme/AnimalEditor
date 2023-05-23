namespace AnimalEditor.Model.Animals
{
    public class Owl : Bird
    {
        public TimeOnly HuntStartTime { get; set; }

        public Owl(DateOnly birthDate, int maxFlightHeight, Nest nest, TimeOnly huntStartTime) : base(birthDate, maxFlightHeight, nest) 
        {
            HuntStartTime = huntStartTime;
        }
        
        public Owl(DateOnly birthDate, int maxFlightHeight, int capacity, bool isUsed, TimeOnly huntStartTime) : base(birthDate, maxFlightHeight, capacity, isUsed) 
        {
            HuntStartTime = huntStartTime;
        }

        public Owl() { }
    }
}

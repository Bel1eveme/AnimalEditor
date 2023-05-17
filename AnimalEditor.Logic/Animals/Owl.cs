namespace AnimalEditor.Model.Animals
{
    internal class Owl : Bird
    {
        public TimeOnly HuntStartTime { get; set; }


        public Owl(DateOnly birthDate, int maxFlightHeight, Nest nest, TimeOnly huntStartTime) : base(birthDate, maxFlightHeight, nest) 
        {
            HuntStartTime = huntStartTime;
        }
    }
}

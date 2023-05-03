namespace AnimalEditor.Model.Animals
{
    internal class Owl : Bird
    {
        public TimeOnly HuntStartTime { get; set; }

        public bool CanFly { get; set; }

        public Owl(DateOnly birthDate, int maxFlightHeight, Nest nest, TimeOnly huntStartTime, bool canFly) : base(birthDate, maxFlightHeight, nest) 
        {
            HuntStartTime = huntStartTime;        
            CanFly = canFly; 
        }
    }
}

namespace AnimalEditor.Model.Animals
{
    internal class Owl : Bird
    {
        public (TimeOnly, TimeOnly) HuntPeriod { get; set; }

        public bool CanFly { get; set; }

        public Owl(DateOnly birthDate, int maxFlightHeight, Nest nest, (TimeOnly, TimeOnly) huntPeriod, bool canFly) : base(birthDate, maxFlightHeight, nest) 
        {
            HuntPeriod = huntPeriod;        
            CanFly = canFly; 
        }
    }
}

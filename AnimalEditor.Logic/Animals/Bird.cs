namespace AnimalEditor.Model.Animals
{
    public abstract class Bird : Animal
    {
        public Nest CurrentNest { get; set; }

        public int MaxFlightHeight { get; set; }

        protected Bird(DateOnly birthDate, int maxFlightHeight, Nest nest) : base(birthDate)
        {
            MaxFlightHeight = maxFlightHeight;
            CurrentNest = nest; 
        }

        protected Bird()
        {
            CurrentNest = new Nest();
        }
    }
}

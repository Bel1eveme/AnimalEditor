namespace AnimalEditor.Model.Animals
{
    public abstract class Mammal : Animal
    {
        public int GestationAge { get; set; }

        protected Mammal(DateOnly birthDate, int gestationAge) : base(birthDate)
        {
            GestationAge = gestationAge;
        }

        protected Mammal() { }
    }
}
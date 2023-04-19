namespace AnimalEditor.Model.Animals
{
    public abstract class Animal
    {
        public DateOnly BirthDate { get; set; }

        protected Animal(DateOnly birthDate) => BirthDate = birthDate; 
    }
}

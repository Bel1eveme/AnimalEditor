namespace AnimalEditor.Model.Animals
{
    public enum DogBreed
    {
        Sheepdog,
        Bulldog,
        Terrier,
        Spaniel
    }
    public class Dog : Mammal
    {
        public DogBreed Breed { get; set; }

        public Dog(DateOnly birthDate, int gestationAge, DogBreed breed) : base(birthDate, gestationAge)
        {
            Breed = breed;
        }

        public Dog() { }
    }
}

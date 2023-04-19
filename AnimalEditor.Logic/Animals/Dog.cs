namespace AnimalEditor.Model.Animals
{
    internal enum DogBreed
    {
        Sheepdog,
        Bulldog,
        Terrier,
        Spaniel
    }
    internal class Dog : Mammal
    {
        public DogBreed Breed { get; set; }

        public Dog(DateOnly birthDate, int gestationAge, DogBreed breed) : base(birthDate, gestationAge)
        {
            Breed = breed;
        }
    }
}

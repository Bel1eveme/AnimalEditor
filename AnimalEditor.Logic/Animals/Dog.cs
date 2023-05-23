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
        public string Name { get; set; } = string.Empty; 
        public DogBreed Breed { get; set; }

        public Dog(DateOnly birthDate, int gestationAge, DogBreed breed, string name) : base(birthDate, gestationAge)
        {
            Breed = breed;
            Name = name;
        }

        public Dog() { }
    }
}

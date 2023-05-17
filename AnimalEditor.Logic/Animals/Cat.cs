namespace AnimalEditor.Model.Animals
{
    public enum CatColor
    {
        Black,
        White,
        Brown, 
        Red
    }
    public class Cat : Mammal
    {
        public CatColor Color { get; set; }     

        public Cat(DateOnly birthDate, int gestationAge, CatColor color) : base(birthDate, gestationAge)
        {
            Color = color;
        }

        public Cat() { }
    }
}

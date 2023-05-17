namespace AnimalEditor.Model.Animals
{
    internal class Nest
    {
        public int Diameter { get; set; }       
           
        public Nest(int capacity) => Diameter = capacity;
    }
}

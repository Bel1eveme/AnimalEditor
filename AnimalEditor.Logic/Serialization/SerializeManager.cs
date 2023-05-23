using System.Reflection;

using AnimalEditor.Model.Animals;
using AnimalEditor.Model.Serialization.SerializableAnimals;

namespace AnimalEditor.Model.Serialization
{
    public class SerializeManager
    {
        private readonly Dictionary<string, ISerializer> _serializers;
        public SerializeManager()
        {
            _serializers = new Dictionary<string, ISerializer>();
            foreach (var serializer in GetSerializers())
            {
                _serializers.Add(serializer.GetExtension(), serializer);
            }
        }
        public ISerializer? GetRequiredSerializer(string fileExtension)
        {
            return !_serializers.ContainsKey(fileExtension) ? null : _serializers[fileExtension];
        }

        public List<string> GetAllFileExtensions()
        {
            return _serializers.Select(x => x.Value.GetExtension()).ToList();
        }

        private List<ISerializer> GetSerializers()
        {
            var implementors = new List<ISerializer>();

            Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t != typeof(ISerializer) && typeof(ISerializer).IsAssignableFrom(t))
                .ToList()
                .ForEach(x => implementors.Add((ISerializer)Activator.CreateInstance(x)));

            return implementors;
        }

        public static List<SerializableAnimal> ListToSerializableList(List<Animal> animals)
        {
            var serializableAnimals = new List<SerializableAnimal>();

            foreach (var animal in animals)
            {
                switch (animal)
                {
                    case Cat cat:
                        serializableAnimals.Add(new SerializableCat(cat));
                        break;
                    case Dog dog:
                        serializableAnimals.Add(new SerializableDog(dog));
                        break;
                    case Owl owl:
                        serializableAnimals.Add(new SerializableOwl(owl));
                        break;
                    case Stork stork:
                        serializableAnimals.Add(new SerializableStork(stork));
                        break;
                    default:
                        throw new Exception(nameof(animal));
                }
            }

            return serializableAnimals;
        }

        public static List<Animal> SerializableListToList(List<SerializableAnimal> serializableAnimals)
        {
            var animals = new List<Animal>();

            foreach (var serializableAnimal in serializableAnimals)
            {
                switch (serializableAnimal)
                {
                    case SerializableCat cat:
                        animals.Add(new Cat(DateOnly.FromDateTime(cat.BirthDate), cat.GestationAge, cat.Color));
                        break;
                    case SerializableDog dog:
                        animals.Add(new Dog(DateOnly.FromDateTime(dog.BirthDate), dog.GestationAge, dog.Breed, dog.Name));
                        break;
                    case SerializableOwl owl:
                        animals.Add(new Owl(DateOnly.FromDateTime(owl.BirthDate), owl.MaxFlightHeight, 
                                    owl.CurrentNest.Capacity, owl.CurrentNest.IsUsed, TimeOnly.FromDateTime(owl.HuntStartTime)));
                        break;
                    case SerializableStork stork:
                        animals.Add(new Stork(DateOnly.FromDateTime(stork.BirthDate), stork.MaxFlightHeight,
                                    stork.CurrentNest.Capacity, stork.CurrentNest.IsUsed, stork.BeakLength));
                        break;
                    default:
                        throw new Exception(nameof(serializableAnimal));
                }
            }

            return animals;
        }
    }
}

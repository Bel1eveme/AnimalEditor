using System.Data;

using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model
{
    public class DataManager
    {
        private Dictionary<Type, Dictionary<int, Animal>> _animals;

        public DataManager(List<Type> types)
        {
            _animals = new Dictionary<Type, Dictionary<int, Animal>>();
            types.ForEach(x => _animals.Add(x, new Dictionary<int, Animal>()));
        }
        public DataTable CreateDataTable(Type type)
        {
            var table = new DataTable();

            var idColumn = new DataColumn("Id", Type.GetType("System.Int32")!);
            idColumn.Unique = true;
            table.Columns.Add(idColumn);
            table.PrimaryKey = new[] { table.Columns["Id"]! };

            foreach (var propertyInfo in type.GetProperties())
            {
                if (!propertyInfo.PropertyType.IsClass || propertyInfo.PropertyType == typeof(string))
                {
                    table.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
              
                }
                else
                {
                    foreach (var subPropertyInfo in propertyInfo.PropertyType.GetProperties())
                        table.Columns.Add(new DataColumn(subPropertyInfo.Name, subPropertyInfo.PropertyType));
                }
            }

            foreach (var item in _animals[type])
            {
                var id = item.Key;
                var list = new List<(string, object)>(){ ("Id", id) };
                list.AddRange(GetObjectValues(type, item.Value));

                var values = list.Select(x => x.Item2).ToArray();
                table.Rows.Add(values);
            }

            return table;
        }

        public void Add(Type type, Animal animal)
        {
            _animals[type].Add(GetNextKey(_animals[type]), animal);
        }

        public void Remove(Type type, int id)
        {
            _animals[type].Remove(id);
        }

        public void Edit(Type type, int id, Animal animal)
        {
            _animals[type][id] = animal;
        }

        public Animal GetById(Type type, int id)
        {
            return _animals[type][id];
        }

        public static int GetNextKey<T>(Dictionary<int, T> dictionary)
        {
            if (dictionary.Count == 0) return 1;

            var min = dictionary.Keys.Min();
            var max = dictionary.Keys.Max();

            var result = Enumerable.Range(min, max - min).Except(dictionary.Keys).FirstOrDefault();

            return result == default ? max + 1 : result ;
        }

        private static List<(string, object)> GetObjectValues(Type type, Animal animal)
        {
            var list = new List<(string, object)>();
            foreach (var propertyInfo in type.GetProperties())
            {
                var propertyType = propertyInfo.PropertyType;
                if (!propertyType.IsClass || propertyType == typeof(string))
                {
                    list.Add((propertyInfo.Name, propertyInfo.GetValue(animal)!));
                }
                else
                {
                    var propertyObject = propertyInfo.GetValue(animal);
                    foreach (var subPropertyInfo in propertyType.GetProperties())
                    {
                        var value = propertyType?.GetProperty(subPropertyInfo.Name)?.GetValue(propertyObject);
                        list.Add((subPropertyInfo.Name, value!));
                    }
                }
            }

            return list;
        }

        public List<Animal> GetAnimalList()
        {
            var animals = new List<Animal>();
            var dictionaries = _animals.Select(x => x.Value);

            foreach (var dictionary in dictionaries)
            {
                animals.AddRange(dictionary.Select(animal => animal.Value));
            }

            return animals;
        }

        public void SetAnimalsFromList(List<Animal> animals, List<Type> types)
        {
            _animals = new Dictionary<Type, Dictionary<int, Animal>>();
            types.ForEach(x => _animals.Add(x, new Dictionary<int, Animal>()));

            foreach (var animal in animals)
            {
                foreach (var type in types)
                {
                    if (animal.GetType().IsAssignableTo(type))
                    {
                        _animals[type].Add(GetNextKey(_animals[type]), animal);
                    }
                }
            }
        }

    }
}

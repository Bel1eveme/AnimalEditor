using System.Data;
using System.Dynamic;
using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model
{
    public class DataManager
    {
        private Dictionary<Type, Dictionary<int, Animal>> _animals = new Dictionary<Type, Dictionary<int, Animal>>();

        public DataManager(List<Type> types)
        {
            types.ForEach(x => _animals.Add(x, new Dictionary<int, Animal>()));
        }
        public DataTable CreateDataTable(Type type)
        {
            var table = new DataTable();

            DataColumn idColumn = new DataColumn("Id", Type.GetType("System.Int32")!);
            idColumn.Unique = true;
            //idColumn.AllowDBNull = false;
            //idColumn.AutoIncrement = true;
            //idColumn.AutoIncrementSeed = 1;
            //idColumn.AutoIncrementStep = 1;
            table.Columns.Add(idColumn);
            table.PrimaryKey = new DataColumn[] { table.Columns["Id"]! };

            foreach (var propertyInfo in type.GetProperties())
            {
                if (!propertyInfo.PropertyType.IsClass)
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

                object[] values = list.Select(x => x.Item2).ToArray();
                foreach (var prop in list)
                {
                    table.Rows.Add(values);
                }
            }

            return table;
        }

        public void Add(Type type, Animal animal)
        {
            _animals[type].Add(GetNextKey<Animal>(_animals[type]), animal);
        }

        public void Add(Type type, Animal animal, int id)
        {
            _animals[type].Add(id, animal);
        }

        public void Remove(Type type, int id)
        {
            _animals[type].Remove(id);
        }

        public void Edit(Type type, int id, Animal animal)
        {
            Remove(type, id);
            Add(type, animal, id);
        }

        public static int GetNextKey<T>(Dictionary<int, T> dictionary)
        {
            var min = dictionary.Keys.Min();
            var max = dictionary.Keys.Max();

            return Enumerable.Range(min, max - min).Except(dictionary.Keys).First();
        }

        private static List<(string, object)> GetObjectValues(Type type, Animal animal)
        {
            var list = new List<(string, object)>();
            foreach (var propertyInfo in type.GetProperties())
            {
                if (!propertyInfo.PropertyType.IsClass)
                {
                    list.Add((propertyInfo.Name, propertyInfo.GetValue(animal)!));
                }
                else
                {
                    foreach (var subPropertyInfo in propertyInfo.PropertyType.GetProperties())
                        list.Add((propertyInfo.Name, propertyInfo.GetValue(animal)!));
                }
            }

            return list;
        }

        //public void Add(Type type, object[] values)
        //{
        //    var table = _tables[type];
        //    var newValues = new object[values.Length + 1];
        //    newValues[0] = null;
        //    for (int i = 1; i < newValues.Length; i++)
        //        newValues[i] = values[i - 1];
        //    table.Rows.Add(newValues);
        //}

        //public void Remove(Type type, int id)
        //{
        //    var table = _tables[type];
        //    DataRow dr = table.Select($"Id={id}").FirstOrDefault();
        //    if (dr != null)
        //    {
        //        table.Rows.Remove(dr);
        //    }
        //}

        //public void Edit(Type type, int id, object[] values)
        //{
        //    Remove(type, id);
        //    Add(type, values);
        //}

    }
}

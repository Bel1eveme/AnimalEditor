using System.Data;

using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model
{
    public class DataManager
    {
        private readonly Dictionary<Type, DataTable> _tables = new Dictionary<Type, DataTable>();
        public DataManager(List<Type> types)
        {
            foreach (var type in types)
            {
                _tables.Add(type, CreateDataTable(type));
            }
        }

        private DataTable CreateDataTable(Type type)
        {
            var table = new DataTable();

            DataColumn idColumn = new DataColumn("Id", Type.GetType("System.Int32"));
            idColumn.Unique = true;
            idColumn.AllowDBNull = false;
            idColumn.AutoIncrement = true;
            idColumn.AutoIncrementSeed = 1;
            idColumn.AutoIncrementStep = 1;
            table.Columns.Add(idColumn);
            table.PrimaryKey = new DataColumn[] { table.Columns["Id"] };

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

            return table;
        }

        public DataTable GetDataTable(Type type)
        {
            return _tables[type];
        }
    }
}

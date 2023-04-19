using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        table.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
                }
            }

            /*

            DataColumn ageColumn = new DataColumn("Age", Type.GetType("System.Int32"));
            table.Columns.Add(ageColumn);

            DataColumn timeColumn = new DataColumn("HuntTimeStart", Type.GetType("System.TimeOnly"));
            table.Columns.Add(timeColumn);

            DataColumn dateColumn = new DataColumn("BirthDate", Type.GetType("System.DateOnly"));
            table.Columns.Add(dateColumn);

            DataColumn canFlyColumn = new DataColumn("CanFly", Type.GetType("System.Boolean"));
            table.Columns.Add(canFlyColumn);

            DataColumn catBreedColumn = new DataColumn("CatColor", typeof(CatColor));
            table.Columns.Add(catBreedColumn);

            */

            return table;
        }

        public DataTable GetDataTable(Type type)
        {
            return _tables[type];
        }
    }
}

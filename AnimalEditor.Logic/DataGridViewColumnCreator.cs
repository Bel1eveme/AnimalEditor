using System.Windows.Forms;
using System.Reflection;


namespace AnimalEditor.Model
{
    public static class DataGridViewColumnCreator
    {
        private record EnumValue
        {
            public string Display { get; set; }
            public int Value { get; set; }
            public EnumValue(string display, int value)
            {
                Display = display;
                Value = value;  
            }
        }
        public static DataGridViewComboBoxColumn GetEnumColumn(PropertyInfo property)
        {
            var col = new DataGridViewComboBoxColumn();
            col.DataPropertyName = property.Name;
            col.Name = property.Name;
            var values = Enum.GetValues(property.PropertyType);
            var enumValues = new List<EnumValue>();
            foreach (var value in values)
            {
                enumValues.Add(new EnumValue(value.ToString()!, (int)value));
            }

            col.DataSource = enumValues;
            col.DisplayMember = "Display";
            col.ValueMember = "Value";
            return col;    
        }

        public static DataGridViewTextBoxColumn GetTextColumn(PropertyInfo property)
        {
            var col = new DataGridViewTextBoxColumn();
            col.ValueType = typeof(string);
            col.DataPropertyName = property.Name;
            col.Name = property.Name;
            return col;
        }

        public static DataGridViewTextBoxColumn GetIntColumn(PropertyInfo property)
        {
            var col = new DataGridViewTextBoxColumn(); 
            col.ValueType = typeof(int);
            col.DataPropertyName = property.Name;
            col.Name = property.Name;
            return col;
        }

        public static DataGridViewTextBoxColumn GetDateColumn(PropertyInfo property)
        {
            var col = new DataGridViewTextBoxColumn();
            col.ValueType = typeof(DateOnly);
            col.DataPropertyName = property.Name;
            col.Name = property.Name;
            return col;
        }

        public static DataGridViewTextBoxColumn GetTimeColumn(PropertyInfo property)
        {
            var col = new DataGridViewTextBoxColumn();
            col.ValueType = typeof(TimeOnly);
            col.DataPropertyName = property.Name;
            col.Name = property.Name;
            return col;
        }

        public static DataGridViewTextBoxColumn GetBoolColumn(PropertyInfo property)
        {
            var col = new DataGridViewTextBoxColumn();
            col.ValueType = typeof(bool);
            col.DataPropertyName = property.Name;
            col.Name = property.Name;
            return col;
        }

        public static DataGridViewTextBoxColumn GetIdColumn()
        {
            var col = new DataGridViewTextBoxColumn();
            col.ValueType = typeof(int);
            col.DataPropertyName = @"Id";
            col.Name = @"Id";
            return col;
        }
    }
}

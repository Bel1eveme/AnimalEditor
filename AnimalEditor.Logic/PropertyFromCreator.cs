using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalEditor.Model
{
    public static class PropertyFormCreator
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
        public static Form GetEnumForm(PropertyInfo property)
        {
            var form = new Form();
            var label = new Label();    
            label.Text = "Enter: " + property.Name; 

            


            var col = new DataGridViewComboBoxColumn();
            col.DataPropertyName = property.Name;
            col.Name = property.Name;
            var values = Enum.GetValues(property.PropertyType);
            var enumValues = new List<EnumValue>();
            foreach (var value in values)
            {
                enumValues.Add(new EnumValue(value.ToString(), (int)value));
            }

            col.DataSource = enumValues;
            col.DisplayMember = "Display";
            col.ValueMember = "Value";
            return col;
        }

        
    }
}

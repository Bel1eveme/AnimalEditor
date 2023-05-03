using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalEditor.View
{
    internal class CustomDataGridViewColumn : DataGridViewTextBoxColumn
    {
        public Type Type { get; private set; }
        public CustomDataGridViewColumn(Type type, string value)
        {
            Type = type;
        }
    }
}

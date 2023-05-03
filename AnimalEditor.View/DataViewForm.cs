using System.CodeDom;
using System.Reflection;
using AnimalEditor.Model;
using AnimalEditor.Model.Animals;

namespace AnimalEditor.View
{
    public partial class DataViewForm : Form
    {
        private readonly DataManager _dataManager;
        public DataViewForm()
        {
            InitializeComponent();

            _dataManager = new DataManager(ReflexionAnalyzer.GetConcreteClasses());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            classDomainUpDown.Items.AddRange(ReflexionAnalyzer.GetConcreteAnimalNames());
            classDomainUpDown.SelectedIndex = 0;
        }

        private void BuildDataGridView(Type type)
        {
            ClearDataGridView(objectsDataGridView);

            objectsDataGridView.Columns.Add(DataGridViewColumnCreator.GetIdColumn());

            foreach (var propertyInfo in type.GetProperties())
            {
                Type propertyType = propertyInfo.PropertyType;
                if (!propertyInfo.PropertyType.IsClass)
                {
                    objectsDataGridView.Columns.Add(GetColumnByProperties(propertyInfo));
                }
                else
                {
                    foreach (var property in propertyInfo.PropertyType.GetProperties())
                    {
                        objectsDataGridView.Columns.Add(GetColumnByProperties(property));
                    }
                }
            }

            objectsDataGridView.DataSource = _dataManager.GetDataTable(type);
        }
        private void ClearDataGridView(DataGridView dataGridView)
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
        }
        private void classDomainUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            BuildDataGridView(ReflexionAnalyzer.GetTypeByString(classDomainUpDown.SelectedItem.ToString()!));
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            
        }

        private void objectsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var type = objectsDataGridView.Columns[e.ColumnIndex].ValueType;
            var form = GetFormByType(this, type, "qweqwe");
            form.Show();
        }

        private DataGridViewColumn GetColumnByProperties(PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;   
            if (type == Type.GetType("System.Int32"))
                return DataGridViewColumnCreator.GetIntColumn(propertyInfo);
            if (type == Type.GetType("System.String"))
                return DataGridViewColumnCreator.GetTextColumn(propertyInfo);
            if (type == Type.GetType("System.DateOnly"))
                return DataGridViewColumnCreator.GetDateColumn(propertyInfo);
            if (type == Type.GetType("System.TimeOnly"))
                return DataGridViewColumnCreator.GetTimeColumn(propertyInfo);
            if (type == Type.GetType("System.Boolean"))
                return DataGridViewColumnCreator.GetBoolColumn(propertyInfo);
            if (type.IsEnum)
                return DataGridViewColumnCreator.GetEnumColumn(propertyInfo);
            throw new Exception("No column creator for this type.");
        }

        private ValueForm GetFormByType(Form caller, Type type, object value)
        {
            if (type == Type.GetType("System.Int32"))
                return ValueFormCreator.GetIntForm(caller, type, value);
            if (type == Type.GetType("System.String"))
                return ValueFormCreator.GetTextForm(caller, type, value);
            if (type == Type.GetType("System.DateOnly"))
                return ValueFormCreator.GetDateForm(caller, type, value); ;
            if (type == Type.GetType("System.TimeOnly"))
                return ValueFormCreator.GetTimeForm(caller, type, value);
            if (type == Type.GetType("System.Boolean"))
                return ValueFormCreator.GetCheckForm(caller, type, value); ;
            if (type.IsEnum)
                return ValueFormCreator.GetEnumForm(caller, type, value);
            throw new Exception("No column creator for this type.");
        }
    }
}
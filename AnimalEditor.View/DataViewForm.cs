using System.CodeDom;
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

            foreach (var propertyInfo in type.GetProperties())
            {
                Type newType = propertyInfo.PropertyType;
                if (propertyInfo.PropertyType == Type.GetType("System.Int32"))
                    objectsDataGridView.Columns.Add(DataGridViewColumnCreator.GetIntColumn(propertyInfo));
                else if (propertyInfo.PropertyType == Type.GetType("System.String"))
                    objectsDataGridView.Columns.Add(DataGridViewColumnCreator.GetTextColumn(propertyInfo));
                else if (propertyInfo.PropertyType == Type.GetType("System.DateOnly"))
                    objectsDataGridView.Columns.Add(DataGridViewColumnCreator.GetTextColumn(propertyInfo));
                else if (propertyInfo.PropertyType == Type.GetType("System.TimeOnly"))
                    objectsDataGridView.Columns.Add(DataGridViewColumnCreator.GetEnumColumn(propertyInfo));
                else if (propertyInfo.PropertyType.IsEnum)
                    objectsDataGridView.Columns.Add(DataGridViewColumnCreator.GetEnumColumn(propertyInfo));
                
                else
                {
                    throw new Exception("No column creator for this type.");
                }
            }

            objectsDataGridView.DataSource = _dataManager.GetDataTable(type);

            Console.WriteLine(@"Hello");
        }

        private void CreateColumnsForObject(DataGridView dataGridView, Type type)
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
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

    }
}
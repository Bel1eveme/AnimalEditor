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
            var dataGridView = new DataGridView();  
            foreach (var propertyInfo in type.GetProperties())
            {
                Type newType = propertyInfo.PropertyType;   
                if (propertyInfo.PropertyType == Type.GetType("System.Int32"))
                    dataGridView.Columns.Add(DataGridViewColumnCreator.GetIntColumn(propertyInfo));
                else if (propertyInfo.PropertyType == Type.GetType("System.String"))
                    dataGridView.Columns.Add(DataGridViewColumnCreator.GetTextColumn(propertyInfo));
                else if (propertyInfo.PropertyType == Type.GetType("System.DateOnly"))
                    dataGridView.Columns.Add(DataGridViewColumnCreator.GetTextColumn(propertyInfo));
                else if (propertyInfo.PropertyType.IsEnum)
                    dataGridView.Columns.Add(DataGridViewColumnCreator.GetEnumColumn(propertyInfo));
                else
                {
                    throw new Exception("No column creator for this type.");
                }
            }

            objectsDataGridView = dataGridView;
            objectsDataGridView.DataSource =
                _dataManager.GetDataTable(type);
        }

        private void classDomainUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            BuildDataGridView(ReflexionAnalyzer.GetTypeByString(classDomainUpDown.SelectedItem.ToString()!));
        }
    }
}
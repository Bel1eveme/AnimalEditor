using System.CodeDom;
using System.Reflection;
using System.Windows.Forms;
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

            _dataManager = new DataManager();
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

            var col = objectsDataGridView.Columns[0].Visible = false;

            objectsDataGridView.DataSource = _dataManager.CreateDataTable(type);
        }
        private void ClearDataGridView(DataGridView dataGridView)
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
        }

        private void classDomainUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            BuildDataGridView(GetCurrentType());
        }

        private Type GetCurrentType()
        {
            return ReflexionAnalyzer.GetTypeByString(classDomainUpDown.SelectedItem.ToString()!);
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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (objectsDataGridView.SelectedRows.Count > 0 && !objectsDataGridView.SelectedRows[0].IsNewRow)
            {
                var selectedRow = objectsDataGridView.SelectedRows[0].Index;
                var id = objectsDataGridView.Rows[selectedRow].Cells[0].Value;
                _dataManager.Remove(GetCurrentType(), (int)id);
            }

            objectsDataGridView.DataSource = _dataManager.CreateDataTable(GetCurrentType());
            Console.WriteLine(@"Row removed.");
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (objectsDataGridView.SelectedRows.Count <= 0 || objectsDataGridView.SelectedRows[0].IsNewRow) return;

            var selectedRow = objectsDataGridView.SelectedRows[0].Index;
            var id = objectsDataGridView.Rows[selectedRow].Cells[0].Value;
            var values = new List<(string, Type, object)>();
            for (var i = 1; i < objectsDataGridView.ColumnCount; i++)
            {
                values.Add((objectsDataGridView.Columns[i].Name, objectsDataGridView.Columns[i].ValueType, objectsDataGridView.Rows[selectedRow].Cells[i].Value));
            }
            var form = new EditForm(GetCurrentType(), values, EditForm.CreationMode.Edit);

            form.Shown += (o, args) => Enabled = false;
            form.Closed += (o, args) => Enabled = true;
            form.ShowDialog();

            var newValues = form.ValuesItself;

            _dataManager.Edit(GetCurrentType(), (int)id, newValues.ToArray());

            objectsDataGridView.DataSource = _dataManager.CreateDataTable(GetCurrentType());
            Console.WriteLine(@"Row changed.");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var values = new List<(string, Type, object)>();
            for (var i = 1; i < objectsDataGridView.ColumnCount; i++)
            {
                values.Add((objectsDataGridView.Columns[i].Name, objectsDataGridView.Columns[i].ValueType, null));
            }
            var form = new EditForm(GetCurrentType(), values, EditForm.CreationMode.Add);

            form.Shown += (o, args) => Enabled = false;
            form.Closed += (o, args) => Enabled = true;
            form.ShowDialog();
            var newValues = form.ValuesItself;
            _dataManager.Add(GetCurrentType(), newValues.ToArray());

            objectsDataGridView.DataSource = _dataManager.CreateDataTable(GetCurrentType());
            Console.WriteLine(@"Row added.");
        }
    }
}
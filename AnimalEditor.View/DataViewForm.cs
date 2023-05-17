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
                if (!propertyType.IsClass)
                {
                    objectsDataGridView.Columns.Add(GetColumnByProperties(propertyInfo));
                }
                else
                {
                    foreach (var property in propertyType.GetProperties())
                    {
                        objectsDataGridView.Columns.Add(GetColumnByProperties(property));
                        objectsDataGridView.Columns[^1].HeaderText = propertyType.Name +
                                                                     objectsDataGridView.Columns[^1].HeaderText;
                    }
                }
            }
            objectsDataGridView.Columns[0].Visible = false;

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
            if (objectsDataGridView.SelectedRows.Count <= 0 || objectsDataGridView.SelectedRows[0].IsNewRow) return;

            var selectedRow = objectsDataGridView.SelectedRows[0].Index;
            var id = objectsDataGridView.Rows[selectedRow].Cells[0].Value;
            _dataManager.Remove(GetCurrentType(), (int)id);

            objectsDataGridView.DataSource = _dataManager.CreateDataTable(GetCurrentType());
            Console.WriteLine(@"Row removed.");
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (objectsDataGridView.SelectedRows.Count <= 0 || objectsDataGridView.SelectedRows[0].IsNewRow) return;

            var selectedRow = objectsDataGridView.SelectedRows[0].Index;
            var id = objectsDataGridView.Rows[selectedRow].Cells[0].Value;

            var form = new EditForm(_dataManager.GetById(GetCurrentType(), (int)id));

            form.Shown += (o, args) => Enabled = false;
            form.Closed += (o, args) => Enabled = true;
            form.ShowDialog();

            var animal = form.CurrentAnimal;
            _dataManager.Edit(GetCurrentType(), (int)id, animal);

            objectsDataGridView.DataSource = _dataManager.CreateDataTable(GetCurrentType());
            Console.WriteLine(@"Row changed.");
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var form = new EditForm(GetCurrentType());

            form.Shown += (o, args) => Enabled = false;
            form.Closed += (o, args) => Enabled = true;
            form.ShowDialog();

            if (!form.HasChanged) return;

            _dataManager.Add(GetCurrentType(), form.CurrentAnimal);
            objectsDataGridView.DataSource = _dataManager.CreateDataTable(GetCurrentType());
            Console.WriteLine(@"Row added.");
        }
    }
}
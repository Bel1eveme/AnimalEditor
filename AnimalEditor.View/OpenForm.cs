using AnimalEditor.Model;
using AnimalEditor.Model.Serialization;

namespace AnimalEditor.View
{
    public partial class OpenForm : Form
    {
        private readonly string _defaultObjectsPath;

        private readonly DataManager _dataManager;

        private readonly ArchiveManager _archiveManager;

        private readonly SerializeManager _serializeManager;

        private string _filePath;

        public OpenForm(DataManager dataManager)
        {
            InitializeComponent();

            _filePath = string.Empty;
            _dataManager = dataManager;
            _archiveManager = new ArchiveManager();
            _serializeManager = new SerializeManager();
            _defaultObjectsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "objects");

            FormatTextBox.TextAlign = HorizontalAlignment.Center;
            ArchieveTextBox.TextAlign = HorizontalAlignment.Center;
        }

        private string GetExtension(string fileName)
        {
            var splits = fileName.Split(".");

            return splits?.Length > 1 ? "." + splits[^1] : string.Empty;
        }

        private string GetSecondExtension(string fileName)
        {
            var splits = fileName.Split(".");

            return splits?.Length > 2 ? "." + splits[^2] : string.Empty;
        }

        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = _defaultObjectsPath,
                Title = @"Choose file path",
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            if (!File.Exists(openFileDialog.FileName))
            {
                MessageBox.Show(@"File not exists.");
                return;
            }

            _filePath = openFileDialog.FileName;
            filePathTextBox.Text = openFileDialog.FileName;
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            var firstExtension = GetExtension(_filePath);

            var archiver = _archiveManager.GetArchiverByExtension(firstExtension);

            if (archiver == null)
            {
                ArchieveTextBox.Text = @"None";

                var serializer = _serializeManager.GetRequiredSerializer(firstExtension);
                if (serializer == null)
                {
                    MessageBox.Show(@"Wrong file name format.");
                    return;
                }

                FormatTextBox.Text = serializer.GetExtension();
                try
                {
                    var serializedAnimals = serializer.Deserialize(_filePath);
                    _dataManager.SetAnimalsFromList(serializedAnimals, ReflexionAnalyzer.GetConcreteClasses());
                }
                catch (Exception)
                {
                    MessageBox.Show(@"File format error.");
                    return;
                }
            }
            else
            {
                var serializer = _serializeManager.GetRequiredSerializer(GetSecondExtension(_filePath));
                if (serializer == null)
                {
                    MessageBox.Show(@"Wrong file name format.");
                    return;
                }

                FormatTextBox.Text = serializer.GetExtension();
                ArchieveTextBox.Text = archiver.GetExtension();

                try
                {
                    var serializedStream = archiver.Unzip(_filePath);
                    var serializedAnimals = serializer.Deserialize(serializedStream);
                    _dataManager.SetAnimalsFromList(serializedAnimals, ReflexionAnalyzer.GetConcreteClasses());
                }
                catch (Exception)
                {
                    MessageBox.Show(@"File format error.");
                    return;
                }
            }
            MessageBox.Show(@"Data has been opened.");
        }
    }
}

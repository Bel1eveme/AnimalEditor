using AnimalEditor.Model;
using AnimalEditor.Model.Serialization;

namespace AnimalEditor.View
{
    public partial class SaveForm : Form
    {
        private readonly string _defaultObjectsPath;

        private readonly SerializeManager _serializeManager;

        private readonly ArchiveManager _archiveManager;

        private readonly DataManager _dataManager;

        private string _fileName;

        public SaveForm(DataManager dataManager)
        {
            InitializeComponent();

            _fileName = string.Empty;
            _dataManager = dataManager;
            _archiveManager = new ArchiveManager();
            _serializeManager = new SerializeManager();
            _defaultObjectsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "objects");

            FormatUpDown.Items.AddRange(_serializeManager.GetAllFileExtensions());
            ArchiveUpDown.Items.AddRange(_archiveManager.GetAllArchiverExtensions());
            ArchiveUpDown.TextAlign = HorizontalAlignment.Center;
            FormatUpDown.TextAlign = HorizontalAlignment.Center;
            ArchiveUpDown.Items.Add("None");
            FormatUpDown.SelectedIndex = 0;
            ArchiveUpDown.SelectedIndex = 0;
        }

        private void doButton_Click(object sender, EventArgs e)
        {
            var archiveExtension = GetExtension(ArchiveUpDown);
            var serializeExtension = GetExtension(FormatUpDown);

            var archiver = _archiveManager.GetArchiverByExtension(archiveExtension);

            if (archiver == null)
            {
                var serializer = _serializeManager.GetRequiredSerializer(serializeExtension);
                if (serializer == null)
                {
                    MessageBox.Show(@"Wrong file name format.");
                    return;
                }

                _fileName += serializer.GetExtension();
                    
                try
                {
                    serializer.Serialize(_dataManager.GetAnimalList(), _fileName);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"File format error.");
                    return;
                }
            }
            else
            {
                var serializer = _serializeManager.GetRequiredSerializer(serializeExtension);
                if (serializer == null)
                {
                    MessageBox.Show(@"Wrong file name format.");
                    return;
                }

                _fileName += serializer.GetExtension();
                _fileName += archiver.GetExtension();

                try
                {
                    var serializedAnimals = serializer.Serialize(_dataManager.GetAnimalList());
                    archiver.Zip(serializedAnimals, _fileName);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"File format error.");
                    return;
                }

            }
            MessageBox.Show(@"Data has been saved.");
        }

        private void ChoosePathButton_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                InitialDirectory = _defaultObjectsPath,
                Title = @"Choose save path",
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            if (File.Exists(saveFileDialog.FileName))
            {
                MessageBox.Show(@"Such file already exists.");
                return;
            }

            _fileName = saveFileDialog.FileName;
            filePathTextBox.Text = saveFileDialog.FileName;
        }

        private string GetExtension(DomainUpDown upDown)
        {
            var splits = upDown.SelectedItem?.ToString()?.Split(".");

            return splits?.Length > 1 ? "." + splits[^1] : string.Empty;
        }
    }
}

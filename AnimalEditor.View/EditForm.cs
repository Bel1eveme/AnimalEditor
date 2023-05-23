using System.Reflection;

using AnimalEditor.Model.Animals;

namespace AnimalEditor.View
{
    public partial class EditForm : Form
    {
        public Animal CurrentAnimal { get; private set; }

        public Animal ResultAnimal { get; private set; }
        private enum CreationMode { Edit, Add }

        private readonly CreationMode _mode;

        private readonly Type _type;

        public bool HasChanged { get; private set; }

        public EditForm(Animal animal)
        {
            InitializeComponent();

            CurrentAnimal = GetCopy(animal);
            ResultAnimal = animal;
            _mode = CreationMode.Edit;
            _type = animal.GetType();
            HasChanged = false;

            CreateForm();
        }

        public EditForm(Type type)
        {
            InitializeComponent();

            CurrentAnimal = GetNewAnimal(type);
            ResultAnimal = CurrentAnimal;
            _mode = CreationMode.Add;
            _type = type;
            HasChanged = false;

            CreateForm();
        }

        private void CreateForm()
        {
            var currentXOffset = 200;
            var currentYOffset = 5;

            Font = new Font(FontFamily.GenericSerif, 14);
            Height = WindowHeight;
            Width = WindowWidth;
            StartPosition = FormStartPosition.CenterScreen;
            Text = _mode == CreationMode.Edit ? @"Edit form" : @"Add form";
            AutoScroll = true;

            var label = new Label()
            {
                Top = currentYOffset,
                Left = currentXOffset,
                Text = _type.Name,
                Height = ButtonHeight,
                Width = ButtonWidth,
            };
            Controls.Add(label);
            currentYOffset += ButtonHeight;

            foreach (var propertyInfo in _type.GetProperties())
            {
                var propertyType = propertyInfo.PropertyType;
                if (!propertyType.IsClass || propertyType == typeof(string))
                {
                    var panel = CreatePanel(propertyInfo, CurrentAnimal);
                    panel.Top = currentYOffset;
                    Controls.Add(panel);

                    currentYOffset += panel.Height;
                }
                else
                {
                    foreach (var subPropertyInfo in propertyType.GetProperties())
                    {
                        var instance = _type.GetProperty(propertyInfo.Name)?.GetValue(CurrentAnimal);
                        var panel = CreatePanel(subPropertyInfo, instance!, propertyType.Name);
                        panel.Top = currentYOffset;
                        Controls.Add(panel);

                        currentYOffset += panel.Height;
                    }
                }
            }

            var button = new Button()
            {
                Top = currentYOffset,
                Left = currentXOffset,
                Text = _mode == CreationMode.Edit ? @"Edit" : @"Add",
                Height = ButtonHeight,
                Width = ButtonWidth,
            };
            button.Click += ButtonClickHandler;

            Controls.Add(button);
        }

        private FlowLayoutPanel CreatePanel(PropertyInfo propertyInfo, object value, string textBefore = "")
        {
            var panel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                BorderStyle = BorderStyle.None,
                WrapContents = false,
                AutoSize = true,
                Height = InnerPanelHeight,
                AutoScroll = true,
            };

            var label = new Label()
            {
                Text = textBefore + propertyInfo.Name + @":",
                Height = ControlHeight,
                Width = ControlWidth,
                AutoSize = true,
            };
            
            var customControl = GetControlByType(propertyInfo, value);

            panel.Controls.Add(label);
            panel.Controls.Add(customControl);

            return panel;
        }

        public Control GetTextBox(PropertyInfo propertyInfo, object value)
        {
            var control = new TextBox()
            {
                Height = ControlHeight,
                Width = ControlWidth,
                DataBindings = { new Binding("Text", value, propertyInfo.Name) },
            };
            return control;
        }

        public Control GetNumberUpDown(PropertyInfo propertyInfo, object value)
        {
            var control = new NumericUpDown()
            {
                Height = ControlHeight,
                Width = ControlWidth,
                DataBindings = { new Binding("Value", value, propertyInfo.Name) },
            };
            return control;
        }

        private Control GetCheckBox(PropertyInfo propertyInfo, object value)
        {
            var control = new CheckBox()
            {
                Height = ControlHeight,
                Width = ControlWidth,
                AutoSize = true,
                DataBindings = { new Binding("Checked", value, propertyInfo.Name) },
            };
            return control;
        }

        public Control GetDatePiker(PropertyInfo propertyInfo, object value)
        {
            var binding = new Binding("Value", value, propertyInfo.Name);
            binding.Format += DateOnlyToDateTime;
            binding.Parse += DateTimeToDateOnly;

            var control = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = @"yyyy/MM/dd",
                Height = ControlHeight,
                Width = ControlWidth,
                DataBindings = { binding },
            };
            return control;
        }

        public Control GetTimePiker(PropertyInfo propertyInfo, object value)
        {
            var binding = new Binding("Value", value, propertyInfo.Name);
            binding.Format += TimeOnlyToDateTime;
            binding.Parse += DateTimeToTimeOnly;

            var control = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Height = ControlHeight,
                Width = ControlWidth,
                DataBindings = { binding },
            };
            return control;
        }

        public Control GetComboBox(PropertyInfo propertyInfo, object value)
        {
            var binding = new Binding("SelectedItem", value, propertyInfo.Name);

            var control = new ComboBox()
            {
                Height = ControlHeight,
                Width = ControlWidth,
                DataSource = Enum.GetValues(propertyInfo.PropertyType),
                DataBindings = { binding },
            };

            return control;
        }

        private void ButtonClickHandler(object? sender, EventArgs e)
        {
            if (sender is not Button button) return;

            var form = button.FindForm();
            if (form is null) return;

            ResultAnimal = CurrentAnimal;
            HasChanged = true;

            MessageBox.Show(@"New values saved.");
        }

        private void DateTimeToDateOnly(object? sender, ConvertEventArgs e)
        {
            if (e.DesiredType != typeof(DateOnly)) return;
            if (e.Value is not DateTime dateTime) return;

            e.Value = DateOnly.FromDateTime(dateTime);
        }

        private void DateOnlyToDateTime(object? sender, ConvertEventArgs e)
        {
            if (e.DesiredType != typeof(DateTime)) return;
            if (e.Value is not DateOnly dateOnly) return;

            e.Value = dateOnly.ToDateTime(TimeOnly.Parse("0:00"));
        }

        private void DateTimeToTimeOnly(object? sender, ConvertEventArgs e)
        {
            if (e.DesiredType != typeof(TimeOnly)) return;
            if (e.Value is not DateTime dateTime) return;

            e.Value = TimeOnly.FromDateTime(dateTime);
        }

        private void TimeOnlyToDateTime(object? sender, ConvertEventArgs e)
        {
            if (e.DesiredType != typeof(DateTime)) return;
            if (e.Value is not TimeOnly timeOnly) return;

            var date = new DateTime(2022, 1, 1);
            e.Value = date + timeOnly.ToTimeSpan();
        }

        private void CustomEnumValueToEnumValue(object? sender, ConvertEventArgs e)
        {
            if (e.DesiredType != typeof(EnumValue)) return;
            if (e.Value is not CatColor customEnumValue) return;

            e.Value = new EnumValue(customEnumValue.ToString()!, (int)customEnumValue);
        }

        private void EnumValueToCustomEnumValue(object? sender, ConvertEventArgs e)
        {
            if (e.DesiredType != typeof(CatColor)) return;
            if (e.Value is not EnumValue enumValue) return;

            e.Value = enumValue.Value;
        }

        private void ForceComboBoxChange(object? sender, EventArgs e)
        {
            if (sender is not ComboBox comboBox) return;

            comboBox.DataBindings["SelectedItem"]?.WriteValue();
        }

        private Control GetControlByType(PropertyInfo propertyInfo, object value)
        {
            if (propertyInfo.PropertyType == Type.GetType("System.Int32"))
                return GetNumberUpDown(propertyInfo, value);
            if (propertyInfo.PropertyType == Type.GetType("System.String"))
                return GetTextBox(propertyInfo, value);
            if (propertyInfo.PropertyType == Type.GetType("System.DateOnly"))
                return GetDatePiker(propertyInfo, value);
            if (propertyInfo.PropertyType == Type.GetType("System.TimeOnly"))
                return GetTimePiker(propertyInfo, value);
            if (propertyInfo.PropertyType == Type.GetType("System.Boolean"))
                return GetCheckBox(propertyInfo, value);
            if (propertyInfo.PropertyType.IsEnum)
                return GetComboBox(propertyInfo, value);
            throw new Exception("No control creator for this type.");
        }

        private Animal GetCopy(Animal animal)
        {
            var newAnimal = GetNewAnimal(animal.GetType());
            foreach (var property in animal.GetType().GetProperties())
            {
                var propertyType = property.PropertyType;
                if (!propertyType.IsClass || propertyType == typeof(string))
                {
                    var propertyValue = animal.GetType()?.GetProperty(property.Name)?.GetValue(animal);
                    newAnimal.GetType()?.GetProperty(property.Name)?.SetValue(newAnimal, propertyValue);
                }
                else
                {
                    var propertyObject = property.GetValue(animal)!;
                    var newPropertyObject = property.GetValue(newAnimal)!;
                    foreach (var subProperty in propertyType.GetProperties())
                    {
                        var propertyValue = propertyType?.GetProperty(subProperty.Name)?.GetValue(propertyObject);
                        newPropertyObject.GetType()?.GetProperty(subProperty.Name)?.SetValue(newPropertyObject, propertyValue);
                    }
                }
            }

            return newAnimal;
        }

        private Animal GetNewAnimal(Type type)
        {
            if (Activator.CreateInstance(type) is Animal animal)
                return animal;
            throw new Exception("GG");
        }

        private record EnumValue(string Display, int Value)
        {
            public string Display { get; set; } = Display;
            public int Value { get; set; } = Value;
        }

        private const int WindowHeight = 400;

        private const int WindowWidth = 500;

        private const int PanelHeight = 600;

        private const int PanelWidth = 450;

        private const int InnerPanelHeight = 50;

        private const int InnerPanelWidth = 500;

        private const int ControlHeight = 100;

        private const int ControlWidth = 300;

        private const int PanelOffset = 20;

        private const int ButtonHeight = 40;

        private const int ButtonWidth = 100;

    }
}

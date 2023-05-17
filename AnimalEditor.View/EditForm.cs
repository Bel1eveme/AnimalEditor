using System.Reflection;
using AnimalEditor.Model.Animals;

namespace AnimalEditor.View
{
    public partial class EditForm : Form
    {
        public Animal ResultAnimal { get; private set; }
        public enum CreationMode { Edit, Add }

        private readonly CreationMode _mode;

        private readonly Type _type;

        private Dictionary<PropertyInfo, Control> propertyControls;

        private static bool _hasChanged;
        public EditForm(Animal animal, CreationMode mode)
        {
            InitializeComponent();

            ResultAnimal = animal;
            _mode = mode;
            _type = animal.GetType();
            _hasChanged = false;

            propertyControls = new Dictionary<PropertyInfo, Control>();

            CreateForm();
        }

        public EditForm(Type type, CreationMode mode)
        {
            InitializeComponent();
            
            if (Activator.CreateInstance(type) is Animal animal)
                ResultAnimal = animal;
            else
            {
                throw new Exception("GG");
            }

            _mode = mode;
            _type = type;
            _hasChanged = false;

            propertyControls = new Dictionary<PropertyInfo, Control>();

            CreateForm();
        }

        public List<object> ValuesItself;

        private List<object> _newValuesItself;

        private readonly List<(string, Type)> _types;

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
                var panel = CreatePanel(propertyInfo);
                panel.Top = currentYOffset;
                Controls.Add(panel);

                currentYOffset += panel.Height;
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

        private void ButtonClickHandler(object? sender, EventArgs e)
        {
            if (sender is not Button button) return;

            var form = button.FindForm();
            if (form is null) return;

            _newValuesItself = GetNewValues(form);

            ValuesItself = _newValuesItself;

            MessageBox.Show(@"New values saved.");
        }

        private FlowLayoutPanel CreatePanel(PropertyInfo propertyInfo, object value)
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
                Text = propertyInfo.Name + @":",
                Height = ControlHeight,
                Width = ControlWidth,
                AutoSize = true,
            };

            var customControl = _mode == CreationMode.Edit ? GetControlByType(propertyInfo.PropertyType, value) 
                                                            : GetControlByTypeWithDefaults(propertyInfo.PropertyType);
            propertyControls.Add(propertyInfo, customControl);

            panel.Controls.Add(label);
            panel.Controls.Add(customControl);

            return panel;
        }

        private Control GetControlByType(Type type, object value)
        {
            if (type == Type.GetType("System.Int32"))
                return GetNumberUpDown(value);
            if (type == Type.GetType("System.String"))
                return GetTextBox(value);
            if (type == Type.GetType("System.DateOnly"))
                return GetDatePiker(value);
            if (type == Type.GetType("System.TimeOnly"))
                return GetTimePiker(value);
            if (type == Type.GetType("System.Boolean"))
                return GetCheckBox(value);
            if (type.IsEnum)
                return GetComboBox(type, value);
            throw new Exception("No control creator for this type.");
        }

        private Control GetControlByTypeWithDefaults(Type type)
        {
            if (type == Type.GetType("System.Int32"))
                return GetNumberUpDown(0);
            if (type == Type.GetType("System.String"))
                return GetTextBox("");
            if (type == Type.GetType("System.DateOnly"))
                return GetDatePiker(DateOnly.Parse("01.01.2000"));
            if (type == Type.GetType("System.TimeOnly"))
                return GetTimePiker(TimeOnly.Parse("00:00:00"));
            if (type == Type.GetType("System.Boolean"))
                return GetCheckBox(false);
            if (type.IsEnum)
            {
                var values = Enum.GetValues(type);
                return GetComboBox(type, values.GetValue(0)!);
            }
            throw new Exception("No control creator for this type.");
        }

        public Control GetTextBox(object value)
        {
            var control = new TextBox()
            {
                Height = ControlHeight,
                Width = ControlWidth,
                DataBindings = { Type.GetType(_) }
            };
            if (value is string str)
            {
                control.Text = str;
            }
            return control;
        }

        public Control GetNumberUpDown(object value)
        {
            var control = new NumericUpDown()
            {
                Height = ControlHeight,
                Width = ControlWidth,
            };
            if (value is int number)
            {
                control.Value = number;
            }
            return control;
        }

        public Control GetDatePiker(object value)
        {
            var control = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = @"yyyy/MM/dd",
                Height = ControlHeight,
                Width = ControlWidth,
            };
            if (value is DateOnly date)
            {
                control.Value = date.ToDateTime(TimeOnly.Parse("0:00"));
            }
            return control;
        }

        public Control GetTimePiker(object value)
        {
            var control = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Height = ControlHeight,
                Width = ControlWidth,
            };
            
            var date = new DateTime(2022, 1, 1);
            if (value is TimeOnly time)
            {
                control.Value = date + time.ToTimeSpan();
            }
            return control;
        }

        public Control GetComboBox(Type type, object value)
        {
            var control = new ComboBox()
            {
                Height = ControlHeight,
                Width = ControlWidth,
            };
            var values = Enum.GetValues(type);
            var enumValues = new List<EnumValue>();
            foreach (var val in values)
            {
                enumValues.Add(new EnumValue(val.ToString()!, (int)val));
            }

            control.DisplayMember = "Display";
            control.ValueMember = "Value";

            var objects = new object[enumValues.Count];
            for (int i = 0; i < enumValues.Count; i++)
            {
                objects[i] = enumValues[i];
            }

            control.Items.AddRange(objects);

            if (!DBNull.Value.Equals(value))
                control.SelectedIndex = (int)value;

            return control;
        }

        private Control GetCheckBox(object value)
        {
            var control = new CheckBox()
            {
                Height = ControlHeight,
                Width = ControlWidth,
                AutoSize = true,
            };
            if (value is bool boolValue)
            {
                control.Checked = boolValue;
            }
            return control;
        }

        private record EnumValue(string Display, int Value)
        {
            public string Display { get; set; } = Display;
            public int Value { get; set; } = Value;
        }

        private List<object> GetNewValues(Form form)
        {
            var list = new List<object>();

            var controls = form.Controls;
            var customControls = new List<Control>();
            foreach (Control item in controls)
            {
                var subControls = item.Controls;
                foreach (Control subControl in subControls)
                {
                    if (subControl is not Label)
                    {
                        customControls.Add(subControl);
                    }
                }
            }

            foreach (var item in customControls)
            {
                switch (item)
                {
                    case TextBox textBox:
                        list.Add(textBox.Text);
                        break;
                    case NumericUpDown numericUpDown:
                        list.Add(numericUpDown.Value);
                        break;
                    case ComboBox comboBox:
                        var enumValue = comboBox.SelectedItem;
                        if (enumValue is EnumValue ev)
                        {
                            list.Add(ev.Value);
                        }
                        else
                        {
                            throw new Exception("Rofl exception.");
                        }
                        break;
                    case CheckBox checkBox:
                        list.Add(checkBox.Checked);
                        break;
                    case DateTimePicker dateTimePicker:
                        if (dateTimePicker.Format == DateTimePickerFormat.Custom)
                        {
                            string date = dateTimePicker.Value.ToLongDateString();
                            var dateOnly = DateOnly.Parse(date);
                            list.Add(dateOnly);
                        }
                        else
                        {
                            string time = dateTimePicker.Value.ToLongTimeString();
                            var timeOnly = TimeOnly.Parse(time);
                            list.Add(timeOnly);
                        }
                        break;
                    
                }
            }

            return list;
        }
    }

}

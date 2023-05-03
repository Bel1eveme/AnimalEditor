using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using AnimalEditor.View;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace AnimalEditor.Model
{
    public static class ValueFormCreator
    {

        private static ValueForm CreateValueForm(Form caller, Control control, object valueItself)
        {
            int formWidth = 300;
            int formHeight = 200;

            int panelWidth = 300;
            int panelHeight = 200;

            int fieldWidth = 270;
            int fieldHeight = 50;

            var valueForm = new ValueForm(caller, typeof(string), valueItself)
            {
                Font = new Font(FontFamily.GenericSerif, 14),
                Size = new Size(formWidth, formHeight),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                MinimizeBox = false,
                MaximizeBox = false,

            };

            var flowPanel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.TopDown,
                Size = new Size(panelWidth, panelHeight),
            };

            var label = new Label()
            {
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(fieldWidth, fieldHeight),
                Text = "Enter your value:",
            };

            control.Size = new Size(fieldWidth, fieldHeight);

            var button = new Button()
            {
                Size = new Size(fieldWidth, fieldHeight),
                Text = "Save",
            };

            flowPanel.Controls.Add(label);
            flowPanel.Controls.Add(control);
            flowPanel.Controls.Add(button);

            valueForm.Controls.Add(flowPanel);

            return valueForm;
        }
        public static ValueForm GetTextForm(Form caller, object valueItself)
        {
            var control = new TextBox();
            control.Text = valueItself as string;
            return CreateValueForm(caller, control, valueItself);
        }

        public static ValueForm GetIntForm(Form caller, object valueItself)
        {
            var control = new NumericUpDown();
            if (valueItself is int)
            {
                //control.Value = valueItself.To;
            }

            return CreateValueForm(caller, control, valueItself);
        }

        public static ValueForm GetDateForm(Form caller, object valueItself)
        {
            var control = new DateTimePicker();
            control.Format = DateTimePickerFormat.Custom;
            control.CustomFormat = @"yyyy/MM/dd";
            var time = new TimeOnly(0);
            control.Value = value.ToDateTime(TimeOnly.Parse("0:00"));
            return CreateValueForm(caller, control, valueItself);
        }

        public static ValueForm GetTimeForm(Form caller, TimeOnly value, object valueItself)
        {
            var control = new DateTimePicker();
            control.Format = DateTimePickerFormat.Time;
            control.ShowUpDown = true;
            var date = new DateTime(2022, 1, 1);
            control.Value = date + value.ToTimeSpan();
            return CreateValueForm(caller, control, valueItself);
        }

        public static ValueForm GetEnumForm(Form caller, TimeOnly value, object valueItself)
        {
            var control = new ComboBox();
            //control.DataSource
            //control.Format = DateTimePickerFormat.Time;
            //control.Value = value;
            return CreateValueForm(caller, control, valueItself);
        }

        public static ValueForm GetCheckForm(Form caller, bool value, object valueItself)
        {
            var control = new CheckBox();
            control.Checked = value;
            return CreateValueForm(caller, control, valueItself);
        }
    }
}

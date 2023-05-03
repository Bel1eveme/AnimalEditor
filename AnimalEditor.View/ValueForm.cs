using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalEditor.View
{
    public class ValueForm : Form
    {
        public Type ValueType { get; private set; }

        public object ValueItself { get; private set; }

        public Form CallerForm { get; private set; }

        public ValueForm(Form caller, Type valueType, object valueItself)
        {
            CallerForm = caller;
            ValueType = valueType;
            ValueItself = valueItself;

            Load += BlockForm;
            FormClosed += UnblockForm;
        }

        private static void BlockForm(object? sender, EventArgs e)
        {
            if (sender is ValueForm form)
            {
                form.CallerForm.Enabled = false;
            }
        }
        private static void UnblockForm(object? sender, EventArgs e)
        {
            if (sender is ValueForm form)
            {
                form.CallerForm.Enabled = true;
            }
        }
    }
}

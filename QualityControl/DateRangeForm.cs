using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Server
{
    public partial class DateRangeForm : Form
    {
        public DateRangeForm()
        {
            InitializeComponent();
        }

        public DateTime right {get; private set;}
        public DateTime left  {get; private set;}
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            left = dateTimePicker1.Value;
            right = dateTimePicker2.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.CompareTo(dateTimePicker1.Value) < 0)
            {
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.CompareTo(dateTimePicker2.Value) > 0)
            {
                dateTimePicker1.Value = dateTimePicker2.Value;
            }
        }
    }
}

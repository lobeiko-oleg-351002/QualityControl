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
    public partial class ContinueChoiceForm : Form
    {
        public ContinueChoiceForm()
        {
            InitializeComponent();
        }

        public ContinueChoiceForm(string message)
        {
            InitializeComponent();
            label1.Text = message;
            CenterToScreen();
            button1.Top = label1.Bottom + 30;
            button2.Top = button1.Top;
            this.Height += 10;
        }

        public bool IsContinue { get; private set; }
        private void button1_Click(object sender, EventArgs e)
        {
            IsContinue = false;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IsContinue = true;
            Close();
        }
    }
}

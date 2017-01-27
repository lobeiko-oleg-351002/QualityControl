using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Client.Forms
{
    public partial class ChangeForm : Form
    {
        DirectoryForm parent;

        public ChangeForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        public ChangeForm(DirectoryForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            CenterToParent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected virtual void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

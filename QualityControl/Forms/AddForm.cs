using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIL.Entities;
using UIL.Entities.Interface;

namespace QualityControl_Client.Forms
{
    public partial class AddForm : Form
    {
        DirectoryForm parent;

        public AddForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        public AddForm(DirectoryForm parent)
        {
            InitializeComponent();
            CenterToParent();
            this.parent = parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        virtual protected void button2_Click(object sender, EventArgs e)
        {
            parent.RefreshData();
            Close();
        }
    }
}

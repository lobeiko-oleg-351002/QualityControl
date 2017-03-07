using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIL.Entities.Interface;

namespace QualityControl_Client.Forms
{
    public partial class DirectoryForm : Form
    {
        public DirectoryForm()
        {           
            InitializeComponent();
            CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        protected virtual void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected virtual void button1_Click(object sender, EventArgs e)
        {
            
        }

        public virtual void RefreshData() { }

        protected virtual void button2_Click(object sender, EventArgs e)
        {

        }

        protected virtual void button3_Click(object sender, EventArgs e)
        {

        }

        public virtual void SelectRow(IBllEntity entity)
        {

        }
    }
}

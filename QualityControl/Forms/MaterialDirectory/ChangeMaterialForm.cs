using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIL.Entities;

namespace QualityControl_Client.Forms.MaterialDirectory
{
    public partial class ChangeMaterialForm : ChangeForm
    {
        UilMaterial oldMaterial;
        public ChangeMaterialForm() : base()
        {
            InitializeComponent();
        }

        public ChangeMaterialForm(DirectoryForm parent, UilMaterial oldMaterial, DataGridViewRow currentRow) : base(parent)
        {
            InitializeComponent();
            this.oldMaterial = oldMaterial;
            textBox1.Text = (string)currentRow.Cells[0].Value;
            richTextBox1.Text = (string)currentRow.Cells[1].Value;

        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                oldMaterial.Name = textBox1.Text;
                oldMaterial.Description = richTextBox1.Text;
                IMaterialRepository repository = ServiceChannelManager.Instance.MaterialRepository;
                repository.Update(oldMaterial);
                base.button2_Click(sender, e);
            }
        }
    }
}

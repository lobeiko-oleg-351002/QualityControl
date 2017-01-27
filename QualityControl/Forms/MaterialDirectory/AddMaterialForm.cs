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
    public partial class AddMaterialForm : AddForm
    {
        public AddMaterialForm() : base()
        {
            InitializeComponent();
        }
        public AddMaterialForm(DirectoryForm parent) : base(parent)
        {
            InitializeComponent();
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                UilMaterial Material = new UilMaterial
                {
                    Name = textBox1.Text,
                    Description = richTextBox1.Text
                };
                IMaterialRepository repository = ServiceChannelManager.Instance.MaterialRepository;
                repository.Create(Material);
                base.button2_Click(sender, e);
            }
        }


    }
}

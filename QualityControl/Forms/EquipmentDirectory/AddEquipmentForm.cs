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

namespace QualityControl_Client.Forms.EquipmentDirectory
{
    public partial class AddEquipmentForm : AddForm
    {
        public AddEquipmentForm() : base()
        {
            InitializeComponent();
        }
        public AddEquipmentForm(DirectoryForm parent) : base(parent)
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
                UilEquipment Equipment = new UilEquipment
                {
                    Name = textBox1.Text,
                    Type = textBox2.Text,
                    FactoryNumber = (int)numericUpDown1.Value,                    
                    IsChecked = new byte[] { Convert.ToByte(checkBox1.Checked) },
                    CheckDate = dateTimePicker1.Value,
                    TechnicalCheckDate = dateTimePicker2.Value,
                    NextTechnicalCheckDate = dateTimePicker3.Value,
                    Pressmark = textBox4.Text,
                    NumberOfTechnicalCheck = textBox3.Text,
                };
                IEquipmentRepository repository = ServiceChannelManager.Instance.EquipmentRepository;
                repository.Create(Equipment);
                base.button2_Click(sender, e);
            }
        }
    }
}

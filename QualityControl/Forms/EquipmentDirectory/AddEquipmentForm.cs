using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Client.Forms.EquipmentDirectory
{
    public partial class AddEquipmentForm : AddForm
    {
        public AddEquipmentForm() : base()
        {
            InitializeComponent();
        }
        IUnitOfWork uow;
        public AddEquipmentForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                BllEquipment Equipment = new BllEquipment
                {
                    Name = textBox1.Text,
                    Type = textBox2.Text,
                    FactoryNumber = (int)numericUpDown1.Value,
                    IsChecked = checkBox1.Checked,
                    CheckDate = dateTimePicker1.Value,
                    TechnicalCheckDate = dateTimePicker2.Value,
                    NextTechnicalCheckDate = dateTimePicker3.Value,
                    Pressmark = textBox4.Text,
                    NumberOfTechnicalCheck = textBox3.Text,
                };
                IEquipmentService Service = new EquipmentService(uow);
                Service.Create(Equipment);
                base.button2_Click(sender, e);
            }
        }
    }
}

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
    public partial class ChangeEquipmentForm : ChangeForm
    {
        BllEquipment oldEquipment;
        public ChangeEquipmentForm() : base()
        {
            InitializeComponent();
        }
        IUnitOfWork uow;
        public ChangeEquipmentForm(DirectoryForm parent, BllEquipment oldEquipment, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            this.oldEquipment = oldEquipment;
            textBox1.Text = oldEquipment.Name;
            textBox2.Text = oldEquipment.Type;
            textBox4.Text = oldEquipment.Pressmark;
            numericUpDown1.Value = Convert.ToDecimal(oldEquipment.FactoryNumber);
            checkBox1.Checked = oldEquipment.IsChecked.Value;
            textBox3.Text = oldEquipment.NumberOfTechnicalCheck;
            dateTimePicker1.Value = oldEquipment.CheckDate.Value;
            dateTimePicker2.Value = oldEquipment.TechnicalCheckDate.Value;
            dateTimePicker3.Value = oldEquipment.NextTechnicalCheckDate.Value;

        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                oldEquipment.Name = textBox1.Text;
                oldEquipment.Type = textBox2.Text;
                oldEquipment.Pressmark = textBox4.Text;
                oldEquipment.FactoryNumber = (int)numericUpDown1.Value;
                oldEquipment.IsChecked = checkBox1.Checked;
                oldEquipment.NumberOfTechnicalCheck = textBox3.Text;
                oldEquipment.CheckDate = dateTimePicker1.Value;
                oldEquipment.TechnicalCheckDate = dateTimePicker2.Value;
                oldEquipment.NextTechnicalCheckDate = dateTimePicker3.Value;


                IEquipmentService Service = new EquipmentService(uow);
                Service.Update(oldEquipment);
                base.button2_Click(sender, e);
            }
        }
    }
}

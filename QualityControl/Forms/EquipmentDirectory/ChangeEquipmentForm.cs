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
    public partial class ChangeEquipmentForm : ChangeForm
    {
        UilEquipment oldEquipment;
        public ChangeEquipmentForm() : base()
        {
            InitializeComponent();
        }

        public ChangeEquipmentForm(DirectoryForm parent, UilEquipment oldEquipment, DataGridViewRow currentRow) : base(parent)
        {
            InitializeComponent();
            this.oldEquipment = oldEquipment;
            textBox1.Text = (string)currentRow.Cells[0].Value;
            textBox2.Text = (string)currentRow.Cells[1].Value;
            textBox4.Text = oldEquipment.Pressmark;
            numericUpDown1.Value = Convert.ToDecimal(oldEquipment.FactoryNumber);
            checkBox1.Checked = currentRow.Cells[4].Value.ToString() == "Да" ? true : false;
            textBox3.Text = oldEquipment.NumberOfTechnicalCheck;
            dateTimePicker1.Value = (DateTime)currentRow.Cells[6].Value;
            dateTimePicker2.Value = (DateTime)currentRow.Cells[7].Value;
            dateTimePicker3.Value = (DateTime)currentRow.Cells[8].Value;

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
                oldEquipment.IsChecked[0] = Convert.ToByte(checkBox1.Checked);
                oldEquipment.NumberOfTechnicalCheck = textBox3.Text;
                oldEquipment.CheckDate = dateTimePicker1.Value;
                oldEquipment.TechnicalCheckDate = dateTimePicker2.Value;
                oldEquipment.NextTechnicalCheckDate = dateTimePicker3.Value;


                IEquipmentRepository repository = ServiceChannelManager.Instance.EquipmentRepository;
                repository.Update(oldEquipment);
                base.button2_Click(sender, e);
            }
        }
    }
}

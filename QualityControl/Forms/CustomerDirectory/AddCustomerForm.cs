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

namespace QualityControl_Client.Forms.CustomerDirectory
{
    public partial class AddCustomerForm : AddForm
    {
        public AddCustomerForm() : base()
        {
            InitializeComponent();
        }
        public AddCustomerForm(DirectoryForm parent) : base(parent)
        {
            InitializeComponent();
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите название организации", "Оповещение");
            }
            else
            {
                UilCustomer customer = new UilCustomer
                {
                    Organization = textBox1.Text,
                    Address = textBox2.Text,
                    Phone = textBox3.Text,
                    Contract = textBox4.Text,
                    ContractBeginDate = dateTimePicker1.Value,
                    ContractEndDate = dateTimePicker2.Value
                };
                ICustomerRepository repository = ServiceChannelManager.Instance.CustomerRepository;
                repository.Create(customer);
                base.button2_Click(sender, e);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Repositories.Interface;
using BLL.Entities;
using BLL.Services.Interface;
using BLL.Services;

namespace QualityControl_Client.Forms.CustomerDirectory
{
    public partial class AddCustomerForm : AddForm
    {
        public AddCustomerForm() : base()
        {
            InitializeComponent();
        }
        IUnitOfWork uow;
        public AddCustomerForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            this.uow = uow;
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
                BllCustomer customer = new BllCustomer
                {
                    Organization = textBox1.Text,
                    Address = textBox2.Text,
                    Phone = textBox3.Text,
                    Contract = textBox4.Text,
                    ContractBeginDate = dateTimePicker1.Value,
                    ContractEndDate = dateTimePicker2.Value
                };
                ICustomerService Service = new CustomerService(uow);
                Service.Create(customer);
                base.button2_Click(sender, e);
            }
        }
    }
}

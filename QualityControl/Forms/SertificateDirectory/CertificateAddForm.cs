using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Client.Forms.EmployeeDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace QualityControl_Client.Forms.SertificateDirectory
{
    public partial class CertificateAddForm : AddForm
    {
        public CertificateAddForm() : base()
        {
            InitializeComponent();
        }
        IUnitOfWork uow;

        IEnumerable<BllControlName> controlNames;
        public CertificateAddForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            IControlNameService controlNameService = new ControlNameService(uow);
            controlNames = controlNameService.GetAll();
            foreach (var name in controlNames)
            {
                comboBox1.Items.Add(name.Name);
            }
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите название", "Оповещение");
            }
            else
            {
                BllCertificate certificate = new BllCertificate
                {
                    Title = textBox1.Text,
                    CheckDate = dateTimePicker1.Value,
                    ControlName = comboBox1.SelectedIndex != -1 ? controlNames.ElementAt(comboBox1.SelectedIndex) : null,
                    Duration = (int)numericUpDown1.Value,
                    Employee = employee
                };
                ICertificateService Service = new CertificateService(uow);
                Service.Create(certificate);
                base.button2_Click(sender, e);
            }
        }

        BllEmployee employee;
        private void button3_Click(object sender, EventArgs e)
        {
            ChooseEmployeeForm employeeForm = new ChooseEmployeeForm(uow);
            employeeForm.ShowDialog(this);
            employee = employeeForm.GetChosenEmployee();
            if (employee != null)
            {
                textBox2.Text = employee.Sirname + " " + employee.Name + employee.Fathername;
            }
        }
    }
}

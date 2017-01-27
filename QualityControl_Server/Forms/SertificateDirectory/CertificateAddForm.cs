using QualityControl_Client.Forms.EmployeeDirectory;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UIL.Entities;
using UIL.Entities.Interface;

namespace QualityControl_Client.Forms.SertificateDirectory
{
    public partial class CertificateAddForm : AddForm
    {
        public CertificateAddForm() : base()
        {
            InitializeComponent();
        }

        IEnumerable<UilControlName> controlNames;
        public CertificateAddForm(DirectoryForm parent) : base(parent)
        {
            InitializeComponent();
            IControlNameRepository controlNameRepository = ServiceChannelManager.Instance.ControlNameRepository;
            controlNames = controlNameRepository.GetAll();
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
                UilCertificate certificate = new UilCertificate
                {
                    Title = textBox1.Text,
                    CheckDate = dateTimePicker1.Value,
                    ControlName = comboBox1.SelectedIndex != -1 ? controlNames.ElementAt(comboBox1.SelectedIndex) : null,
                    Duration = (int)numericUpDown1.Value,
                    Employee = employee
                };
                ICertificateRepository repository = ServiceChannelManager.Instance.CertificateRepository;
                repository.Create(certificate);
                base.button2_Click(sender, e);
            }
        }

        UilEmployee employee;
        private void button3_Click(object sender, EventArgs e)
        {
            ChooseEmployeeForm employeeForm = new ChooseEmployeeForm();
            employeeForm.ShowDialog(this);
            employee = employeeForm.GetChosenEmployee();
            if (employee != null)
            {
                textBox2.Text = employee.Sirname + " " + employee.Name + employee.Fathername;
            }
        }
    }
}

using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Client.Forms.EmployeeDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QualityControl_Client.Forms.SertificateDirectory
{
    public partial class ChangeCertificateForm : ChangeForm
    {
        BllCertificate oldCertificate;
        IUnitOfWork uow;
        public ChangeCertificateForm() : base()
        {
            InitializeComponent();
        }

        IEnumerable<BllControlName> controlNames;
        public ChangeCertificateForm(DirectoryForm parent, BllCertificate oldCertificate, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.oldCertificate = oldCertificate;
            this.uow = uow;
            textBox1.Text = oldCertificate.Title;
            if (oldCertificate.Employee != null)
            {
                textBox2.Text = oldCertificate.Employee.Sirname + " " + oldCertificate.Employee.Name + " " + oldCertificate.Employee.Fathername;
            }
            
            IControlNameService controlNameService = new ControlNameService(uow);
            controlNames = controlNameService.GetAll();
            foreach (var name in controlNames)
            {
                comboBox1.Items.Add(name.Name);
            }
            comboBox1.SelectedItem = oldCertificate.ControlName != null ? oldCertificate.ControlName.Name : "";
            dateTimePicker1.Value = oldCertificate.CheckDate;

        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите название", "Оповещение");
            }
            else
            {
                oldCertificate.Title = textBox1.Text;
                oldCertificate.CheckDate = dateTimePicker1.Value;
                oldCertificate.Employee = employee;
                oldCertificate.ControlName = comboBox1.SelectedIndex != -1 ? controlNames.ElementAt(comboBox1.SelectedIndex) : null;
                oldCertificate.Duration = (int)numericUpDown1.Value;
                ICertificateService Service = new CertificateService(uow);
                Service.Update(oldCertificate);
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

using QualityControl_Client.Forms.EmployeeDirectory;
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
using UIL.Entities.Interface;

namespace QualityControl_Client.Forms.SertificateDirectory
{
    public partial class ChangeCertificateForm : ChangeForm
    {
        UilCertificate oldCertificate;
        public ChangeCertificateForm() : base()
        {
            InitializeComponent();
        }

        IEnumerable<UilControlName> controlNames;
        public ChangeCertificateForm(DirectoryForm parent, UilCertificate oldCertificate, DataGridViewRow currentRow) : base(parent)
        {
            InitializeComponent();
            this.oldCertificate = oldCertificate;
            textBox1.Text = (string)currentRow.Cells[0].Value;
            if (oldCertificate.Employee != null)
            {
                textBox2.Text = oldCertificate.Employee.Sirname + " " + oldCertificate.Employee.Name + " " + oldCertificate.Employee.Fathername;
            }
            
            IControlNameRepository controlNameRepository = ServiceChannelManager.Instance.ControlNameRepository;
            controlNames = controlNameRepository.GetAll();
            foreach (var name in controlNames)
            {
                comboBox1.Items.Add(name.Name);
            }
            comboBox1.SelectedItem = oldCertificate.ControlName != null ? oldCertificate.ControlName.Name : "";
            dateTimePicker1.Value = (DateTime)currentRow.Cells[1].Value;

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
                ICertificateRepository repository = ServiceChannelManager.Instance.CertificateRepository;
                repository.Update(oldCertificate);
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

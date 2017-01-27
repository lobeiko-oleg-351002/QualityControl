using QualityControl_Client.Forms.SertificateDirectory;
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

namespace QualityControl_Client.Forms.EmployeeDirectory
{
    public partial class AddEmployeeForm : AddForm
    {
        UilCertificateLib certificateLib;
        List<UilCertificate> certificates = new List<UilCertificate>();
        public AddEmployeeForm() : base()
        {
            InitializeComponent();
        }
        public AddEmployeeForm(DirectoryForm parent) : base(parent)
        {
            InitializeComponent();
            certificateLib = new UilCertificateLib();
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            List<UilSelectedCertificate> selectedCertificates = new List<UilSelectedCertificate>();
            foreach (var element in certificates)
            {
                selectedCertificates.Add(new UilSelectedCertificate
                {
                    Certificate = element
                });
            }
            certificateLib.SelectedCertificate = selectedCertificates;
            UilEmployee Employee = new UilEmployee
            {
                Name = textBox1.Text,
                Sirname = textBox2.Text,
                Fathername = textBox3.Text,
                Function = textBox4.Text,
                MedicalCheckDate = dateTimePicker1.Value,
                KnowledgeCheckDate = dateTimePicker2.Value
            };
            IEmployeeRepository repository = ServiceChannelManager.Instance.EmployeeRepository;

            string errorMessage = "Неверно указаны данные";
            bool isError = false;
            if (Employee.Name == "" || Employee.Sirname == "" || Employee.Fathername == "" || Employee.Function == "")
            {
                isError = true;
            }
            //foreach(var certificate in Employee.CertificateLib.SelectedCertificate)
            //{
            //    if (certificate.Certificate.Sirname != Employee.Sirname || certificate.Certificate.Name != Employee.Name || certificate.Certificate.Fathername != Employee.Fathername)
            //    {
            //        isError = true;
            //        errorMessage += "\n" + certificate.Certificate.Name;
            //    }
            //}
            if (isError == false)
            {
                repository.Create(Employee);
                base.button2_Click(sender, e);
            }
            else
            {
                MessageBox.Show(errorMessage, "Оповещение");
            }                      
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ChooseCertificateForm certificateForm = new ChooseCertificateForm(
            //    new UilEmployee
            //{
            //    Name = textBox1.Text,
            //    Sirname = textBox2.Text,
            //    Fathername = textBox3.Text,
            //});
            //certificateForm.ShowDialog(this);
            //UilCertificate certificate = certificateForm.GetChosenCertificate();
            //if (certificate != null)
            //{
            //    certificates.Add(certificate);
            //    comboBox1.Items.Add(certificate.Title);
            //    comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //certificates.RemoveAt(comboBox1.SelectedIndex);
            //comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
            //if (comboBox1.Items.Count > 0)
            //{
            //    comboBox1.SelectedIndex = 0;
            //}
        }
    }
}

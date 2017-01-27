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
    public partial class ChangeEmployeeForm : ChangeForm
    {
        UilEmployee oldEmployee;
        public ChangeEmployeeForm() : base()
        {
            InitializeComponent();
        }
        //UilCertificateLib certificateLib;
        List<UilSelectedCertificate> certificates = new List<UilSelectedCertificate>();
        public ChangeEmployeeForm(DirectoryForm parent, UilEmployee oldEmployee, DataGridViewRow currentRow) : base(parent)
        {
            InitializeComponent();
            this.oldEmployee = oldEmployee;
            //certificateLib = oldEmployee.CertificateLib;
            textBox1.Text = (string)currentRow.Cells[0].Value;
            textBox2.Text = (string)currentRow.Cells[1].Value;
            textBox3.Text = (string)currentRow.Cells[2].Value;
            textBox4.Text = (string)currentRow.Cells[3].Value;
            //if (oldEmployee.CertificateLib != null)
            //{
            //    foreach (var certificate in oldEmployee.CertificateLib.SelectedCertificate)
            //    {
            //        comboBox1.Items.Add(certificate.Certificate.Title);
            //        certificates.Add(certificate);
            //    }
            //}
            //if (comboBox1.Items.Count > 0)
            //{
            //    comboBox1.SelectedIndex = 0;
            //}
            dateTimePicker1.Value = oldEmployee.MedicalCheckDate.Value;
            dateTimePicker2.Value = oldEmployee.KnowledgeCheckDate.Value;
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            oldEmployee.Name = textBox1.Text;
            oldEmployee.Sirname = textBox2.Text;
            oldEmployee.Fathername = textBox3.Text;
            oldEmployee.Function = textBox4.Text;
            //oldEmployee.CertificateLib.SelectedCertificate = certificates;
            oldEmployee.MedicalCheckDate = dateTimePicker1.Value;
            oldEmployee.KnowledgeCheckDate = dateTimePicker2.Value;

            IEmployeeRepository repository = ServiceChannelManager.Instance.EmployeeRepository;
            string errorMessage = "Неверно указаны данные";
            bool isError = false;
            if (oldEmployee.Name == "" || oldEmployee.Sirname == "" || oldEmployee.Fathername == "" || oldEmployee.Function == "")
            {
                isError = true;
            }
            
            //foreach (var certificate in oldEmployee.CertificateLib.SelectedCertificate)
            //{
            //    if (certificate.Certificate.Sirname != oldEmployee.Sirname || certificate.Certificate.Name != oldEmployee.Name || certificate.Certificate.Fathername != oldEmployee.Fathername)
            //    {
            //        isError = true;
            //        errorMessage += "\n" + certificate.Certificate.Name;
            //    }
            //}
            if (isError == false)
            {
                repository.Update(oldEmployee);
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
            //    certificates.Add(new UilSelectedCertificate {
            //        Certificate =  certificate
            //    });
            //    comboBox1.Items.Add(certificate.Title);
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //if (comboBox1.SelectedIndex != -1)
            //{
            //    certificates.RemoveAt(comboBox1.SelectedIndex);
            //    comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
            //}

        }
    }
}

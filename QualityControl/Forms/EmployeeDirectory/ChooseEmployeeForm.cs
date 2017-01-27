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

namespace QualityControl_Client.Forms.EmployeeDirectory
{
    public partial class ChooseEmployeeForm : DirectoryForm
    {
        List<UilEmployee> Employees;
        UilEmployee employee;
        public ChooseEmployeeForm() : base()
        {
            InitializeComponent();
            RefreshData();
            dataGridView1.Columns[4].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IEmployeeRepository repository = ServiceChannelManager.Instance.EmployeeRepository;
            Employees = repository.GetAll().ToList();
            foreach (var Employee in Employees)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = Employee.Name;
                row.Cells[1].Value = Employee.Sirname;
                row.Cells[2].Value = Employee.Fathername;
                row.Cells[3].Value = Employee.Function;
                //if (Employee.CertificateLib != null)
                //{
                //    foreach (var certificate in Employee.CertificateLib.SelectedCertificate)
                //    {
                //        ((DataGridViewComboBoxCell)row.Cells[4]).Items.Add(certificate.Certificate.Title);
                //    }
                //    if (Employee.CertificateLib.SelectedCertificate.Count != 0)
                //    {
                //        ((DataGridViewComboBoxCell)row.Cells[4]).Value = ((DataGridViewComboBoxCell)row.Cells[4]).Items[0];
                //    }

                //}
                row.Cells[4].Value = Employee.MedicalCheckDate;
                row.Cells[5].Value = Employee.KnowledgeCheckDate;
                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddEmployeeForm AddEmployeeForm = new AddEmployeeForm(this);
            AddEmployeeForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IEmployeeRepository repository = ServiceChannelManager.Instance.EmployeeRepository;
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                repository.Delete(Employees[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IEmployeeRepository repository = ServiceChannelManager.Instance.EmployeeRepository;
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeEmployeeForm changeEmployeeForm = new ChangeEmployeeForm(this, Employees[rowsList[i].Index], rowsList[i]);
                changeEmployeeForm.ShowDialog(this);
            }
            RefreshData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            if (rows.Count != 1)
            {
                MessageBox.Show("Выберите только одну строку таблицы");
            }
            else
            {
                employee = Employees[rows[0].Index];
                this.Close();
            }
        }

        public UilEmployee GetChosenEmployee()
        {
            return employee;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            employee = Employees[rows[0].Index];
            this.Close();
        }
    }
}

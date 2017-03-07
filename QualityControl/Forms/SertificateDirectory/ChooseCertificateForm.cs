using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;

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
    public partial class ChooseCertificateForm : DirectoryForm
    {
        List<BllCertificate> certificates;
        BllCertificate certificate;
        IUnitOfWork uow;
        BllEmployee employee;
        public ChooseCertificateForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            dataGridView1.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy";
            RefreshData();

        }

        public ChooseCertificateForm() : base()
        {

        }

        public ChooseCertificateForm(BllEmployee employee) : base()
        {
            InitializeComponent();
            this.employee = employee;
            RefreshData();            
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            ICertificateService Service = new CertificateService(uow);
            certificates = Service.GetAll().ToList();
            foreach (var certificate in certificates)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = certificate.Title;
                row.Cells[1].Value = certificate.CheckDate;
                row.Cells[2].Value = certificate.Employee != null ? certificate.Employee.Sirname + " " + certificate.Employee.Name + " " + certificate.Employee.Fathername : "";
                row.Cells[3].Value = certificate.ControlName != null ? certificate.ControlName.Name : "<не указано>";
                row.Cells[4].Value = certificate.Duration;
                dataGridView1.Rows.Add(row);             
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            CertificateAddForm certificateAddForm = new CertificateAddForm(this, uow);
            certificateAddForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            ICertificateService Service = new CertificateService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(certificates[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            ICertificateService Service = new CertificateService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeCertificateForm changeCertificateForm = new ChangeCertificateForm(this, certificates[rowsList[i].Index], uow);
                changeCertificateForm.ShowDialog(this);
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
                certificate = certificates[rows[0].Index];
                this.Close();
            }
        }

        public BllCertificate GetChosenCertificate()
        {
            return certificate;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            certificate = certificates[rows[0].Index];
            this.Close();
        }
    }
}

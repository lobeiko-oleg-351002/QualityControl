using BLL.Entities;
using BLL.Entities.Interface;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Client.Forms.SertificateDirectory;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Client.Forms
{
    public partial class SertificateDirectoryForm : DirectoryForm
    {

        List<BllCertificate> certificates;
        IUnitOfWork uow;
        public SertificateDirectoryForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            dataGridView1.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy";
            RefreshData();
            
        }
        public SertificateDirectoryForm() : base()
        {
            InitializeComponent();
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
                row.Cells[3].Value = certificate.ControlName != null ? certificate.ControlName.Name : "<не указан>";
                row.Cells[4].Value = certificate.Duration;
                dataGridView1.Rows.Add(row);
            }
        }

        protected override void button1_Click(object sender, EventArgs e)
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
            for(int i = rowsList.Count-1; i >= 0; i--)
            {
                ChangeCertificateForm changeCertificateForm = new ChangeCertificateForm(this, certificates[rowsList[i].Index], uow);
                changeCertificateForm.ShowDialog(this);
            }
            RefreshData();
        }

        public override void SelectRow(IBllEntity entity)
        {
            dataGridView1.ClearSelection();
            var id = certificates.FindIndex(certificate => certificate.Id == entity.Id);
            if (id > -1) dataGridView1.Rows[id].Selected = true;
        }
    }
}

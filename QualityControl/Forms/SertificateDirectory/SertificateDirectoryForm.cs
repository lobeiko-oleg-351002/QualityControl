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
using UIL.Entities.Interface;

namespace QualityControl_Client.Forms
{
    public partial class SertificateDirectoryForm : DirectoryForm
    {

        List<UilCertificate> certificates;
        public SertificateDirectoryForm() : base()
        {
            InitializeComponent();
            dataGridView1.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy";
            RefreshData();
            
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            ICertificateRepository repository = ServiceChannelManager.Instance.CertificateRepository;
            certificates = repository.GetAll().ToList();
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
            CertificateAddForm certificateAddForm = new CertificateAddForm(this);
            certificateAddForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            ICertificateRepository repository = ServiceChannelManager.Instance.CertificateRepository;
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                repository.Delete(certificates[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            ICertificateRepository repository = ServiceChannelManager.Instance.CertificateRepository;
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for(int i = rowsList.Count-1; i >= 0; i--)
            {
                ChangeCertificateForm changeCertificateForm = new ChangeCertificateForm(this, certificates[rowsList[i].Index], rowsList[i]);
                changeCertificateForm.ShowDialog(this);
            }
            RefreshData();
        }
    }
}

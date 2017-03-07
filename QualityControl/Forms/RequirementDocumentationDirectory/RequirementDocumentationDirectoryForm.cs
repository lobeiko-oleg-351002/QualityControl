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

namespace QualityControl_Client.Forms.RequirementDocumentationDirectory
{
    public partial class RequirementDocumentationDirectoryForm : DirectoryForm
    {
        List<BllRequirementDocumentation> RequirementDocumentations;
        IUnitOfWork uow;
        public RequirementDocumentationDirectoryForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
            saveFileDialog1.Filter = "PDF file (*.pdf)|*.pdf";
            saveFileDialog2.Filter = "Excel files (*.xls)|*.xls";
        }
        public RequirementDocumentationDirectoryForm() : base()
        {
            InitializeComponent();
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IRequirementDocumentationService Service = new RequirementDocumentationService(uow);
            RequirementDocumentations = Service.GetAll().ToList();
            foreach (var RequirementDocumentation in RequirementDocumentations)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = RequirementDocumentation.Name;
                row.Cells[1].Value = RequirementDocumentation.Pressmark;
                row.Cells[2].Value = RequirementDocumentation.ObjectGroup;
                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddRequirementDocumentationForm AddRequirementDocumentationForm = new AddRequirementDocumentationForm(this, uow);
            AddRequirementDocumentationForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IRequirementDocumentationService Service = new RequirementDocumentationService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(RequirementDocumentations[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IRequirementDocumentationService Service = new RequirementDocumentationService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeRequirementDocumentationForm changeRequirementDocumentationForm = new ChangeRequirementDocumentationForm(this, RequirementDocumentations[rowsList[i].Index], uow);
                changeRequirementDocumentationForm.ShowDialog(this);
            }
            RefreshData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                ConvertManager.ConvertDataGridToPdf(dataGridView1, saveFileDialog1.FileName);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            saveFileDialog2.FileName = "";
            if (DialogResult.OK == saveFileDialog2.ShowDialog())
            {
                ConvertManager.ConvertDataGridToExcel(dataGridView1, saveFileDialog2.FileName);
            }
        }
    }
}

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

namespace QualityControl_Client.Forms.ControlMethodDocumentationDirectory
{
    public partial class ChooseControlMethodDocumentationForm : DirectoryForm
    {
        List<BllControlMethodDocumentation> controlMethodDocumentations;
        BllControlMethodDocumentation controlMethodDocumentation;
        IUnitOfWork uow;
        public ChooseControlMethodDocumentationForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
            CenterToParent();
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IControlMethodDocumentationService Service = new ControlMethodDocumentationService(uow);
            controlMethodDocumentations = Service.GetAll().ToList();
            foreach (var controlMethodDocumentation in controlMethodDocumentations)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = controlMethodDocumentation.Name;
                row.Cells[1].Value = controlMethodDocumentation.Pressmark;
                row.Cells[2].Value = controlMethodDocumentation.ControlName != null ? controlMethodDocumentation.ControlName.Name : "";
                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddControlMethodDocumentationForm addControlMethodDocumentationForm = new AddControlMethodDocumentationForm(this, uow);
            addControlMethodDocumentationForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IControlMethodDocumentationService Service = new ControlMethodDocumentationService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(controlMethodDocumentations[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IControlMethodDocumentationService Service = new ControlMethodDocumentationService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeControlMethodDocumentationForm changeControlMethodDocumentationForm = new ChangeControlMethodDocumentationForm(this, controlMethodDocumentations[rowsList[i].Index], uow);
                changeControlMethodDocumentationForm.ShowDialog(this);
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
                controlMethodDocumentation = controlMethodDocumentations[rows[0].Index];
                this.Close();
            }
        }

        public BllControlMethodDocumentation GetChosenControlMethodDocumentation()
        {
            return controlMethodDocumentation;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            controlMethodDocumentation = controlMethodDocumentations[rows[0].Index];
            this.Close();
        }
    }
}

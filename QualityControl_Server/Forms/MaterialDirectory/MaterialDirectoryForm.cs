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

namespace QualityControl_Client.Forms.MaterialDirectory
{
    public partial class MaterialDirectoryForm : DirectoryForm
    {
        List<UilMaterial> Materials;
        public MaterialDirectoryForm() : base()
        {
            InitializeComponent();
            RefreshData();
            saveFileDialog2.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog1.Filter = "PDF file (*.pdf)|*.pdf";
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IMaterialRepository repository = ServiceChannelManager.Instance.MaterialRepository;
            Materials = repository.GetAll().ToList();
            foreach (var Material in Materials)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = Material.Name;
                row.Cells[1].Value = Material.Description;
                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddMaterialForm AddMaterialForm = new AddMaterialForm(this);
            AddMaterialForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IMaterialRepository repository = ServiceChannelManager.Instance.MaterialRepository;
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                repository.Delete(Materials[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IMaterialRepository repository = ServiceChannelManager.Instance.MaterialRepository;
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeMaterialForm changeMaterialForm = new ChangeMaterialForm(this, Materials[rowsList[i].Index], rowsList[i]);
                changeMaterialForm.ShowDialog(this);
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

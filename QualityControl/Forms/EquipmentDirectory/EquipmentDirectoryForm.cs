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

namespace QualityControl_Client.Forms.EquipmentDirectory
{
    public partial class EquipmentDirectoryForm : DirectoryForm
    {
        List<UilEquipment> Equipments;
        public EquipmentDirectoryForm() : base()
        {
            InitializeComponent();
            RefreshData();
            saveFileDialog1.Filter = "PDF file (*.pdf)|*.pdf";
            saveFileDialog2.Filter = "Excel files (*.xls)|*.xls";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IEquipmentRepository repository = ServiceChannelManager.Instance.EquipmentRepository;
            Equipments = repository.GetAll().ToList();
            foreach (var Equipment in Equipments)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = Equipment.Name;
                row.Cells[1].Value = Equipment.Pressmark;
                row.Cells[2].Value = Equipment.Type;
                row.Cells[3].Value = Equipment.FactoryNumber;
                row.Cells[4].Value = Equipment.IsChecked[0] == 1 ? "Да" : "Нет";
                row.Cells[5].Value = Equipment.NumberOfTechnicalCheck;
                row.Cells[6].Value = Equipment.CheckDate;
                row.Cells[7].Value = Equipment.TechnicalCheckDate;
                row.Cells[8].Value = Equipment.NextTechnicalCheckDate;
                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddEquipmentForm AddEquipmentForm = new AddEquipmentForm(this);
            AddEquipmentForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IEquipmentRepository repository = ServiceChannelManager.Instance.EquipmentRepository;
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                repository.Delete(Equipments[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IEquipmentRepository repository = ServiceChannelManager.Instance.EquipmentRepository;
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeEquipmentForm changeEquipmentForm = new ChangeEquipmentForm(this, Equipments[rowsList[i].Index], rowsList[i]);
                changeEquipmentForm.ShowDialog(this);
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

        public override void SelectRow(IUilEntity entity)
        {
            dataGridView1.ClearSelection();
            var id = Equipments.FindIndex(eq => eq.Id == entity.Id);
            if (id > -1)   dataGridView1.Rows[id].Selected = true;
        }
    }
}

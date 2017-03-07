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

namespace QualityControl_Client.Forms.MaterialDirectory
{
    public partial class ChooseMaterialForm : DirectoryForm
    {
        private void button5_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            if (rows.Count != 1)
            {
                MessageBox.Show("Выберите только одну строку таблицы");
            }
            else
            {
                material = Materials[rows[0].Index];
                this.Close();
            }

        }
        IUnitOfWork uow;
        List<BllMaterial> Materials;
        BllMaterial material;
        public ChooseMaterialForm() : base()
        {

        }
        public ChooseMaterialForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IMaterialService Service = new MaterialService(uow);
            Materials = Service.GetAll().ToList();
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
            AddMaterialForm AddMaterialForm = new AddMaterialForm(this, uow);
            AddMaterialForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IMaterialService Service = new MaterialService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(Materials[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IMaterialService Service = new MaterialService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeMaterialForm changeMaterialForm = new ChangeMaterialForm(this, Materials[rowsList[i].Index], uow);
                changeMaterialForm.ShowDialog(this);
            }
            RefreshData();
        }

        public BllMaterial GetChosenMaterial()
        {
            return material;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            material = Materials[rows[0].Index];
            this.Close();
        }
    }
}

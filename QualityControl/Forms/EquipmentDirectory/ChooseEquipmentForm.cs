
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Repositories.Interface;
using BLL.Services.Interface;
using BLL.Services;
using BLL.Entities;

namespace QualityControl_Client.Forms.EquipmentDirectory
{
    public partial class ChooseEquipmentForm : DirectoryForm
    {
        List<BllEquipment> Equipments;
        BllEquipment equipment;

        IUnitOfWork uow;
        public ChooseEquipmentForm(IUnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
        }

        public ChooseEquipmentForm()
        {
            InitializeComponent();
        }


        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IEquipmentService Service = new EquipmentService(uow);
            Equipments = Service.GetAll().ToList();
            foreach (var Equipment in Equipments)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = Equipment.Name;
                row.Cells[1].Value = Equipment.Pressmark;
                row.Cells[2].Value = Equipment.Type;
                row.Cells[3].Value = Equipment.FactoryNumber;
                row.Cells[4].Value = Equipment.IsChecked.Value;
                row.Cells[5].Value = Equipment.NumberOfTechnicalCheck;
                row.Cells[6].Value = Equipment.CheckDate;
                row.Cells[7].Value = Equipment.TechnicalCheckDate;
                row.Cells[8].Value = Equipment.NextTechnicalCheckDate;
                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddEquipmentForm AddEquipmentForm = new AddEquipmentForm(this, uow);
            AddEquipmentForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IEquipmentService Service = new EquipmentService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(Equipments[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IEquipmentService Service = new EquipmentService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeEquipmentForm changeEquipmentForm = new ChangeEquipmentForm(this, Equipments[rowsList[i].Index], uow);
                changeEquipmentForm.ShowDialog(this);
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
                equipment = Equipments[rows[0].Index];
                this.Close();
            }
        }

        public BllEquipment GetChosenEquipment()
        {
            return equipment;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            equipment = Equipments[rows[0].Index];
            this.Close();
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Entities;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using BLL.Services;

namespace QualityControl_Client.Forms.ComponentDirectory
{
    public partial class ChooseComponentForm : DirectoryForm
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
                Component = Components[rows[0].Index];
                this.Close();
            }

        }

        List<BllComponent> Components;
        BllComponent Component;
        IUnitOfWork uow;
        public ChooseComponentForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IComponentService Service = new ComponentService(uow);
            Components = Service.GetAll().ToList();
            foreach (var Component in Components)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = Component.Name;
                row.Cells[1].Value = Component.Pressmark;
                row.Cells[2].Value = Component.Template != null ? Component.Template.Name : "<отсутствует>";
                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddComponentForm AddComponentForm = new AddComponentForm(this, uow);
            AddComponentForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IComponentService Service = new ComponentService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(Components[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IComponentService Service = new ComponentService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeComponentForm changeComponentForm = new ChangeComponentForm(this, Components[rowsList[i].Index], uow);
                changeComponentForm.ShowDialog(this);
            }
            RefreshData();
        }

        public BllComponent GetChosenComponent()
        {
            return Component;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            Component = Components[rows[0].Index];
            this.Close();
        }
    }
}

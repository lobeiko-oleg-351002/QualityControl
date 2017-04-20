using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Client.Forms.WeldJointDirectory
{
    public partial class WeldJointDirectoryForm : DirectoryForm
    {
        List<BllWeldJoint> WeldJoints;
        IUnitOfWork uow;
        public WeldJointDirectoryForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();

        }
        public WeldJointDirectoryForm() : base()
        {
            InitializeComponent();
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IWeldJointService Service = new WeldJointService(uow);
            WeldJoints = Service.GetAll().ToList();
            foreach (var WeldJoint in WeldJoints)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = WeldJoint.Name;
                row.Cells[1].Value = WeldJoint.Description;
                row.Cells[2].Value = WeldJoint.Image != null ? byteArrayToImage(WeldJoint.Image) : null;
                row.MinimumHeight = 100;
                dataGridView1.Rows.Add(row);
            }
            ((DataGridViewImageColumn)dataGridView1.Columns[2]).ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddWeldJointForm AddWeldJointForm = new AddWeldJointForm(this, uow);
            AddWeldJointForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IWeldJointService Service = new WeldJointService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                Service.Delete(WeldJoints[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IWeldJointService Service = new WeldJointService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeWeldJointForm changeWeldJointForm = new ChangeWeldJointForm(this, WeldJoints[rowsList[i].Index], rowsList[i]);
                changeWeldJointForm.ShowDialog(this);
            }
            RefreshData();
        }

        private Image byteArrayToImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                return Image.FromStream(ms);
            }
        }
    }
}

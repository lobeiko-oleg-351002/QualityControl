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

namespace QualityControl_Client.Forms.ResultDirectory
{
    public partial class ResultDirectoryForm : DirectoryForm
    {
        public ResultDirectoryForm()
        {
            InitializeComponent();
        }

        BllResultLib resultLib;

        public ResultDirectoryForm(BllResultLib lib)
        {
            InitializeComponent();
            resultLib = lib;
            foreach (var result in lib.Result)
            {
                AddResultRowToDataGrid(result);
            }
        }

        private void AddResultRowToDataGrid(BllResult result)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dataGridView1);

            row.Cells[0].Value = result.Number;
            row.Cells[1].Value = result.Welder;
            row.Cells[2].Value = result.Mark;
            row.Cells[3].Value = result.DefectDescription;
            row.Cells[4].Value = result.Norm;
            row.Cells[5].Value = result.Quality;
            dataGridView1.Rows.Add(row);
        }

        protected override void button1_Click(object sender, EventArgs e)
        {
            resultLib.Result.Add(new BllResult());
            dataGridView1.Rows.Add(new DataGridViewRow());
            base.button1_Click(sender, e);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                resultLib.Result.RemoveAt(row.Index);
                dataGridView1.Rows.Remove(row);
            }
            base.button2_Click(sender, e);
        }

        protected override void button4_Click(object sender, EventArgs e)
        {
            var results = resultLib.Result;
            var rows = dataGridView1.Rows;
            for (int i = 0; i < results.Count; i++)
            {
                var number = 0;
                bool isInt = rows[i].Cells[0].Value != null ? int.TryParse(rows[i].Cells[0].Value.ToString(), out number) : false;
                results[i].Number = isInt ? number : 0;
                results[i].Welder = (string)rows[i].Cells[1].Value;
                results[i].Mark = (string)rows[i].Cells[2].Value;
                results[i].DefectDescription = (string)rows[i].Cells[3].Value;
                results[i].Norm = (string)rows[i].Cells[4].Value;
                results[i].Quality = (string)rows[i].Cells[5].Value;
            }
            base.button4_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.SelectedRows)
            {
                DataGridViewRow temp = new DataGridViewRow();
                temp.CreateCells(dataGridView1);
                for(int i = 0; i < row.Cells.Count; i++)
                {
                    temp.Cells[i].Value = row.Cells[i].Value;
                }
                dataGridView1.Rows.Add(temp);
                resultLib.Result.Add(new BllResult());
            }
        }
    }
}

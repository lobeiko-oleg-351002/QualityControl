﻿using ServerWcfService.Services.Interface;
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

namespace QualityControl_Client.Forms.CustomerDirectory
{
    public partial class CustomerDirectoryForm : DirectoryForm
    {
        List<UilCustomer> Customers;
        public CustomerDirectoryForm() : base()
        {
            InitializeComponent();
            RefreshData();
            saveFileDialog2.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog1.Filter = "PDF file (*.pdf)|*.pdf";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            ICustomerRepository repository = ServiceChannelManager.Instance.CustomerRepository;
            Customers = repository.GetAll().ToList();
            int num = 0;
            foreach (var Customer in Customers)
            {
                num++;
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = num;
                row.Cells[1].Value = Customer.Organization;
                row.Cells[2].Value = Customer.Address;
                row.Cells[3].Value = Customer.Phone;
                row.Cells[4].Value = Customer.Contract;
                row.Cells[5].Value = Customer.ContractBeginDate;
                row.Cells[6].Value = Customer.ContractEndDate;
                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddCustomerForm AddCustomerForm = new AddCustomerForm(this);
            AddCustomerForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            ICustomerRepository repository = ServiceChannelManager.Instance.CustomerRepository;
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                repository.Delete(Customers[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            ICustomerRepository repository = ServiceChannelManager.Instance.CustomerRepository;
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeCustomerForm changeCustomerForm = new ChangeCustomerForm(this, Customers[rowsList[i].Index], rowsList[i]);
                changeCustomerForm.ShowDialog(this);
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

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
using BLL.Services;
using DAL.Repositories.Interface;

namespace QualityControl_Client.Forms.UserDirectory
{
    public partial class UserDirectoryForm : DirectoryForm
    {
        IUnitOfWork uow;
        List<BllUser> Users;
        public UserDirectoryForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            RefreshData();
        }

        public UserDirectoryForm() : base()
        {
            InitializeComponent();
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IUserService Service = new UserService(uow);
            Users = Service.GetAll().ToList();
            foreach (var User in Users)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = User.Employee != null ? User.Employee.Sirname : "<не указано>";
                row.Cells[1].Value = User.Employee != null ? User.Employee.Name : "<не указано>";
                row.Cells[2].Value = User.Login;
                row.Cells[3].Value = User.Password;
                row.Cells[4].Value = User.Role != null ? User.Role.Name : "";
                row.Cells[5].Value = User.ModifiedDate != null ? "Отредактировано " + User.ModifiedDate.Value.Date.ToString("dd.MM.yyyy") : "";
                dataGridView1.Rows.Add(row);
            }
        }

        protected override void button1_Click(object sender, EventArgs e)
        {
            UserAddForm UserAddForm = new UserAddForm(this, uow);
            UserAddForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IUserService Service = new UserService(uow);
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                if (Users[row.Index].Role.Name != "Администратор")
                {
                    Service.Delete(Users[row.Index]);
                }
                else
                {
                    MessageBox.Show("В системе должен быть адинистратор", "Оповещение");
                }
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            IUserService Service = new UserService(uow);
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeUserForm changeUserForm = new ChangeUserForm(this, Users[rowsList[i].Index], uow);
                changeUserForm.ShowDialog(this);
            }
            RefreshData();
        }
    }
}

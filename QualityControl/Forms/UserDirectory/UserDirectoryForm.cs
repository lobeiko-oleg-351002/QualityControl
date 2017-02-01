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

namespace QualityControl_Client.Forms.UserDirectory
{
    public partial class UserDirectoryForm : DirectoryForm
    {

        List<UilUser> Users;
        public UserDirectoryForm() : base()
        {
            InitializeComponent();
            RefreshData();

        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            IUserRepository repository = ServiceChannelManager.Instance.UserRepository;
            Users = repository.GetAll().ToList();
            foreach (var User in Users)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = User.Employee != null ? User.Employee.Sirname : "<не указано>";
                row.Cells[1].Value = User.Employee != null ? User.Employee.Name : "<не указано>";
                row.Cells[2].Value = User.Login;
                row.Cells[3].Value = User.Password;
                row.Cells[4].Value = User.Role != null ? User.Role.Name : "";
                dataGridView1.Rows.Add(row);
            }
        }

        protected override void button1_Click(object sender, EventArgs e)
        {
            UserAddForm UserAddForm = new UserAddForm(this);
            UserAddForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            IUserRepository repository = ServiceChannelManager.Instance.UserRepository;
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                if (Users[row.Index].Role.Name != "Администратор")
                {
                    repository.Delete(Users[row.Index]);
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
            IUserRepository repository = ServiceChannelManager.Instance.UserRepository;
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeUserForm changeUserForm = new ChangeUserForm(this, Users[rowsList[i].Index], rowsList[i]);
                changeUserForm.ShowDialog(this);
            }
            RefreshData();
        }
    }
}

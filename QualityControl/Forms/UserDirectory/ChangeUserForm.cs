using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Client.Forms.EmployeeDirectory;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Client.Forms.UserDirectory
{
    public partial class ChangeUserForm : ChangeForm
    {
        BllUser oldUser;
        IUnitOfWork uow;
        public ChangeUserForm() : base()
        {
            InitializeComponent();
        }

        IEnumerable<BllControlName> controlNames;
        public ChangeUserForm(DirectoryForm parent, BllUser oldUser, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            this.oldUser = oldUser;
            textBox1.Text = oldUser.Login;
            textBox2.Text = oldUser.Password;
            if (oldUser.Employee != null)
            {
                textBox3.Text = oldUser.Employee.Sirname + " " + oldUser.Employee.Name + " " + oldUser.Employee.Fathername;
            }

        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Введите пароль", "Оповещение");
            }
            else
            {
                oldUser.Login = textBox1.Text;
                oldUser.Password = textBox2.Text;
                oldUser.Employee = employee;
                oldUser.ModifiedDate = DateTime.Now;
                IUserService Service = new UserService(uow);
                Service.Update(oldUser);
                base.button2_Click(sender, e);
            }
        }

        BllEmployee employee;
        private void button3_Click(object sender, EventArgs e)
        {
            ChooseEmployeeForm EmployeeForm = new ChooseEmployeeForm(uow);
            EmployeeForm.ShowDialog(this);
            employee = EmployeeForm.GetChosenEmployee();
            if (employee != null)
            {
                textBox3.Text = employee.Sirname + " " + employee.Name + " " + employee.Fathername;
            }
        }
    }
}

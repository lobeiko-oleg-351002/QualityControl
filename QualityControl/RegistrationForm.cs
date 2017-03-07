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
using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;

namespace QualityControl_Client
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm(IUnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            CenterToScreen();
        }

        BllEmployee employee = null;
        IUnitOfWork uow;
        private void button3_Click(object sender, EventArgs e)
        {
            BllUser user = null;
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Введите данные учетной записи", "Оповещение");
            }
            else
            {
                IRoleService roleService = new RoleService(uow);
                var role = roleService.GetRoleByName("Работник");
                BllUser User = new BllUser
                {
                    Login = textBox1.Text,
                    Password = textBox2.Text,
                    Role = role,
                    Employee = employee
                };
                IUserService Service = new UserService(uow);
                user = Service.Create(User);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует", "Оповещение");
                }
                else
                {
                    MessageBox.Show("Регистрация прошла успешно", "Оповещение");
                    Close();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
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

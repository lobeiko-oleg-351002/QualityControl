
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
using DAL.Repositories.Interface;
using BLL.Services.Interface;
using BLL.Services;

namespace QualityControl_Client
{
    public partial class LogInForm : Form
    {
        public LogInForm(IUnitOfWork uow)
        {
            InitializeComponent();
            CenterToScreen();
            this.uow = uow;
        }

        public BllUser User { get; private set; }
        IUnitOfWork uow;
        private void button1_Click(object sender, EventArgs e)
        {
            RegistrationForm form = new RegistrationForm(uow);
            form.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IUserService Service = new UserService(uow);
            User =  Service.Authorize(textBox1.Text, textBox2.Text);
            if (User != null)
            {
                MessageBox.Show("Авторизация прошла успешно", "Оповещение");
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка авторизации", "Оповещение");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IRoleService Service = new RoleService(uow);

            User = new BllUser
            {
                Role = Service.GetRoleByName("Гость")
            };
            Close();
        }
    }
}

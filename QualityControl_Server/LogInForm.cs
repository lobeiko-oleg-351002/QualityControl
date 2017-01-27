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

namespace QualityControl_Client
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        public UilUser User { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistrationForm form = new RegistrationForm();
            form.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IUserRepository repository = ServiceChannelManager.Instance.UserRepository;
            User =  repository.Authorize(textBox1.Text, textBox2.Text);
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IRoleRepository repository = ServiceChannelManager.Instance.RoleRepository;

            User = new UilUser
            {
                Role = repository.GetRoleByName("Гость")
            };
            Close();
        }
    }
}

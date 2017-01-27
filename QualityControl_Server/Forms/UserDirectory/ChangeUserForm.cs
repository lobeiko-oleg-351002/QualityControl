using QualityControl_Client.Forms.EmployeeDirectory;
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
    public partial class ChangeUserForm : ChangeForm
    {
        UilUser oldUser;
        public ChangeUserForm() : base()
        {
            InitializeComponent();
        }

        IEnumerable<UilControlName> controlNames;
        public ChangeUserForm(DirectoryForm parent, UilUser oldUser, DataGridViewRow currentRow) : base(parent)
        {
            InitializeComponent();
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

                IUserRepository repository = ServiceChannelManager.Instance.UserRepository;
                repository.Update(oldUser);
                base.button2_Click(sender, e);
            }
        }

        UilEmployee employee;
        private void button3_Click(object sender, EventArgs e)
        {
            ChooseEmployeeForm EmployeeForm = new ChooseEmployeeForm();
            EmployeeForm.ShowDialog(this);
            employee = EmployeeForm.GetChosenEmployee();
            if (employee != null)
            {
                textBox3.Text = employee.Sirname + " " + employee.Name + " " + employee.Fathername;
            }
        }
    }
}

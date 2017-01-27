﻿using QualityControl_Client.Forms.EmployeeDirectory;
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
    public partial class UserAddForm : AddForm
    {
        public UserAddForm() : base()
        {
            InitializeComponent();
        }

        IEnumerable<UilControlName> controlNames;
        public UserAddForm(DirectoryForm parent) : base(parent)
        {
            InitializeComponent();
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            UilUser user = null;
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Введите данные учетной записи", "Оповещение");
            }
            else
            {
                IRoleRepository roleRepository = ServiceChannelManager.Instance.RoleRepository;
                var role = roleRepository.GetRoleByName("Работник");
                UilUser User = new UilUser
                {
                    Login = textBox1.Text,
                    Password = textBox2.Text,
                    Role = role,
                    Employee = employee
                };
                IUserRepository repository = ServiceChannelManager.Instance.UserRepository;
                user = repository.CreateWithFeedBack(User);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует", "Оповещение");
                }
                else
                {
                    MessageBox.Show("Регистрация прошла успешно", "Оповещение");
                    base.button2_Click(sender, e);
                }

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
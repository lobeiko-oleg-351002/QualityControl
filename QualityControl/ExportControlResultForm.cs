
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

namespace QualityControl_Client
{
    public partial class ExportControlResultForm : Form
    {
        List<BllControlName> ControlNames;
        public BllControlName SelectedControlName { get; private set; }
        public ExportControlResultForm(IUnitOfWork uow)
        {
            InitializeComponent();
            ControlNames = new List<BllControlName>();
            IControlNameService controlNameService = new ControlNameService(uow);
            var controlNames = controlNameService.GetAll();
            foreach (var controlName in controlNames)
            {
                ControlNames.Add(controlName);
                comboBox1.Items.Add(controlName.Name);
            }
            comboBox1.SelectedIndex = 0;
            CenterToScreen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectedControlName = ControlNames[comboBox1.SelectedIndex];
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedControlName = null;
            Close();
        }


    }
}

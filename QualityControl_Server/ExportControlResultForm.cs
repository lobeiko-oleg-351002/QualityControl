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
    public partial class ExportControlResultForm : Form
    {
        List<UilControlName> ControlNames;
        public UilControlName SelectedControlName { get; private set; }
        public bool isPdfSelected = false;
        public ExportControlResultForm()
        {
            InitializeComponent();
            ControlNames = new List<UilControlName>();
            IControlNameRepository controlNameRepository = ServiceChannelManager.Instance.ControlNameRepository;
            var controlNames = controlNameRepository.GetAll();
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                isPdfSelected = true;
            }
            else
            {
                isPdfSelected = false;
            }
        }
    }
}

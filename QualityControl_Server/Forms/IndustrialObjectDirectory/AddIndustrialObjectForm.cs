using QualityControl_Client.Forms.ComponentDirectory;
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

namespace QualityControl_Client.Forms.IndustrialObjectDirectory
{
    public partial class AddIndustrialObjectForm : AddForm
    {
        UilComponentLib ComponentLib;
        List<UilComponent> Components = new List<UilComponent>();
        public AddIndustrialObjectForm() : base()
        {
            InitializeComponent();
        }
        public AddIndustrialObjectForm(DirectoryForm parent) : base(parent)
        {
            InitializeComponent();
            ComponentLib = new UilComponentLib();
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                List<UilSelectedComponent> selectedComponents = new List<UilSelectedComponent>();
                foreach (var element in Components)
                {
                    selectedComponents.Add(new UilSelectedComponent
                    {
                        Component = element
                    });
                }
                ComponentLib.SelectedComponent = selectedComponents;
                UilIndustrialObject IndustrialObject = new UilIndustrialObject
                {
                    Name = textBox1.Text,
                    ComponentLib = ComponentLib
                };
                IIndustrialObjectRepository repository = ServiceChannelManager.Instance.IndustrialObjectRepository;
                repository.Create(IndustrialObject);
                base.button2_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChooseComponentForm ComponentForm = new ChooseComponentForm();
            ComponentForm.ShowDialog(this);
            UilComponent Component = ComponentForm.GetChosenComponent();
            if (Component != null)
            {
                Components.Add(Component);
                comboBox1.Items.Add(Component.Name);
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Components.RemoveAt(comboBox1.SelectedIndex);
            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
    }
}

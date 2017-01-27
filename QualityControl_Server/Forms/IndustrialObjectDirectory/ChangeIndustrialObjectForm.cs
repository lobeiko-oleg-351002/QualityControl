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
    public partial class ChangeIndustrialObjectForm : ChangeForm
    {
        UilIndustrialObject oldIndustrialObject;
        public ChangeIndustrialObjectForm() : base()
        {
            InitializeComponent();
        }
        //UilComponentLib ComponentLib;
        List<UilSelectedComponent> Components = new List<UilSelectedComponent>();
        public ChangeIndustrialObjectForm(DirectoryForm parent, UilIndustrialObject oldIndustrialObject, DataGridViewRow currentRow) : base(parent)
        {
            InitializeComponent();
            this.oldIndustrialObject = oldIndustrialObject;
            //ComponentLib = oldIndustrialObject.ComponentLib;
            textBox1.Text = (string)currentRow.Cells[0].Value;
            if (oldIndustrialObject.ComponentLib != null)
            {
                foreach (var Component in oldIndustrialObject.ComponentLib.SelectedComponent)
                {
                    comboBox1.Items.Add(Component.Component.Name);
                    Components.Add(Component);
                }
            }
            comboBox1.SelectedIndex = 0;
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                oldIndustrialObject.Name = textBox1.Text;
                oldIndustrialObject.ComponentLib.SelectedComponent = Components;

                IIndustrialObjectRepository repository = ServiceChannelManager.Instance.IndustrialObjectRepository;
                repository.Update(oldIndustrialObject);
                base.button2_Click(sender, e);
            }
        }

        UilComponent Component;
        private void button3_Click(object sender, EventArgs e)
        {
            ChooseComponentForm ComponentForm = new ChooseComponentForm();
            ComponentForm.ShowDialog(this);
            UilComponent Component = ComponentForm.GetChosenComponent();
            if (Component != null)
            {
                Components.Add(new UilSelectedComponent
                {
                    Component = Component
                });
                comboBox1.Items.Add(Component.Name);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Components.RemoveAt(comboBox1.SelectedIndex);
            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
        }
    }
}

using QualityControl_Client.Forms.TemplateDirectory;
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

namespace QualityControl_Client.Forms.ComponentDirectory
{
    public partial class ChangeComponentForm : ChangeForm
    {
        UilComponent oldComponent;
        UilTemplate template;
        public ChangeComponentForm() : base()
        {
            InitializeComponent();
        }

        public ChangeComponentForm(DirectoryForm parent, UilComponent oldComponent, DataGridViewRow currentRow) : base(parent)
        {
            InitializeComponent();
            this.oldComponent = oldComponent;
            textBox1.Text = (string)currentRow.Cells[0].Value;
            textBox2.Text = oldComponent.Pressmark;
            maskedTextBox1.Text = oldComponent.Template != null ? oldComponent.Template.Name : "<отсутствует>";
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                oldComponent.Name = textBox1.Text;
                oldComponent.Template = template;
                oldComponent.Pressmark = textBox2.Text;
                IComponentRepository repository = ServiceChannelManager.Instance.ComponentRepository;
                repository.Update(oldComponent);
                base.button2_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChooseTemplateForm templateForm = new ChooseTemplateForm();
            templateForm.ShowDialog(this);
            template = templateForm.GetChosenTemplate();
            if (template != null)
            {
                maskedTextBox1.Text = template.Name;
            }
        }
    }
}

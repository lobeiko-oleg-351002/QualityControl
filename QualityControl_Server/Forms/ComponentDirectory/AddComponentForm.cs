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
    public partial class AddComponentForm : AddForm
    {
        public AddComponentForm() : base()
        {
            InitializeComponent();
        }
        public AddComponentForm(DirectoryForm parent) : base(parent)
        {
            InitializeComponent();
        }

        UilTemplate template;
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

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                UilComponent component = new UilComponent
                {
                    Name = textBox1.Text,
                    Template = template,
                    Pressmark = textBox2.Text
                };
                IComponentRepository repository = ServiceChannelManager.Instance.ComponentRepository;
                repository.Create(component);
                base.button2_Click(sender, e);
            }
        }
    }
}

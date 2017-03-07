using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Client.Forms.TemplateDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Client.Forms.ComponentDirectory
{
    public partial class AddComponentForm : AddForm
    {
        public AddComponentForm() : base()
        {
            InitializeComponent();
        }
        IUnitOfWork uow;
        public AddComponentForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
        }

        BllTemplate template;
        private void button3_Click(object sender, EventArgs e)
        {
            ChooseTemplateForm templateForm = new ChooseTemplateForm(uow);
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
                BllComponent component = new BllComponent
                {
                    Name = textBox1.Text,
                    Template = template,
                    Pressmark = textBox2.Text
                };
                IComponentService Service = new ComponentService(uow);
                Service.Create(component);
                base.button2_Click(sender, e);
            }
        }
    }
}

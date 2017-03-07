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
using BLL.Services;
using BLL.Services.Interface;
using BLL.Entities;
using DAL.Repositories.Interface;

namespace QualityControl_Client.Forms.ComponentDirectory
{
    public partial class ChangeComponentForm : ChangeForm
    {
        BllComponent oldComponent;
        BllTemplate template;
        public ChangeComponentForm() : base()
        {
            InitializeComponent();
        }
        IUnitOfWork uow;
        public ChangeComponentForm(DirectoryForm parent, BllComponent oldComponent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            this.oldComponent = oldComponent;
            textBox1.Text = oldComponent.Name;
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
                IComponentService Service = new ComponentService(uow);
                Service.Update(oldComponent);
                base.button2_Click(sender, e);
            }
        }

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
    }
}

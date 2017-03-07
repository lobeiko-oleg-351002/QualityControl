using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Client.Forms.RequirementDocumentationDirectory
{
    public partial class ChangeRequirementDocumentationForm : ChangeForm
    {
        BllRequirementDocumentation oldRequirementDocumentation;
        IUnitOfWork uow;
        public ChangeRequirementDocumentationForm() : base()
        {
            InitializeComponent();
        }

        public ChangeRequirementDocumentationForm(DirectoryForm parent, BllRequirementDocumentation oldRequirementDocumentation, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.oldRequirementDocumentation = oldRequirementDocumentation;
            this.uow = uow;
            textBox1.Text = oldRequirementDocumentation.Name;
            textBox2.Text = oldRequirementDocumentation.Pressmark;
            textBox3.Text = oldRequirementDocumentation.ObjectGroup;

        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                oldRequirementDocumentation.Name = textBox1.Text;
                oldRequirementDocumentation.Pressmark = textBox2.Text;
                oldRequirementDocumentation.ObjectGroup = textBox3.Text;
                IRequirementDocumentationService Service = new RequirementDocumentationService(uow);
                Service.Update(oldRequirementDocumentation);
                base.button2_Click(sender, e);
            }
        }
    }
}

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
    public partial class AddRequirementDocumentationForm : AddForm
    {
        IUnitOfWork uow;
        public AddRequirementDocumentationForm() : base()
        {
            InitializeComponent();
        }
        public AddRequirementDocumentationForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                BllRequirementDocumentation RequirementDocumentation = new BllRequirementDocumentation
                {
                    Name = textBox1.Text,
                    Pressmark = textBox2.Text,
                    ObjectGroup = textBox3.Text
                };
                IRequirementDocumentationService Service = new RequirementDocumentationService(uow);
                Service.Create(RequirementDocumentation);
                base.button2_Click(sender, e);
            }
        }
    }
}

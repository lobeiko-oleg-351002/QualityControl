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

namespace QualityControl_Client.Forms.RequirementDocumentationDirectory
{
    public partial class AddRequirementDocumentationForm : AddForm
    {
        public AddRequirementDocumentationForm() : base()
        {
            InitializeComponent();
        }
        public AddRequirementDocumentationForm(DirectoryForm parent) : base(parent)
        {
            InitializeComponent();
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                UilRequirementDocumentation RequirementDocumentation = new UilRequirementDocumentation
                {
                    Name = textBox1.Text,
                    Pressmark = textBox2.Text,
                    ObjectGroup = textBox3.Text
                };
                IRequirementDocumentationRepository repository = ServiceChannelManager.Instance.RequirementDocumentationRepository;
                repository.Create(RequirementDocumentation);
                base.button2_Click(sender, e);
            }
        }
    }
}

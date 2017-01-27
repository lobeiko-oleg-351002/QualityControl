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
    public partial class ChangeRequirementDocumentationForm : ChangeForm
    {
        UilRequirementDocumentation oldRequirementDocumentation;
        public ChangeRequirementDocumentationForm() : base()
        {
            InitializeComponent();
        }

        public ChangeRequirementDocumentationForm(DirectoryForm parent, UilRequirementDocumentation oldRequirementDocumentation, DataGridViewRow currentRow) : base(parent)
        {
            InitializeComponent();
            this.oldRequirementDocumentation = oldRequirementDocumentation;
            textBox1.Text = (string)currentRow.Cells[0].Value;

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
                IRequirementDocumentationRepository repository = ServiceChannelManager.Instance.RequirementDocumentationRepository;
                repository.Update(oldRequirementDocumentation);
                base.button2_Click(sender, e);
            }
        }
    }
}

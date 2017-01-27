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

namespace QualityControl_Client.Forms.ControlMethodDocumentationDirectory
{
    public partial class ChangeControlMethodDocumentationForm : ChangeForm
    {
        UilControlMethodDocumentation oldControlMethodDocumentation;
        IEnumerable<UilControlName> controlNames;
        public ChangeControlMethodDocumentationForm() : base()
        {
            InitializeComponent();

        }
        public ChangeControlMethodDocumentationForm(DirectoryForm parent, UilControlMethodDocumentation oldControlMethodDocumentation, DataGridViewRow currentRow) : base(parent)
        {
            InitializeComponent();
            IControlNameRepository controlNameRepository = ServiceChannelManager.Instance.ControlNameRepository;
            controlNames = controlNameRepository.GetAll();
            foreach (var name in controlNames)
            {
                comboBox1.Items.Add(name.Name);
            }
            this.oldControlMethodDocumentation = oldControlMethodDocumentation;
            textBox1.Text = (string)currentRow.Cells[0].Value;
            textBox2.Text = (string)currentRow.Cells[1].Value;
            comboBox1.SelectedItem = oldControlMethodDocumentation.ControlName.Name;
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите название", "Оповещение");
            }
            else
            {
                oldControlMethodDocumentation.Name = textBox1.Text;
                oldControlMethodDocumentation.Pressmark = textBox2.Text;
                oldControlMethodDocumentation.ControlName = controlNames.ElementAt(comboBox1.SelectedIndex);
                IControlMethodDocumentationRepository repository = ServiceChannelManager.Instance.ControlMethodDocumentationRepository;
                repository.Update(oldControlMethodDocumentation);
                base.button2_Click(sender, e);
            }
        }
    }
}

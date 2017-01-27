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
    public partial class AddControlMethodDocumentationForm : AddForm
    {
        public AddControlMethodDocumentationForm() : base()
        {
            InitializeComponent();
        }

        IEnumerable<UilControlName> controlNames;

        public AddControlMethodDocumentationForm(DirectoryForm parent) : base(parent)
        {
            InitializeComponent();
            IControlNameRepository controlNameRepository = ServiceChannelManager.Instance.ControlNameRepository;
            controlNames = controlNameRepository.GetAll();
            foreach (var name in controlNames)
            {
                comboBox1.Items.Add(name.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите название", "Оповещение");
            }
            else
            {
                UilControlMethodDocumentation controlMethodDocumentation = new UilControlMethodDocumentation
                {
                    Name = textBox1.Text,
                    Pressmark = textBox2.Text,
                    ControlName = comboBox1.SelectedIndex != -1 ? controlNames.ElementAt(comboBox1.SelectedIndex) : null
                };
                IControlMethodDocumentationRepository repository = ServiceChannelManager.Instance.ControlMethodDocumentationRepository;
                repository.Create(controlMethodDocumentation);
                base.button2_Click(sender, e);
            }
        }
    }
}

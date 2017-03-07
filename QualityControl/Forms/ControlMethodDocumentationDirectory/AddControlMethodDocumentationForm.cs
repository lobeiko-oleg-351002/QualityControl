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

namespace QualityControl_Client.Forms.ControlMethodDocumentationDirectory
{
    public partial class AddControlMethodDocumentationForm : AddForm
    {
        IUnitOfWork uow;
        public AddControlMethodDocumentationForm() : base()
        {
            InitializeComponent();
        }

        IEnumerable<BllControlName> controlNames;

        public AddControlMethodDocumentationForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            IControlNameService controlNameService = new ControlNameService(uow);
            controlNames = controlNameService.GetAll();
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
                BllControlMethodDocumentation controlMethodDocumentation = new BllControlMethodDocumentation
                {
                    Name = textBox1.Text,
                    Pressmark = textBox2.Text,
                    ControlName = comboBox1.SelectedIndex != -1 ? controlNames.ElementAt(comboBox1.SelectedIndex) : null
                };
                IControlMethodDocumentationService Service = new ControlMethodDocumentationService(uow);
                Service.Create(controlMethodDocumentation);
                base.button2_Click(sender, e);
            }
        }
    }
}

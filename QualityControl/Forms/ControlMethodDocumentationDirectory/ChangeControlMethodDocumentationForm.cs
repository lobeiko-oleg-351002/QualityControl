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
    public partial class ChangeControlMethodDocumentationForm : ChangeForm
    {
        BllControlMethodDocumentation oldControlMethodDocumentation;
        IEnumerable<BllControlName> controlNames;
        IUnitOfWork uow;
        public ChangeControlMethodDocumentationForm() : base()
        {
            InitializeComponent();

        }
        public ChangeControlMethodDocumentationForm(DirectoryForm parent, BllControlMethodDocumentation oldControlMethodDocumentation, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            IControlNameService controlNameService = new ControlNameService(uow);
            controlNames = controlNameService.GetAll();
            foreach (var name in controlNames)
            {
                comboBox1.Items.Add(name.Name);
            }
            this.oldControlMethodDocumentation = oldControlMethodDocumentation;
            textBox1.Text = oldControlMethodDocumentation.Name;
            textBox2.Text = oldControlMethodDocumentation.Pressmark;
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
                IControlMethodDocumentationService Service = new ControlMethodDocumentationService(uow);
                Service.Update(oldControlMethodDocumentation);
                base.button2_Click(sender, e);
            }
        }
    }
}
